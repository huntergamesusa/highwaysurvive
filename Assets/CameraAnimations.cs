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

	public static GameObject CamParent;

	// Use this for initialization
	void Awake () {
		target = GameObject.Find ("EmptyBody");
		mainCam = Camera.main;
		gameLoc = transform.GetChild (1).gameObject;
		CamParent = new GameObject ();
		CamParent.transform.position = target.transform.position;
		CamParent.transform.eulerAngles = new Vector3(0,0,0);

		ChangeCamera("start");
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

		Collider[] allChildren = mainCam.transform.GetComponentsInChildren<Collider> ();
		foreach (Collider child in allChildren) {
			// do what you want with the rigidbody
			child.enabled = false;
		}

		mainCam.transform.parent = null;
		gameLoc.transform.localPosition = new Vector3 (gameLoc.transform.localPosition.x, gameLoc.transform.localPosition.y, PlayerPrefs.GetFloat ("cameraZ"));
		switch (loc){

		case "start":
			mainCam.transform.parent = CamParent.transform;
			menuPos = new Vector3 (target.transform.position.x - offset.x, target.transform.position.y - offset.y, target.transform.position.z - offset.z);
			LeanTween.move (mainCam.gameObject, menuPos, .75f).setEaseInOutCubic ().setOnComplete(RotateAroundCharacter);
			LeanTween.rotate (mainCam.gameObject, menuRot, .75f).setEaseInOutCubic ();
			break;

		case "menu":
			
			menuPos = new Vector3 (target.transform.position.x - offset.x, target.transform.position.y - offset.y, target.transform.position.z - offset.z);
			LeanTween.move (mainCam.gameObject, menuPos, .75f).setEaseInOutCubic ();
			LeanTween.rotate (mainCam.gameObject, menuRot, .75f).setEaseInOutCubic ();
			break;
		case "ingame":
		LeanTween.move (mainCam.gameObject, gameLoc.transform.position, .75f).setEaseInOutCubic();
			LeanTween.rotate (mainCam.gameObject, gameLoc.transform.eulerAngles, .75f).setEaseInOutCubic().setOnComplete(EnableColliders);
		break;

	}

	}

	public static void RotateAroundCharacter(){
		LeanTween.rotateAround(CamParent, Vector3.up, 360,10).setLoopClamp ();
	}

	public static void EnableColliders(){
		Collider[] allChildren = mainCam.transform.GetComponentsInChildren<Collider> ();
		foreach (Collider child in allChildren) {
			// do what you want with the rigidbody
			child.enabled = true;
		}
	}

}
