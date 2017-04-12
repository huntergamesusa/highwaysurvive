using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonAnimations : MonoBehaviour {
	public RectTransform [] myBttns;
	Vector2 startPos;
	bool flip;
	public bool bottomUp;
	// Use this for initialization

	void Start(){
		startPos = myBttns [0].anchoredPosition;

	}

	public void ResetAnimation(){
		for (int i = 0; i < myBttns.Length; i++) {
			myBttns [i].anchoredPosition = startPos;
			myBttns [i].gameObject.SetActive (false);

		}
		flip = false;

	}

	public void Animate(float distance){
		
		if (!flip) {
			flip = true;
			if (bottomUp) {
				distance = distance * 1;
			} else {
				distance = distance * -1;

			}
		} else {
			flip = false;
			if (bottomUp) {
				distance = distance * -1;
			} else {
				distance = distance * 1;

			}
		}
		for (int i = 0; i < myBttns.Length; i++) {
			myBttns [i].gameObject.SetActive (true);
			if (flip) {
				
				LeanTween.moveY (myBttns [i], myBttns [i].anchoredPosition.y + (distance * (i + 1)), .5f).setEaseInOutQuart ();
			} else {
				if (i == myBttns.Length - 1) {
					LeanTween.moveY (myBttns [i], myBttns [i].anchoredPosition.y + (distance * (i + 1)), .5f).setEaseInOutQuart ().setOnComplete(TurnOff);

				} else {
					LeanTween.moveY (myBttns [i], myBttns [i].anchoredPosition.y + (distance * (i + 1)), .5f).setEaseInOutQuart ();

				}
			}
		}
	}

	void TurnOff(){
		for (int i = 0; i < myBttns.Length; i++) {
			myBttns [i].gameObject.SetActive (false);

		}
	}
}
