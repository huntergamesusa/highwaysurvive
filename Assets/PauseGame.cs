using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	float mytimeScale;
	// Use this for initialization
	void Start () {
		mytimeScale = Time.timeScale;
	}
	
	// Update is called once per frame
	public void PauseMyGame (bool pauseme) {
		if(pauseme==false){
			Time.timeScale=mytimeScale;
		}
		else{
			Time.timeScale=0;

		}
	}
}
