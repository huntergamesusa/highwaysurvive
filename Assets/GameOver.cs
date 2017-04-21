using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOver : MonoBehaviour {
	[Header("Gameover Text UI")]

	public TextMeshProUGUI gameOverFinalScoreTxt;
	public TextMeshProUGUI highScoreTxt;
	public Text coinsToGo;
	public Text coinsForPrize;


	List <GameObject> myBars = new List<GameObject>();

	[Header("Gameover bars")]

	public GameObject coinsToGoBar;
	public GameObject winAPrizeBar;
	public GameObject earnCoinsBar;
	public GameObject freeGiftBar;
	public GameObject rateUsBar;

	[Header("Gameover layouts")]
	public GameObject dimmer;
	public GameObject gameOverTop;
	public GameObject gameOverMiddle;
	public GameObject gameOverBottom;

	Vector3 gameOverTopPos;
	Vector3 gameOverMiddlePos;
	Vector2 gameOverBottomPos;

	[Header("Continue Objects")]
	public GameObject parentContinue;
	public GameObject continueTimer;

	[Header("Other Scripts")]
	public GameState myGameState;

	int timeRemaining;

	public delegate void gameOverEventHandler (bool isCurrentGameOver);
	public static event gameOverEventHandler isGameOver;

	// Use this for initialization
	void Awake () {
		timeRemaining = 4;
		gameOverTopPos = gameOverTop.transform.localPosition;
		gameOverMiddlePos = gameOverMiddle.transform.localPosition;
		gameOverBottomPos = gameOverBottom.GetComponent<RectTransform> ().anchoredPosition;

		myBars.Add (coinsToGoBar);
		myBars.Add (winAPrizeBar);
		myBars.Add (earnCoinsBar);
		myBars.Add (freeGiftBar);
		myBars.Add (rateUsBar);


		ToggleActiveParent (false);
	}

	public void RestartingGame(){

		isGameOver (false);

	}

	public IEnumerator GameEnded(){
		PlayerPrefs.SetInt ("Plays", PlayerPrefs.GetInt ("Plays") + 1);

		isGameOver (true);
		myGameState.ToggleControls (false);
		myGameState.OverridePutBackPowerup ();
		var randomChance = Random.Range (0, 100);
		print (randomChance);
		if (randomChance >= 50) {
			yield return new WaitForSecondsRealtime(1);

			InitTimer();
		} else {
			yield return new WaitForSecondsRealtime(2);

			AnimateGameOver ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.G)){
			AnimateGameOver ();
//			InitTimer();
		}
	}

	void InitTimer(){
		timeRemaining = 6;
		parentContinue.SetActive(true);
			
		AnimateTime ();
	}

	void AnimateTime(){
		if (timeRemaining > 1) {
			timeRemaining--;
			continueTimer.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
			continueTimer.GetComponent<TextMeshProUGUI> ().text = timeRemaining.ToString ();
			LeanTween.scale (continueTimer.GetComponent<RectTransform> (), new Vector3 (1f, 1f, 1f), 1).setEaseInOutSine ().setOnComplete (AnimateTime);
		} else {
			AnimateGameOver ();
		}
	}

	public void TurnOffContinue(){
		parentContinue.SetActive(false);
		timeRemaining = 6;
		LeanTween.cancel (gameObject);
		LeanTween.cancel (continueTimer.GetComponent<RectTransform> ());

	}

	public void AnimateGameOver(){
		parentContinue.SetActive(false);
		ToggleActiveParent (true);
		dimmer.GetComponent<Image> ().color = new Color (0, 0, 0, 0);
		LeanTween.alpha(dimmer.GetComponent<RectTransform>(), 0.39f, .3f);

		LeanTween.move(gameOverTop.GetComponent<RectTransform>(),gameOverTopPos,.3f).setEaseOutElastic();
		LeanTween.move(gameOverMiddle.GetComponent<RectTransform>(),gameOverMiddlePos,.3f).setEaseOutElastic();
		LeanTween.moveY(gameOverBottom.GetComponent<RectTransform>(),gameOverBottomPos.y,.3f).setEaseOutElastic();

	}

			public void ToggleActiveParent(bool act){
		dimmer.SetActive (act);
		gameOverTop.SetActive (act);
		gameOverMiddle.SetActive (act);
		gameOverBottom.SetActive(act);
		if (!act) {
			gameOverTop.transform.localPosition = new Vector3 (-1000, gameOverTop.transform.localPosition.y, gameOverTop.transform.localPosition.z);
			gameOverMiddle.transform.localPosition = new Vector3 (1000, gameOverMiddle.transform.localPosition.y, gameOverMiddle.transform.localPosition.z);
			gameOverBottom.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -250);


		} 

			}

	public void GetCoinInfo(int coins){
		if (coins < 100) {
			coinsToGoBar.SetActive (true);
			winAPrizeBar.SetActive (false);
			coinsToGo.text = (100-coins).ToString();

		} else {
			winAPrizeBar.SetActive (true);

			coinsToGoBar.SetActive (false);
		}
	}

	public void GetScoringInfo(int score, int highscore, int coins){

		gameOverFinalScoreTxt.text = score.ToString();
		highScoreTxt.text = "BEST "+highscore.ToString();
		GetCoinInfo (coins);

		if (PlayerPrefs.GetInt ("GiftReady") > 0 && PlayerPrefs.GetInt ("Gifts") >=0) {
			freeGiftBar.SetActive (true);

		} 
		else {
			freeGiftBar.SetActive (false);

		}

		CheckOnRateBar ();

		coinsForPrize.text ="100";

	}

	void CheckOnRateBar(){
		if (PlayerPrefs.GetInt ("Plays") > 5) {
			int myInt = Random.Range (0, 100);

			if (myInt > 80) {
				if (!PlayerPrefs.HasKey ("RateGame")) {
					int myCount = 0;
					for (int i = 0; i < myBars.Count; i++) {
						if (myBars [i].activeInHierarchy) {
							myCount++;
						}
					}
					if (myCount < 4) {
						rateUsBar.SetActive (true);
					}
				}
			} else {
				rateUsBar.SetActive (false);

			}
		}
	}
}
