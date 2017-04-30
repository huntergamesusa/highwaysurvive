using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[Serializable]

public class ScoresModel {
	public int killzombie = 100;
	public int killcar =250;
	public int coinpickup =25;

}

public class ScoringManager : MonoBehaviour {


	public static ScoresModel myScores  = new ScoresModel (); 
	public static Text ingamescore;
	public static Text distanceScoreTXT;
	public static Text coinsTXT;
	public static GameObject scoreInGameUI;
	public static int score;
	public static float distance;
	public static bool isGameOver;
	public GameOver myGameOverScript;
	// Use this for initialization




	void GameOverDelegate(bool isCurrentGameOver){
		isGameOver = isCurrentGameOver;

		if (isCurrentGameOver) {
			if (score > PlayerPrefs.GetInt ("HighScore")) {
				PlayerPrefs.SetInt ("HighScore", score);

			}
			myGameOverScript.GetScoringInfo(score,PlayerPrefs.GetInt ("HighScore"),PlayerPrefs.GetInt ("Coins"));

		}

	}

	void Awake () {
		GameOver.isGameOver += this.GameOverDelegate;

		scoreInGameUI = Resources.Load ("ScoreTextPrefab") as GameObject;
		ingamescore = GameObject.Find ("GameScore").GetComponent<Text>();
		distanceScoreTXT = GameObject.Find ("DistanceScore").GetComponent<Text>();
		coinsTXT = GameObject.Find ("CoinText").GetComponent<Text> ();


		distance = 0;
		score = 0;
		distanceScoreTXT.text = distance.ToString ("f0") + "M";
		coinsTXT.text = PlayerPrefs.GetInt ("Coins").ToString();
		ingamescore.text = score.ToString();

	}
	
	// Update is called once per frame
	public static void UpdateScore (int mainscore, int mult, Vector3 pos) {
		if (isGameOver)
			return;

			ingamescore.transform.localScale = new Vector3 (1, 1, 1);
			print (mainscore);
			score += (mainscore * mult);
			LeanTween.scale (ingamescore.GetComponent<RectTransform> (), new Vector2 (1.5f, 1.5f), .15f).setEaseInOutSine ();
			LeanTween.scale (ingamescore.GetComponent<RectTransform> (), new Vector2 (1f, 1f), .15f).setEaseOutSine ().setDelay (.15f);
			ingamescore.text = score.ToString ();

//		ingamescore.text = score.ToString("#000000000");

			if (pos != null) {
				GameObject myScoreObj = Instantiate (scoreInGameUI, pos, Quaternion.identity) as GameObject;
				myScoreObj.GetComponent<AnimatePointsObject> ().mypoints = mainscore * mult;
			}
		


	}

	public  void UpdateDistance (float mainscore) {
		if (isGameOver)
			return;
			
//		ingamescore.transform.localScale = new Vector3 (1, 1, 1);
		distance += mainscore;
//		LeanTween.scale (ingamescore.GetComponent<RectTransform> (), new Vector2 (1.35f, 1.35f), .15f).setEaseInOutSine ();
//		LeanTween.scale (ingamescore.GetComponent<RectTransform> (), new Vector2 (1f, 1f), .15f).setEaseOutSine ().setDelay(.15f);

		distanceScoreTXT.text = distance.ToString ("f0") + "M";


	}
	public static void UpdateCoins (int cc) {
		//		ingamescore.transform.localScale = new Vector3 (1, 1, 1);
		PlayerPrefs.SetInt ("Coins",PlayerPrefs.GetInt ("Coins")+cc);

		LeanTween.scale (coinsTXT.GetComponent<RectTransform> (), new Vector2 (1.5f, 1.5f), .15f).setEaseInOutSine ();
		LeanTween.scale (coinsTXT.GetComponent<RectTransform> (), new Vector2 (1f, 1f), .15f).setEaseOutSine ().setDelay(.15f);

		coinsTXT.text = PlayerPrefs.GetInt ("Coins").ToString();

		if (cc < 0) {
			GameObject.Find("GameOver").SendMessage("UpdateBarsAfterPurchase", PlayerPrefs.GetInt ("Coins"));
		}

	}

	public static void ResetScore () {
		score = 0;
		distance = 0;
		ingamescore.text = score.ToString();

	}

}
