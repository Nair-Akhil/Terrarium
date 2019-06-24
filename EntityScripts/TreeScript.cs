using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : EntityBeheviour {

	// Use this for initialization
	public GameObject seed;

	float count;
	int maxCount = 50;

	List<GameObject> clouds;
	
	// Update is called once per frame
	public override void setup(){
		count = 0;

		clouds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cloud"));

		
	}
	public override void Update () {

		if(count <= 0){

		for(int i=0; i<otherEntities.Count;i++){
			if(otherEntities[i]){
					
				
				
				if(otherEntities[i].GetComponent<Raindrop>()!=null){
					if(Vector3.Distance(transform.position,otherEntities[i].transform.position)<30){
						
						GameObject instance = Instantiate(seed,transform.position+new Vector3(20,20),Quaternion.identity);
						instance.GetComponent<Rigidbody>().AddForce(Vector3.Normalize((new Vector3(Random.Range(-1,1),0.2f,Random.Range(-1,1)))*0.5f));
						count = maxCount;
						break;
					}
				}
			}
		}}
		else{

			count -=2*Time.deltaTime;

		}
		float d = 10000;
		int index = -1;
		for(int i=0; i<clouds.Count;i++){
			


			float cd = Vector3.Distance(transform.position,clouds[i].transform.position);

			if(cd<d){
				d = cd;
				index = i;
				target = clouds[i].transform.position;

			}

			
			
		}

		Vector3 vel = target-transform.position;
		vel.Normalize();
		vel.y = -gravity*Time.deltaTime;

		characterController.Move(vel);
		
	}

}
