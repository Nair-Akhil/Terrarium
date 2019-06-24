using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterdrop : EntityBeheviour, IAdvertise, IDrinkable {

	public float ad;
	public float sat;

	int extraframe = 5;
	int life = 5000;

	public float advertised(){
		return ad;
	}

	public float drink(){
		float t = sat;
		sat = 0;
		ad = 0;
		return t;
	}

	void update(){
		life -=1;
		if(sat == 0){
	        
			extraframe -=1;
		}
		if((extraframe < 0)||(life<0)){
			Destroy(gameObject);
		}
		
	}
	
}
