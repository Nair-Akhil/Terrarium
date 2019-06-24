using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EntityBeheviour : MonoBehaviour {

	protected List<GameObject> otherEntities; 
	protected List<float[]> stats; 
	public CharacterController characterController;
	protected float speed = 8.0f;
	public float gravity = 20.0f;
	public Vector3 target;
	protected Vector3 moveDirection = Vector3.zero;
	public virtual void Start () {

		
		otherEntities = new List<GameObject>(GameObject.FindGameObjectsWithTag("Entity"));
		stats = new List<float[]>();

		setup();
		initializeStats();

	}

	public virtual void setup(){
	}
	
	public virtual void initializeStats(){
		float[] hunger = new float[4];
		hunger[0] = 100;
		hunger[1] = -5f;
		hunger[2] = 0;
		hunger[3] =80;
		stats.Add(hunger);

	}

	protected void addNewStat(float a, float b, float c, float d){
		float[] temp = new float[4];
		temp[0] = a;
		temp[1] = b;
		temp[2] = c;
		temp[3] = d;
		stats.Add(temp);
	}
	public virtual void Update () {

		adjustStats();
		getCurrentTarget();
		move();

	}

	public virtual void adjustStats(){

		if(stats.Count>0){
		for(int i=0; i<stats.Count;i++){

				stats[i][0] += (stats[i][1])*Time.deltaTime;
				if(stats[i][2] != 0){
					
					stats[i][0]+=stats[i][2];
					stats[i][2] = 0;
				}

			 if(stats[i][0]<0){
				stats[i][0] +=stats[i][0]*-1;
			}
			
		}
		}

	}
	public void OnCollisionEnter(Collision col){

		if(col.gameObject.name=="Terrain"){
			
		}


	}
	public void OnCollisionExit(Collision col){

		if(col.gameObject.name=="Terrain"){

		}

	}

	public virtual void move(){

	}
	 public virtual void getCurrentTarget(){
	}

	public virtual void OnGUI(){
	}

}

