using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenemanager : MonoBehaviour {

	// Use this for initialization
	Scene scene;
	public int restarttime = 10;
	void Start () {
		//scene = SceneManager.GetActiveScene();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.frameCount%(restarttime*60) == 0){
			 SceneManager.LoadScene("terrain");
		}
		
	}
}
