using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : EntityBeheviour,IEdible,IAdvertise {


	public int satisfaction = 20;
	float cSatisfaction ;

	public bool ready = true;

	Camera camera;

	Material material;
	

	public GameObject grassPlane;
	GameObject[] grassPlanes;

	public float eat(){

		float t = stats[0][0];
		stats[0][0] = 0;
		ready = false;
		return(t);
		
	}

	public float advertised(){
		return(stats[0][0]);
	}

	public override void setup(){

		satisfaction = satisfaction+Random.Range(-5,5);

		cSatisfaction = 0;
		ready = true;


		

		RaycastHit hit;
		Ray ray = new Ray(transform.position,Vector3.down);
		if(Physics.Raycast(ray,out hit)){
			transform.position = hit.point;
		}

		grassPlanes = new GameObject[5];

		for(int i=0; i<grassPlanes.Length;i++){
			
			grassPlanes[i] = Instantiate(grassPlane,transform.position,transform.rotation *  Quaternion.Euler(0,Random.Range(0,360),0) );
			grassPlanes[i].transform.parent = transform;

		}

		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

	}

	public override void Update(){

		adjustStats();
		getCurrentTarget();
		move();

	}

	public override void initializeStats(){
		float[] satisfactiona = new float[3];
		satisfactiona[0] = satisfaction;
		satisfactiona[1] = 1;
		satisfactiona[2] = 0;
		stats.Add(satisfactiona);
	}

	public override void adjustStats(){
		

		if(stats.Count>0){
			for(int i=0; i<stats.Count;i++){
				if(stats[i][0]<satisfaction){
					ready = false;
					
					stats[i][0]+=(stats[i][1])*Time.deltaTime;
					
					if(stats[i][2]!=0){
						stats[i][0]-=stats[i][2];
						stats[i][2] = 0;
					}

					if(stats[i][0]>satisfaction*0.75f){
						if(Random.Range(0,100)>96){
							Destroy(gameObject);
						}
					}
				}else{
					ready = true;
				}
			}
		}

		
		


		/*
		
		if(cSatisfaction < satisfaction){
			cSatisfaction += 1*Time.deltaTime;
			ready = false;
		}else{
			ready = true;
		} */

		//transform.localScale= new Vector3(transform.localScale.x,(cSatisfaction/satisfaction),transform.localScale.z);


	}

/* 
	public void OnGUI(){

		Vector3 screencoord = camera.WorldToScreenPoint(this.transform.position);
	
		

		GUI.Label(new Rect(screencoord.x,camera.pixelHeight-screencoord.y,100,100),stats[0][0].ToString());
		GUI.Label(new Rect(screencoord.x,camera.pixelHeight-screencoord.y+20,100,100),ready.ToString());
	}
	*/
}
