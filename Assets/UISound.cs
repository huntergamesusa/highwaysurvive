using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UISound : MonoBehaviour, IPointerDownHandler{

	GameObject myClipGO;

	void Awake(){
		myClipGO = GameObject.Find ("UISoundGO");
	}
		public void OnPointerDown (PointerEventData eventData) 
		{

		myClipGO.GetComponent<AudioSource> ().Play ();
		}

}
