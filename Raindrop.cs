using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum raindropState{	CloudEnter,
					Cloud,
					CloudExit,
					RainEnter,
					Rain,
					RainExit,
					PuddleEnter,
					Puddle,
					PuddleExit,
					VapourEnter,
					Vapour,
					VapourExit};

public class Raindrop : MonoBehaviour,IDrinkable,IAdvertise {

	// Use this for initialization

	Rigidbody rigidbody;


	public GameObject waterdrop;

	bool targeted = false;

	public float Satisfaction = 20;

	public float maxdist = 5;
	public raindropState state;

	public Transform parent = null;

	ConfigurableJoint joint;

	List<Transform> otherRain;
	List<Transform> clouds;

	public GameObject camera;

	MeshFilter mesh;
	float maxDist;

	bool grounded = false;

	Renderer renderer;

	public float drink(){

		state = raindropState.PuddleExit;
		return Satisfaction;


	}

	public float advertised(){
		return Satisfaction;
	}

	void Start () {

		camera = GameObject.FindGameObjectWithTag("MainCamera");


		mesh = GetComponent<MeshFilter>();
		rigidbody = GetComponent<Rigidbody>();
		joint = GetComponent<ConfigurableJoint>();
		state = raindropState.CloudEnter;
		otherRain = new List<Transform>();
		clouds = new List<Transform>();
		renderer = GetComponent<Renderer>();

		GameObject[] other = GameObject.FindGameObjectsWithTag("Entity");
		

		for(int i=0; i<other.Length;i++){

			if(other[i].GetComponent<Raindrop>()!=null){
				otherRain.Add(other[i].transform);
			}

		}


		other = GameObject.FindGameObjectsWithTag("Cloud");

		for(int i=0; i<other.Length;i++){

			clouds.Add(other[i].transform);
		}

	}
	
	// Update is called once per frame
	void Update () {

		mesh.transform.LookAt(camera.transform);


		
		switch(state){

			case raindropState.CloudEnter:

			
				rigidbody.useGravity = false;
				GetComponent<SphereCollider>().enabled = false;
				GetComponent<MeshRenderer>().enabled = false;
				joint.connectedBody = parent.GetComponent<Rigidbody>();
				joint.xMotion = ConfigurableJointMotion.Limited;
				joint.yMotion = ConfigurableJointMotion.Limited;
				joint.zMotion = ConfigurableJointMotion.Limited;
				
				
			
				state = raindropState.Cloud;
				break;

			case raindropState.Cloud:

				if(parent == null){

					state = raindropState.CloudExit;
				}


				break;

			case raindropState.CloudExit:

				GetComponent<SphereCollider>().enabled = true;
				GetComponent<MeshRenderer>().enabled = true;
				joint.connectedBody = null;
				joint.xMotion = ConfigurableJointMotion.Free;
				joint.yMotion = ConfigurableJointMotion.Free;
				joint.zMotion = ConfigurableJointMotion.Free;
				


				state = raindropState.RainEnter;
				
				break;

			case raindropState.RainEnter:

				
				GetComponent<SphereCollider>().enabled = true;

				rigidbody.useGravity = true;

				//rigidbody.velocity = new Vector3(0,0,0);

				rigidbody.AddForce(new Vector3(0,-1,0));

				state = raindropState.Rain;

				break;

			case raindropState.Rain:

				if(grounded){ state = raindropState.RainExit;}

				break;

			case raindropState.RainExit:

				state = raindropState.PuddleEnter;

				break;

			case raindropState.PuddleEnter:

				
				state = raindropState.Puddle;
				if(Random.Range(0,100)>90){
					GameObject temp = Instantiate(waterdrop,transform.position+=new Vector3(0,20,0),transform.rotation);
					temp.transform.parent = null;
					float ts = Satisfaction + Random.Range(-10,10);
					temp.GetComponent<Waterdrop>().ad = ts;
					temp.GetComponent<Waterdrop>().sat = ts;
				}

				break;

			case raindropState.Puddle:

				Vector3 target = Vector3.zero;
				int c = 0;

				Vector3 closest = new Vector3(1000,1000,1000);
				float dist = Vector3.Distance(closest,transform.position);

				
				for(int i=0; i<otherRain.Count;i++){

					if(otherRain[i].GetComponent<Raindrop>().state==raindropState.Puddle){
						target+=otherRain[i].transform.position;
						c+=1;
						float d = Vector3.Distance(otherRain[i].transform.position,transform.position);
						if(d<dist){
							dist = d;
							closest = otherRain[i].transform.position;
						}



					}

				}
				
/*
				if(target==Vector3.zero){

					rigidbody.AddForce((target-transform.position).normalized*3);


				}else{
 */
					target = (target/c)-transform.position;
					rigidbody.AddForce(target.normalized*3);
//				}

				rigidbody.AddForce(new Vector3(0,-4,0));


				if(Sun.temperature == heat.cool){
					state = raindropState.PuddleExit;
				}
				

				break;

			case raindropState.PuddleExit:
			
				rigidbody.useGravity = false;
				GetComponent<SphereCollider>().enabled = false;

				state = raindropState.VapourEnter;

				grounded = false;
				break;

			case raindropState.VapourEnter:

				
				state = raindropState.Vapour;

				break;

			case raindropState.Vapour:

				int targetIndex = 0;
				float distc = 100000;

				
				for(int i=0; i<clouds.Count;i++){

					float dc = Vector3.Distance(transform.position,clouds[i].position);

					if( dc<distc){
						
						distc = dc;

						targetIndex = i;

					}


				}

				rigidbody.AddForce((clouds[targetIndex].position-transform.position).normalized*5);
				rigidbody.AddForce(new Vector3(Mathf.PerlinNoise(transform.position.x,transform.position.y),0,Mathf.PerlinNoise(transform.position.x+100,transform.position.y+100)));

				/*
				if(Sun.temperature != heat.cool){

					state = raindropState.RainEnter;

				} */

				if(distc<20){

					parent = clouds[targetIndex];
					clouds[targetIndex].GetComponent<Cloud>().addRaindrop(this.transform);

					state = raindropState.VapourExit;


				}

				break;

			case raindropState.VapourExit:

				state = raindropState.CloudEnter;

				break;
			

		}


	}

	void OnCollisionEnter(Collision theCollision)
 {
     if (theCollision.gameObject.name == "Ground")
     {
         grounded = true;
     }
 }
	public void cloud(Transform transformo){

		transform.parent = transformo;
		


	}

}
