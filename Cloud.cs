using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cloud : MonoBehaviour {

	// Use this for initialization

	enum cloudState{ normal, raining}

	List<GameObject> clouds;

	cloudState state;
	ParticleSystem particleSystem;

	Rigidbody rigidbody;

	public GameObject raindrop;

	public int water;

	List<Transform> raindrops;

	float rand;

	void Start () {

		rand = Random.Range(-100,100);

		clouds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cloud"));
		

		particleSystem = GetComponent<ParticleSystem>();
		rigidbody = GetComponent<Rigidbody>();
		raindrops = new List<Transform>();
		for(int i=0; i<water;i++){

			raindrops.Add(Instantiate(raindrop,this.transform.position,transform.rotation).transform);

			raindrops[i].GetComponent<Raindrop>().parent = this.transform;

		}

		state = cloudState.normal;


		
	}
	
	// Update is called once per frame
	void Update () {

		particleSystem.startColor = new Color(1,1,1,(raindrops.Count/100f));

		
		switch(state){

			case cloudState.raining:

				if(raindrops.Count>0){
					
					if(Time.frameCount%10==0){
						
						
						raindrops[0].GetComponent<Raindrop>().parent = null;
						raindrops.RemoveAt(0);
					}
				}

				if(Sun.temperature !=heat.warm){

					state = cloudState.normal;

				}

				break;

			case cloudState.normal:

				rigidbody.AddForce(new Vector3(Mathf.PerlinNoise((Time.frameCount+rand)*0.5f,(Time.frameCount+rand)*0.5f)-0.5f,0,Mathf.PerlinNoise((Time.frameCount-rand)*0.5f,(Time.frameCount-rand)*0.5f)-0.5f)*3);

				if(Sun.temperature != heat.normal){

					state = cloudState.raining;

				}

				break;


		}

		for(int i=0; i<clouds.Count;i++){

			Vector3 vel = transform.position-clouds[i].transform.position;
			vel.Normalize();
			vel = vel*5;
			if(Vector3.Distance(transform.position,clouds[i].transform.position)<200){

				rigidbody.AddForce(vel);
			
			}else{

				rigidbody.AddForce(-vel);
			}

		}


		
		
		
	}

	public void addRaindrop(Transform t){

		raindrops.Add(t);

	}
}
