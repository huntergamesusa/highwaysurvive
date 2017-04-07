using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AnimatePointsObject : MonoBehaviour {
	TextMeshPro myTextMesh;
	public int mypoints;
	// Use this for initialization
	void Start () {
		myTextMesh = GetComponent<TextMeshPro> ();
		myTextMesh.text = mypoints.ToString ();
		transform.position = new Vector3 (transform.position.x, 5.2f, transform.position.z);
//		LeanTween.move (ingamescore.GetComponent<RectTransform> (), new Vector2 (1.35f, 1.35f), .15f).setEaseInOutSine ();
		LeanTween.moveY(GetComponent<RectTransform>(),40,1.5f).setEaseInCirc();
		LeanTween.value (gameObject, updateValueExampleCallback, 1, 0, 1.5f).setOnComplete (KillGO);;

	}



	void KillGO(){
//		Destroy (gameObject);

	}
	void updateValueExampleCallback( float val ){
//		myTextMesh.color = new Color (myTextMesh.color.r, myTextMesh.color.g, myTextMesh.color.b, val);
		myTextMesh.alpha = val;

	}
}
