using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class TreeGenerator : MonoBehaviour {

	public Mesh primitive;

	ArrayList branches;
	
	// Use this for initialization
	void Start () {
		
		GameObject trunk;
		trunk = new GameObject("trunk");
		trunk.AddComponent<MeshFilter>();
		trunk.AddComponent<MeshRenderer>();

		trunk.GetComponent<MeshFilter>().mesh = primitive;
		trunk.transform.position = new Vector3(0,0,0);
		trunk.transform.parent = this.transform;
		branches.Add(primitive);
		
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
