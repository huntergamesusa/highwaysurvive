using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSettings : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		SetDefaults ();
	}
	void SetDefaults(){
		if (!PlayerPrefs.HasKey ("animspeed")) {
			PlayerPrefs.SetFloat ("animspeed", 1.25f);
		}
		if (!PlayerPrefs.HasKey ("carspeed")) {
			PlayerPrefs.SetFloat ("carspeed", 15f);
		}
		if (!PlayerPrefs.HasKey ("MaxCarTime")) {
			PlayerPrefs.SetFloat ("MaxCarTime", 3f);
		}
		if (!PlayerPrefs.HasKey ("minzombiespeed")) {
			PlayerPrefs.SetFloat ("minzombiespeed", 3f);
		}
		if (!PlayerPrefs.HasKey ("maxzombiespeed")) {
			PlayerPrefs.SetFloat ("maxzombiespeed", 7f);
		}
		if (!PlayerPrefs.HasKey ("cameraZ")) {
			PlayerPrefs.SetFloat ("cameraZ", -7.5f);
		}
		if (!PlayerPrefs.HasKey ("powerupspread")) {
			PlayerPrefs.SetInt ("powerupspread", 20);
		}

	}

	public void SetAnimSpeed (string input){
		PlayerPrefs.SetFloat ("animspeed", float.Parse(input));
	}
	public void SetCarpeed (string input){
		PlayerPrefs.SetFloat ("carspeed", float.Parse(input));
	}
	public void SetMaxCarTime (string input){
		PlayerPrefs.SetFloat ("MaxCarTime", float.Parse(input));
	}
	public void Setminzombiespeed(string input){
		PlayerPrefs.SetFloat ("minzombiespeed", float.Parse(input));
	}
	public void Setmaxzombiespeed (string input){
		PlayerPrefs.SetFloat ("maxzombiespeed", float.Parse(input));
	}

	public void SetCamera(string distance){

		PlayerPrefs.SetFloat ("cameraZ", float.Parse(distance));
		GameObject.Find ("CameraPositions").SendMessage ("ChangeCamera", "ingame");
	}
	public void SetPowerupSpread(string distance){

		PlayerPrefs.SetInt ("powerupspread", int.Parse(distance));
	}

	public void ResetPrefs(){
		PlayerPrefs.DeleteAll ();
		SetDefaults ();
	}

}
