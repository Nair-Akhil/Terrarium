using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrassPatchGeneration : MonoBehaviour {

    public float dist;
    public int num;

    public GameObject grassBlade;

    private GameObject[] grassBlades;

	// Use this for initialization
	void Start () {
        
        grassBlades = new GameObject[num];
        
        Vector3 pos = new Vector3();


        for(int i=0; i < grassBlades.Length; i++)
        {

            float r = (Mathf.Sin(Random.Range(-Mathf.PI*0.5f,Mathf.PI*(3/4)))*dist)+Random.Range(1,20);
            float t = Random.Range(0,Mathf.PI*2);
            pos.Set(transform.position.x+r*Mathf.Cos(t),transform.position.y,transform.position.z+r*Mathf.Sin(t));
            grassBlades[i] = Instantiate(grassBlade, pos, Quaternion.EulerAngles(new Vector3(0, Random.Range(0, 360), 0)));
            grassBlades[i].transform.parent = transform;

        }
		
	}
	
	// Update is called once per frame
	void Update () {

        //transform.Rotate(new Vector3(0, 0.5f, 0));
        

    }
}
