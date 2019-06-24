using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum heat{warm,normal,cool};
public class Sun : MonoBehaviour {

	

	 


	 public static heat temperature;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float t = Mathf.PerlinNoise(Time.time*0.25f,Time.time*0.25f);
		print(temperature);
		//print(t);
		
		if(t>0.75){


			temperature = heat.warm;


		}else if(t<0.25){

			temperature = heat.cool;

		}else{

			temperature = heat.normal;
		}

	}
}
