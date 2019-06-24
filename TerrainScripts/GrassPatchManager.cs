using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrassPatchManager : MonoBehaviour {

    public int n;

    public GameObject plane;

    public GameObject grassPatch;

    public GameObject[] grassPatches;

	// Use this for initialization
	void Start () {

        DynamicTerrain p = plane.GetComponent<DynamicTerrain>();

        grassPatches = new GameObject[n];


        float d = p.dim * p.quadDim*0.5f*p.transform.localScale.x-grassPatch.GetComponent<GrassPatchGeneration>().dist;

        for(int i=0; i< grassPatches.Length; i++)
        {

            
           
            grassPatches[i] = Instantiate(grassPatch, new Vector3(Random.Range(-d, d), Random.Range(-d, d), Random.Range(-d, d)),new Quaternion(0,0,0,-1) );
            grassPatches[i].GetComponent<GrassPatchGeneration>().dist = 40;
            grassPatches[i].transform.parent = transform;

        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
