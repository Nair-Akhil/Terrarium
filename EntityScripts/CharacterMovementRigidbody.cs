using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementRigidbody : MonoBehaviour {

	public GameObject cameraObject;

	public CharacterController cCont;
	public Rigidbody rigidbody;

	private Camera cam;

	private bool grounded;
	private float hSpeed;
	private int vSpeed;

	public Vector3 target;

	public Animator anim;

	public float speed = 10.0f;
	public float gravity = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
	void Start () {

		rigidbody = GetComponent<Rigidbody>();

		cCont = GetComponent<CharacterController>();

		cam = cameraObject.GetComponent<Camera>();

		anim = GetComponent<Animator>();

		grounded = false;

		hSpeed = vSpeed = 0;

		target = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			clicked();
		}


		moveDirection = Vector3.zero;
		

		if(Vector3.Distance(cCont.transform.position,target)>5){

			//moveDirection += Vector3.Normalize(cCont.transform.position-target)*-speed;
			Vector3 targetDirection = Vector3.Normalize(cCont.transform.position-target)*-1;

			targetDirection.y=0;


			if(new Vector2(rigidbody.velocity.x,rigidbody.velocity.z).magnitude<30){

				moveDirection=transform.forward*speed;
	
			}

			rigidbody.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.forward,targetDirection,0.02f));
			
			anim.SetBool("Moving",true);


		}else{

			anim.SetBool("Moving",false);

		}

		

		rigidbody.AddForce(moveDirection);
		

	}

	void OnCollisionEnter(Collision col){

		if(col.gameObject.name=="Terrain"){
			
			
			
		}


	}

	void OnCollisionExit(Collision col){

		if(col.gameObject.name=="Terrain"){

			

		}

	}

	void clicked(){

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit = new RaycastHit();

		if(Physics.Raycast(ray,out hit)){


			print("Hit");

			target=hit.point;

		}

		print(target);


	}

}
