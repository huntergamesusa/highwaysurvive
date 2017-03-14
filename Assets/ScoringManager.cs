using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[Serializable]

public class ScoresModel {
	public int killzombie = 100;
	public int killcar =100;
	public int coinpickup =25;

}

public class ScoringManager : MonoBehaviour {


	public static ScoresModel myScores  = new ScoresModel (); 
	public static Text ingamescore;
	public static Text distanceScoreTXT;
	public static Text coinsTXT;

	public static int score;
	public static float distance;

	// Use this for initialization




	void Awake () {
		

		ingamescore = GameObject.Find ("GameScore").GetComponent<Text>();
		distanceScoreTXT = GameObject.Find ("DistanceScore").GetComponent<Text>();
		coinsTXT = GameObject.Find ("CoinText").GetComponent<Text> ();


		distance = 0;
		score = 0;
		distanceScoreTXT.text = distance.ToString ("f0") + "M";
		coinsTXT.text = PlayerPrefs.GetInt ("Coins").ToString();
		ingamescore.text = score.ToString("#000000000");

	}
	
	// Update is called once per frame
	public static void UpdateScore (int mainscore, int mult) {
		ingamescore.transform.localScale = new Vector3 (1, 1, 1);
		print (mainscore);
		score += (mainscore * mult);
		LeanTween.scale (ingamescore.GetComponent<RectTransform> (), new Vector2 (1.35f, 1.35f), .15f).setEaseInOutSine ();
		LeanTween.scale (ingamescore.GetComponent<RectTransform> (), new Vector2 (1f, 1f), .15f).setEaseOutSine ().setDelay(.15f);

		ingamescore.text = score.ToString("#000000000");


	}

	public  void UpdateDistance (float mainscore) {
//		ingamescore.transform.localScale = new Vector3 (1, 1, 1);
		distance += mainscore;
//		LeanTween.scale (ingamescore.GetComponent<RectTransform> (), new Vector2 (1.35f, 1.35f), .15f).setEaseInOutSine ();
//		LeanTween.scale (ingamescore.GetComponent<RectTransform> (), new Vector2 (1f, 1f), .15f).setEaseOutSine ().setDelay(.15f);

		distanceScoreTXT.text = distance.ToString ("f0") + "M";


	}
	public static void UpdateCoins (int cc) {
		//		ingamescore.transform.localScale = new Vector3 (1, 1, 1);
		PlayerPrefs.SetInt ("Coins",PlayerPrefs.GetInt ("Coins")+cc);

		//		LeanTween.scale (ingamescore.GetComponent<RectTransform> (), new Vector2 (1.35f, 1.35f), .15f).setEaseInOutSine ();
		//		LeanTween.scale (ingamescore.GetComponent<RectTransform> (), new Vector2 (1f, 1f), .15f).setEaseOutSine ().setDelay(.15f);

		coinsTXT.text = PlayerPrefs.GetInt ("Coins").ToString();


	}

	public static void ResetScore () {
		score = 0;
		distance = 0;
		ingamescore.text = score.ToString("#000000000");

	}

}
