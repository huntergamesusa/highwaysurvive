using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColliders : MonoBehaviour {
	GameObject blocker;
	GameObject loadedblocker;
	GameObject target;
	Vector2 asphault = new Vector2(0,9.953932f);
	Vector2 camheight ;
	float dist ;
	float aspectRatioRot;
	float aspectRatioPos;
	Vector3 pos;

	// Use this for initialization
	void Awake () {
		CreateBlocker (1);
		CreateBlocker (-1);

	}
	
	void CreateBlocker(int side){
		aspectRatioRot = ((Camera.main.aspect/.6666667f)*-14.14f)*side;

		camheight = new Vector2 (transform.position.y, transform.position.z);
		dist = Vector2.Distance (asphault, camheight);
		print (dist);
		target = GameObject.Find ("EmptyBody");
		blocker = Resources.Load ("Blocker")as GameObject;
		loadedblocker = Instantiate (blocker, transform.position, Quaternion.identity);
		loadedblocker.transform.eulerAngles = new Vector3 (loadedblocker.transform.eulerAngles.x, aspectRatioRot, loadedblocker.transform.eulerAngles.z);
		if (side == 1) {
			pos = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, dist));
			loadedblocker.transform.position = new Vector3( pos.x,5f,5f);
		}
		if (side == -1) {
			pos = Camera.main.ScreenToWorldPoint (new Vector3 (1,0 , dist));
			loadedblocker.transform.position = new Vector3( pos.x*-1,5f,5f);


		}

		loadedblocker.transform.parent = transform;

	}
}
