using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : EntityBeheviour {

	// Use this for initialization

	Rigidbody rigidbody;
	public GameObject plant;
	

	public override void setup(){

		rigidbody = GetComponent<Rigidbody>();
	
		speed = 15.0f;

	}
	
	// Update is called once per frame


	public override void Update(){

		getCurrentTarget();
		move();
	}

	public override void move(){

		moveDirection = target-transform.position;
		moveDirection.Normalize();
		moveDirection*=speed;
		moveDirection.y -= gravity*Time.deltaTime;


/*
		if(Vector3.Distance(characterController.transform.position,target)>5){

			Vector3 targetDirection = Vector3.Normalize(characterController.transform.position-target)*-1;

			targetDirection.y = 0;

			moveDirection += transform.forward*speed;

			//transform.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.forward,targetDirection,0.2f));


		}else{


		}
 */

		
		rigidbody.AddForce(moveDirection);

	}

	public override void getCurrentTarget(){

		int index = -1;
		float d = 100000;

		for(int i=0; i<otherEntities.Count;i++){
			if(otherEntities[i]){
				if((otherEntities[i].GetComponent<Raindrop>())&&(otherEntities[i].GetComponent<Raindrop>().state == raindropState.Puddle)){

					

					float cd = Vector3.Distance(transform.position,otherEntities[i].transform.position);

					if((cd)<d){
						d = cd;
						index = i;
						target = otherEntities[i].GetComponent<Transform>().position;
					}

				}
			}
		}

		if(index == -1){
			target = transform.position;
		}

		if(d<10){

			otherEntities[index].GetComponent<Raindrop>().drink();
			Instantiate(plant,transform.transform.position,Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
