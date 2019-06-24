/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	//an array of all other entities in scene
	private GameObject[] otherEntities;

	//hashtable has vectors of current stat values, stat decays, and change in stat value through means other than regular decay
	List<float[]> stats;


	//character controller for entity movement
	public CharacterController characterController;

	private bool grounded;
	private float hSpeed;
	private int vSpeed;

	private Vector3 targetPosition;
	
	public Animator animator;

	public float speed = 8.0f;

	public float gravity = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
	
	void Start () {

		characterController = GetComponent<CharacterController>();

		animator = GetComponent<Animator>();

		grounded = false;

		hSpeed = 

		otherEntities = GameObject.FindGameObjectsWithTag("Entity");
		print(otherEntities.Length);

	}
	
	// Update is called once per frame
	void Update () {

		if (hunger<=80){

			float d =10000;

			int index = -1;

			
			for(int i=0; i<foodObjects.Length;i++){

				if(foodObjects[i]){
				float cd = Vector3.Distance(transform.position,foodObjects[i].GetComponent<Transform>().position);
				if ((cd)<d){
					d = cd;
					index = i;
					GetComponent<CharacterMovement>().target = foodObjects[i].GetComponent<Transform>().position;

				}

				}

			}

			if(d<5){


				hunger += foodObjects[index].GetComponent<Food>().satisfaction;
				Destroy(foodObjects[index]);
				foodObjects = GameObject.FindGameObjectsWithTag("Food");

			}


		}
	}

	void updateStats(){

		for(int i=0; i<stats.Count;i++){

			stats[i][0] += stats[i][1]+stats[i][2];

		}

	}

	void move(){

	}

	void OnGUI(){

	}
}
*/