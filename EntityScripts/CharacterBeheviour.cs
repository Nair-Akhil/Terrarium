using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CharacterBeheviour : EntityBeheviour,ISocial,IAdvertise {

	public bool randomize = false;
	public float hungerDecay = -3f;
	public float thirstDecay = -3f;

	public float socialDecay = -1f;

	public float hungerAtten = 10;
	public float thirstAtten = 10;

	public float socialAtten = 10;

	public float likeable = 20;

	public GameObject cameraObject;

	public float range = 300;
	float c = 0;
	Camera camera;

	bool coroutineRunning = false;

	public Animator animator;

	Queue<IEnumerator> actionQueue;

	public float advertised(){
		float sum =0;
		for(int i=0; i<stats.Count;i++){
			sum +=stats[i][0];
		}
		return ((sum/stats.Count)/100)*likeable;
	}
	public float socialise(){
		float sum =0;
		for(int i=0; i<stats.Count;i++){
			sum +=stats[i][0];
		}
		return ((sum/stats.Count)/100)*likeable;
		
	}
	public override void setup(){

		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		speed = 20.0f;
		
		animator = GetComponent<Animator>();

		stats = new List<float[]>();

		actionQueue = new Queue<IEnumerator>();

		if(randomize){
			hungerAtten =Random.Range(5,10);
			thirstAtten =Random.Range(5,10);
			socialAtten =Random.Range(5,10);
		}
	}

	public override void Update(){

		getInput();
		adjustStats();
		StartCoroutine(runActionQueue());
		
		
	}
	

	public override void initializeStats(){
		float[] hunger = new float[4];
		hunger[0] = 100;
		hunger[1] = hungerDecay;
		hunger[2] = 0;
		hunger[3] = hungerAtten;
		stats.Add(hunger);
		
		float[] thirst = new float[4];
		thirst[0] = 100;
		thirst[1] = thirstDecay;
		thirst[2] = 0;
		thirst[3] = thirstAtten;
		stats.Add(thirst);

		float[] social = new float[4];
		social[0]=100;
		social[1]=socialDecay;
		social[2] = 0;
		social[3] = socialAtten;
		stats.Add(social);
	}


	void getInput(){

		if(Input.GetMouseButtonDown(0)){
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit = new RaycastHit();

			if(Physics.Raycast(ray,out hit)){

				target = hit.point;

			}
		}
	}
	

	IEnumerator moveAction(Transform target, float minDist){

		coroutineRunning = true;


		while((Vector3.Distance(transform.position,target.position)>minDist)){
			
			Vector3 moveDirection = Vector3.zero;
			moveDirection.y -=gravity*Time.deltaTime;

			Vector3 targetDirection = Vector3.Normalize(characterController.transform.position-target.position)*-1;

			targetDirection.y = 0;
			moveDirection +=transform.forward*speed;
			transform.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.forward,targetDirection,0.2f));

			animator.SetBool("Moving",true);

			characterController.SimpleMove(moveDirection);

			
			yield return null;

			if(target==null){
				print("broken");
				break;
			}
			
			
		}
		
		coroutineRunning = false;

		animator.SetBool("Moving",false);

		yield break;
		

			
		
		
		
	}

	private IEnumerator runActionQueue(){

		while(true){
			if((actionQueue.Count>0)&&(!coroutineRunning)){
				//print(actionQueue.Count);
				yield return StartCoroutine(actionQueue.Dequeue());
			}else if (actionQueue.Count<1){
				actionSelection();
				
			}
			yield return null;
		}

	}

	void actionSelection(){
		otherEntities = GameObject.FindGameObjectsWithTag("Entity").ToList();

		List<float[]> ranks = new List<float[]>();

		for(int i=0; i<otherEntities.Count;i++){

			if(otherEntities[i]){

				if((otherEntities[i].GetComponent<EntityBeheviour>() is IAdvertise)&&((Vector3.Distance(transform.position,otherEntities[i].transform.position)<range))){
					
					
					float[] tempRank = new float[3];

					if(otherEntities[i].GetComponent<EntityBeheviour>() is IEdible){
						if(otherEntities[i].GetComponent<Food>().ready){

							tempRank[0] = i;
							tempRank[1] = ((stats[0][3]/stats[0][0])-(stats[0][3]/(stats[0][0]+otherEntities[i].GetComponent<Food>().advertised())));
							tempRank[2] = 0;
							ranks.Add(tempRank);

						}

					}else if(otherEntities[i].GetComponent<EntityBeheviour>() is IDrinkable){
							tempRank[0] = i;
							tempRank[1] = ((stats[1][3]/stats[1][0])-(stats[1][3]/(stats[1][0]+otherEntities[i].GetComponent<Waterdrop>().advertised())));
							tempRank[2] = 1;
							ranks.Add(tempRank);

						

					}
					
					else if((otherEntities[i].GetComponent<EntityBeheviour>() is ISocial)&&(otherEntities[i].transform!=transform)){
						tempRank[0] = i;
						tempRank[1] = ((stats[2][3]/stats[2][0])-(stats[2][3]/(stats[2][0]+otherEntities[i].GetComponent<CharacterBeheviour>().advertised())));
						tempRank[2] = 2;
						ranks.Add(tempRank);
					}

					
				}


			}

		}

		print("SCORE before: "+ranks[0][1].ToString()+"-"+ranks[0][2]+"----"+ranks[ranks.Count-1][1]+"-"+ranks[ranks.Count-1][2].ToString());
		ranks = ranks.OrderBy(V=>V[1]).ToList();
		print("SCORE: "+ranks[0][1].ToString()+"-"+ranks[0][2]+"----"+ranks[ranks.Count-1][1]+"-"+ranks[ranks.Count-1][2].ToString());
		//print(ranks.Count);
		float[] hRanked= ranks[Random.Range(ranks.Count-1,ranks.Count-4)];
	
		action(otherEntities[(int)hRanked[0]].transform,(int)hRanked[2],hRanked[1]);
		

	}

	void action(Transform t,int i, float s){
		actionQueue.Enqueue(moveAction(t,20));
		actionQueue.Enqueue(updateStatAction(t, i,s));
	}
	IEnumerator updateStatAction(Transform t,int i, float v){
		coroutineRunning = true;
		if(t!=null){
		if(i == 0){
			stats[i][2] = t.gameObject.GetComponent<Food>().eat();
		
		}else if(i==1){
			stats[i][2]=t.gameObject.GetComponent<Waterdrop>().drink();
		}else if(i==2){
			stats[i][2]=t.gameObject.GetComponent<CharacterBeheviour>().socialise();
		}
		}
		coroutineRunning = false;
		yield break;
		
	}


	public override void OnGUI(){

		Vector3 screencoord = camera.WorldToScreenPoint(this.transform.position);
	
		screencoord.y = camera.pixelHeight-screencoord.y;
		
		GUI.Label(new Rect(screencoord.x,screencoord.y,100,100),stats[0][0].ToString());
		GUI.Label(new Rect(screencoord.x,screencoord.y+10,100,100),stats[1][0].ToString());
		GUI.Label(new Rect(screencoord.x,screencoord.y+20,100,100),stats[2][0].ToString());
		GUI.Label(new Rect(screencoord.x,screencoord.y+30,100,100),actionQueue.Count.ToString());
		
	}
 
}
