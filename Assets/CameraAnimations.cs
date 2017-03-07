using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimations : MonoBehaviour {
	public static Vector3 offset = new Vector3(0,-5,12);
	public static Vector3 menuPos;
	public static Vector3 menuRot = new Vector3(11.33f,0,0);
	public static GameObject gameLoc;
	public static Camera mainCam;
	public static GameObject target;
	// Use this for initialization
	void Awake () {
		target = GameObject.Find ("EmptyBody");
		mainCam = Camera.main;
		gameLoc = transform.GetChild (1).gameObject;
		ChangeCamera("menu");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Alpha1)){
			ChangeCamera("menu");
		}
		if(Input.GetKeyUp(KeyCode.Alpha2)){
			ChangeCamera("ingame");
		}
	}

	public static void ChangeCamera(string loc){
		gameLoc.transform.localPosition = new Vector3 (gameLoc.transform.localPosition.x, gameLoc.transform.localPosition.y, PlayerPrefs.GetFloat ("cameraZ"));
		switch (loc){
		case "menu":
			menuPos = new Vector3 (target.transform.position.x - offset.x, target.transform.position.y - offset.y, target.transform.position.z - offset.z);
			LeanTween.move (mainCam.gameObject, menuPos, .75f).setEaseInOutCubic ();
			LeanTween.rotate (mainCam.gameObject, menuRot, .75f).setEaseInOutCubic ();
			break;
		case "ingame":
		LeanTween.move (mainCam.gameObject, gameLoc.transform.position, .75f).setEaseInOutCubic();
			LeanTween.rotate (mainCam.gameObject, gameLoc.transform.eulerAngles, .75f).setEaseInOutCubic();
		break;

	}

	}

}
