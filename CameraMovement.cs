using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	Camera camera;

	Rigidbody rigidbody;
	public Transform lookTarget;
	public Transform rotationTarget;
	List<GameObject> entities;
	// Use this for initialization
	void Start () {
		
		camera = GetComponent<Camera>();
		
		rigidbody = GetComponent<Rigidbody>();
		

	}
	
	// Update is called once per frame
	void Update () {

		camera.transform.LookAt(lookTarget);
		transform.LookAt(rotationTarget);
		transform.position += transform.right;

	}
	
}
