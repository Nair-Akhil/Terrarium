using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	// Use this for initialization

	public Mesh rock1;
	public Mesh rock2;
	public Mesh rock3;
	public Mesh rock4;

	MeshFilter mFilter;
	MeshCollider mCollider;


	public bool s = true;
	private Mesh[] rocks;

	public float scale = 10;



	void Start () {

		if(s){
		RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            //print("hit down");
            //Debug.DrawLine(transform.position, hit.point);
            transform.position = hit.point;

        }
		}
		
		transform.localScale = new Vector3(scale*Random.Range(2,10),scale*Random.Range(2,10),scale*Random.Range(2,10));

		rocks = new Mesh[4];
		rocks[0] = rock1;
		rocks[1] = rock2;
		rocks[2] = rock3;
		rocks[3] = rock4;

		mFilter = GetComponent<MeshFilter>();
		//mCollider = GetComponent<MeshCollider>();

		int i = Random.Range(0,4);

		mFilter.mesh = rocks[i];
		//mCollider.sharedMesh = rocks[i];

		



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
