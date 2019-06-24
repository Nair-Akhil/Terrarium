using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entitiy1 : MonoBehaviour {

	// Use this for initialization
	
	public Rigidbody rigidbody;

	void Start () {
		
		rigidbody = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
		
		float a = Mathf.PerlinNoise(Time.frameCount*0.005f,transform.position.x*0.005f);

		rigidbody.AddForce(transform.forward*10);
		rigidbody.rotation =  Quaternion.Euler(0,a*720,0);
		

	}
}
