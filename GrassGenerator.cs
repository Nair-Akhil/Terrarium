using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GrassGenerator : MonoBehaviour {

	// Use this for initialization


	public GameObject grassPlane;

	public int nPlanes;

	GameObject[] grassPlanes;

	void Start () {
		
		grassPlanes = new GameObject[nPlanes];

		for(int i=0; i<nPlanes;i++){

			grassPlanes[i] = Instantiate(grassPlane, transform.position + (new Vector3(Random.RandomRange(-1f,1f),0,Random.Range(-1f,1f))),transform.rotation);
			grassPlanes[i].GetComponentInChildren<MeshFilter>().transform.RotateAroundLocal(new Vector3(0,1,0),Random.Range(-90,90)) ;
			grassPlanes[i].transform.parent = gameObject.transform;
		}
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
