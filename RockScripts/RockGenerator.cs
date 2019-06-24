using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour {

	// Use this for initialization

	public GameObject Rock;

	public int nRocks;

	float d;

	void Start () {

		d = GameObject.Find("Ground").GetComponent<DynamicTerrain>().dimensions*0.5f;
		
		d = 25*0.5f*16;

		for(int i=0; i<nRocks;i++){
			
			
			Vector3 p = new Vector3(Random.Range(-d,d),this.transform.position.y,Random.Range(-d,d));
			GameObject r = Instantiate(Rock,p,this.transform.rotation);
			r.transform.parent = this.transform;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
