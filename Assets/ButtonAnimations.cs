using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnimations : MonoBehaviour,  IPointerExitHandler, IPointerEnterHandler {
	public RectTransform [] myBttns;
	public GameObject dimmer;
	Vector2 startPos;
	bool flip;
	public bool bottomUp;

	bool hasFocus;
	// Use this for initialization

	void Start(){
		startPos = myBttns [0].anchoredPosition;

	}

//	public void ResetAnimation(){
//		for (int i = 0; i < myBttns.Length; i++) {
//			myBttns [i].anchoredPosition = startPos;
//			myBttns [i].gameObject.SetActive (false);
//
//		}
//		flip = false;
//
//	}

	public void Animate(float distance){
		
		if (!flip) {
			flip = true;
			if (bottomUp) {
				distance = distance * 1;
				dimmer.SetActive (true);
				LeanTween.alpha(dimmer.GetComponent<RectTransform>(), 0.39f, .3f);

			} else {
				distance = distance * -1;
				dimmer.SetActive (true);
				LeanTween.alpha(dimmer.GetComponent<RectTransform>(), 0.39f, .3f);


			}
		} else {
			flip = false;
			if (bottomUp) {
				distance = distance * -1;
				LeanTween.alpha(dimmer.GetComponent<RectTransform>(), 0f, .3f);
			} else {
				distance = distance * 1;
				LeanTween.alpha(dimmer.GetComponent<RectTransform>(), 0f, .3f);

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

	public void AnimateClosed(float distance){

		if (!flip) {

		} else {
			if (bottomUp) {
				distance = distance * -1;
				LeanTween.alpha(dimmer.GetComponent<RectTransform>(), 0f, .3f);
			} else {
				distance = distance * 1;
				LeanTween.alpha(dimmer.GetComponent<RectTransform>(), 0f, .3f);

			}
		}
		for (int i = 0; i < myBttns.Length; i++) {
			myBttns [i].gameObject.SetActive (true);
			if (flip) {

				LeanTween.moveY (myBttns [i], myBttns [i].anchoredPosition.y + (distance * (i + 1)), .5f).setEaseInOutQuart ().setOnComplete(TurnOff);
			} else {
				
			}
		}
		flip = false;
	}


	void TurnOff(){
		for (int i = 0; i < myBttns.Length; i++) {
			myBttns [i].gameObject.SetActive (false);

		}
		dimmer.SetActive (false);

	}

	void Update(){
		if (Input.GetMouseButtonUp (0)) {
			if (!hasFocus) {
				hasFocus = true;
				AnimateClosed (75);
			}

		}


	}

	public void OnPointerExit(PointerEventData eventData) {
		hasFocus = false;  

	}

	public void OnPointerEnter(PointerEventData eventData) {
		hasFocus = true;               
	}




}
