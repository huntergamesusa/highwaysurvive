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
	public static int score;
	// Use this for initialization




	void Awake () {
		

		ingamescore = GameObject.Find ("GameScore").GetComponent<Text>();
		score = 0;
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
	public static void ResetScore () {
		score = 0;
		ingamescore.text = score.ToString("#000000000");

	}

}
