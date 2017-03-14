using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
//using UnionAssets.FLE;

#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
using UnityEngine;
#else
using UnityEngine.iOS;
#endif


public class TimeReward : MonoBehaviour {
	public DateTime unbiasedTimerEndTimestamp;
	public Text timeLeft;

	public GameObject earnVidButton;
	public GameObject winPrizeButton;
	public GameObject winGiftButton;

	public Text earnVidTxt;
	public Text winPrizeTxt;
	public Text winGiftTxt;

	public Image earnPanel;

	public GameObject FreeItemCam;
	public GameObject FreeItemCamUI;
	public GameObject FreeItem_UI;
	public GameObject characterName;
	public GameObject pointsGift;


	public GameObject playBttn;
	public GameObject closeBttn;
	public AudioClip punchSound;

	public GameObject freeGiftStart;

	private int lastNotificationId = 0;
	private int randomGiftAct;

	public GameObject inMenuShareBttn;
	public GameObject inMenuGiveAwayBttn;
	public Text actualPoints;
	public int inGamePoints;

	private int noScoremult;
	private int someScoremult;

	public GameObject isItNewGameObject;

	// Use this for initialization
	void Awake () {
		// Read PlayerPrefs to restore scheduled timers
		// By default initiliaze both timers in 60 seconds from now
//		IOSNativeUtility.SetApplicationBagesNumber(0);

		unbiasedTimerEndTimestamp = this.ReadTimestamp("unbiasedTimer", UnbiasedTime.Instance.Now().AddSeconds(60));
//		UM_NotificationController();

//		if(IOSNotificationController.Instance.LaunchNotification != null) {
//			ISN_LocalNotification notification = IOSNotificationController.Instance.LaunchNotification;
//			
//			IOSMessage.Create("Launch Notification", "Messgae: " + notification.Message + "\nNotification Data: " + notification.Data);
//		}


		checkGiftReady ();

		if (PlayerPrefs.GetInt ("GiftReady") > 0 && PlayerPrefs.GetInt ("Gifts") >0) {
			freeGiftStart.SetActive (true);

		} 
		else {
			freeGiftStart.SetActive (false);

		}
		inGamePoints = int.Parse (actualPoints.text);

	}

	void Start(){
		IOSNativeUtility.SetApplicationBagesNumber(0);
	}
	
	void OnApplicationPause (bool paused) {
		if (paused) {
			this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);
		}
		else {
			unbiasedTimerEndTimestamp = this.ReadTimestamp("unbiasedTimer", UnbiasedTime.Instance.Now().AddSeconds(60));
			IOSNativeUtility.SetApplicationBagesNumber(0);

			checkGiftReady();

			gameEndChangeButtons();
			checkTimeLeft();

			if (PlayerPrefs.GetInt ("GiftReady") > 0 && PlayerPrefs.GetInt ("Gifts") >0) {
				freeGiftStart.SetActive (true);
				
			} 
			else {
				freeGiftStart.SetActive (false);
				
			}

		}
	}
	
	void OnApplicationQuit () {
		this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);
	}




	public void gameEndChangeButtons(){

		if (PlayerPrefs.GetInt ("selectedLevel") < 2) {
			inGamePoints = int.Parse (actualPoints.text);
			if (inGamePoints > 0) {
				someScoremult++;
			} else {
				noScoremult++;
			}
		}

			if (PlayerPrefs.GetInt ("GiftReady") > 0) {
			earnPanel.enabled =true;
			earnVidTxt.enabled = false;
			earnVidButton.GetComponent<Image> ().enabled = false;
			earnVidButton.GetComponent<Button> ().enabled = false;
			winGiftTxt.enabled = true;
			winGiftButton.GetComponent<Image> ().enabled = true;
			winGiftButton.GetComponent<Button> ().enabled = true;
			winPrizeTxt.enabled = false;
			winPrizeButton.GetComponent<Image> ().enabled = false;
			winPrizeButton.GetComponent<Button> ().enabled = false;
		} 
		else {

			float randomEarnorWin = UnityEngine.Random.Range(0,100);

			if (randomEarnorWin > 50) {
				if(someScoremult>=1 ||noScoremult>=2){
					earnPanel.enabled =true;

					earnVidTxt.enabled = true;
					earnVidButton.GetComponent<Image> ().enabled = true;
					earnVidButton.GetComponent<Button> ().enabled = true;
					winGiftTxt.enabled = false;
					winGiftButton.GetComponent<Image> ().enabled = false;
					winGiftButton.GetComponent<Button> ().enabled = false;
					winPrizeTxt.enabled = false;
					winPrizeButton.GetComponent<Image> ().enabled = false;
					winPrizeButton.GetComponent<Button> ().enabled = false;

					someScoremult =0;
					noScoremult =0;
				}
				else{
	
				}
			} 
			//IF YOU HAVE NOTHING AVAILABLE
			else {
				if(someScoremult>=1 ||noScoremult>=2){
					if(PlayerPrefs.GetInt("myCredits")>=100){
						earnPanel.enabled =true;
						
						earnVidTxt.enabled = false;
						earnVidButton.GetComponent<Image> ().enabled = false;
						earnVidButton.GetComponent<Button> ().enabled = false;
						winGiftTxt.enabled = false;
						winGiftButton.GetComponent<Image> ().enabled = false;
						winGiftButton.GetComponent<Button> ().enabled = false;
						winPrizeTxt.enabled = true;
						winPrizeButton.GetComponent<Image> ().enabled = true;
						winPrizeButton.GetComponent<Button> ().enabled = true;
					
					}
				
				else{

					earnPanel.enabled =false;

					earnVidTxt.enabled = false;
					earnVidButton.GetComponent<Image> ().enabled = false;
					earnVidButton.GetComponent<Button> ().enabled = false;
					winGiftTxt.enabled = false;
					winGiftButton.GetComponent<Image> ().enabled = false;
					winGiftButton.GetComponent<Button> ().enabled = false;
					winPrizeTxt.enabled = false;
					winPrizeButton.GetComponent<Image> ().enabled = false;
					winPrizeButton.GetComponent<Button> ().enabled = false;
				
				}
				}
			}

		

		}

	if (PlayerPrefs.GetInt ("myCredits") < 100) {
			winPrizeTxt.enabled = false;
			winPrizeButton.GetComponent<Image> ().enabled = false;
			winPrizeButton.GetComponent<Button> ().enabled = false;
		}


	}



	void Update(){
//		if (Input.GetKeyUp (KeyCode.T)) {
//			StartCoroutine("giftExpensedResetTime");
////			 giftExpensedResetTime();
//
//		}
//
//		if(Input.GetKeyUp(KeyCode.O)){
//			UnbiasedTime.Instance.Now ().AddSeconds (5);
//
//		}

//		TimeSpan unbiasedRemaining = unbiasedTimerEndTimestamp - UnbiasedTime.Instance.Now();

//		timeLeft.text = string.Format("{0}:{1:D2}:{2:D2}", unbiasedRemaining.Hours, unbiasedRemaining.Minutes, unbiasedRemaining.Seconds);
	}


	void endGameTimeDifference(){
		if (PlayerPrefs.GetInt ("Gifts") > 0) {
	
		}

	}

	void checkTimeLeft(){
		TimeSpan unbiasedRemaining = unbiasedTimerEndTimestamp - UnbiasedTime.Instance.Now();
		Text timeRemainingTXT = GameObject.Find("TimeRemainingGift").GetComponent<Text>();
		if (unbiasedRemaining.Hours > 0) {

			String timeFormatted = string.Format("{0}:{1:D2}", unbiasedRemaining.Hours, (unbiasedRemaining.Minutes));


			timeRemainingTXT.text = SmartLocalization_Nate.giftInTrans+" " + timeFormatted;
		} 
		else {
			if(unbiasedRemaining.TotalHours>0){
				timeRemainingTXT.text = SmartLocalization_Nate.giftInTrans+" "  + (unbiasedRemaining.Minutes+1) + "M";
			}
		}
		if (PlayerPrefs.GetInt ("GiftReady") > 0) {
			timeRemainingTXT.text = "";
		}

	}

	void checkGiftReady(){

		TimeSpan unbiasedRemaining = unbiasedTimerEndTimestamp - UnbiasedTime.Instance.Now();


		if(PlayerPrefs.GetInt("Gifts")==0){
			//0 min
			PlayerPrefs.SetInt("GiftReady",1);
		}

		if (PlayerPrefs.GetInt ("Gifts") == 1) {
			//3 min
			if (unbiasedRemaining.TotalSeconds <= 0) {
				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}

		if(PlayerPrefs.GetInt("Gifts")==2){
			//6 min
			if (unbiasedRemaining.TotalSeconds <= 0) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==3){
			//30 min
			if (unbiasedRemaining.TotalSeconds <= 0) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==4){
			//1hr
			if (unbiasedRemaining.TotalSeconds <= 0) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==5){
			//2hr
			if (unbiasedRemaining.TotalSeconds <= 0) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==6){
			//3hr
			if (unbiasedRemaining.TotalSeconds <= 0) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==7){
			//4hr
			if (unbiasedRemaining.TotalSeconds <= 0) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==8){
			//5hr
			if (unbiasedRemaining.TotalSeconds <= 0) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
			
		}
		if(PlayerPrefs.GetInt("Gifts")>=9){
			//6hr
			if (unbiasedRemaining.TotalSeconds <= 0) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}

	}

	public IEnumerator giftExpensedResetTime(){


		TimeSpan unbiasedRemaining = unbiasedTimerEndTimestamp - UnbiasedTime.Instance.Now();



		FreeItemCam.SendMessage ("animateGUI");

//		if(PlayerPrefs.GetInt ("Gifts")>=1) {
//			IOSNotificationController.Instance.CancelAllLocalNotifications();
//		}

		if (PlayerPrefs.HasKey ("Gifts") != null) {
			PlayerPrefs.SetInt ("Gifts", PlayerPrefs.GetInt ("Gifts")+1);
		}

//		if(PlayerPrefs.GetInt("Gifts")==0){
//			//0 min
//			PlayerPrefs.SetInt("Gifts",1);
//		}
		if (PlayerPrefs.GetInt ("Gifts") == 1) {
				//3 min
			print ("wait 3 minutes");

			unbiasedTimerEndTimestamp = UnbiasedTime.Instance.Now().AddMinutes(3);
				this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);


		
			int NotificationId = UM_NotificationController.Instance.ScheduleLocalNotification("Buddy Toss",SmartLocalization_Nate.claimGiftTrans, 3*60);
 
//			PlayerPrefs.SetInt ("notificationID", lastNotificationId);
			}



		if(PlayerPrefs.GetInt("Gifts")==2 && unbiasedRemaining.TotalSeconds <= 0){
			//6 min
			print ("wait 6 minutes");

			unbiasedTimerEndTimestamp =  UnbiasedTime.Instance.Now().AddMinutes(6);
			this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);

			int NotificationId = UM_NotificationController.Instance.ScheduleLocalNotification("Buddy Toss",SmartLocalization_Nate.claimGiftTrans,6*60);

			
		}
		if(PlayerPrefs.GetInt("Gifts")==3 && unbiasedRemaining.TotalSeconds <= 0){
			//30 min
			print ("wait 30 minutes");

			unbiasedTimerEndTimestamp = UnbiasedTime.Instance.Now().AddMinutes(30);
			this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);

			int NotificationId = UM_NotificationController.Instance.ScheduleLocalNotification("Buddy Toss",SmartLocalization_Nate.claimGiftTrans, 30*60);

		}
		if(PlayerPrefs.GetInt("Gifts")==4 && unbiasedRemaining.TotalSeconds <= 0){
			//1hr
			print ("wait 1 hour");

			unbiasedTimerEndTimestamp =  UnbiasedTime.Instance.Now().AddMinutes(60);
			this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);

			int NotificationId = UM_NotificationController.Instance.ScheduleLocalNotification("Buddy Toss",SmartLocalization_Nate.claimGiftTrans, 60*60);

		}
		if(PlayerPrefs.GetInt("Gifts")==5 && unbiasedRemaining.TotalSeconds <= 0){
			//2hr
			print ("wait 2 hours");

			unbiasedTimerEndTimestamp =  UnbiasedTime.Instance.Now().AddMinutes(120);
			this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);

			int NotificationId = UM_NotificationController.Instance.ScheduleLocalNotification("Buddy Toss",SmartLocalization_Nate.claimGiftTrans, 120*60);

			
		}
		if(PlayerPrefs.GetInt("Gifts")==6 && unbiasedRemaining.TotalSeconds <= 0){
			//3hr
			print ("wait 3 hours");

			unbiasedTimerEndTimestamp =  UnbiasedTime.Instance.Now().AddMinutes(180);
			this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);

			int NotificationId = UM_NotificationController.Instance.ScheduleLocalNotification("Buddy Toss",SmartLocalization_Nate.claimGiftTrans,180*60);

		}
		if(PlayerPrefs.GetInt("Gifts")==7 && unbiasedRemaining.TotalSeconds <= 0){
			//4hr
			print ("wait 4 hours");

			unbiasedTimerEndTimestamp =  UnbiasedTime.Instance.Now().AddMinutes(240);
			this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);

			int NotificationId = UM_NotificationController.Instance.ScheduleLocalNotification("Buddy Toss",SmartLocalization_Nate.claimGiftTrans,240*60);

			
		}
		if(PlayerPrefs.GetInt("Gifts")==8 && unbiasedRemaining.TotalSeconds <= 0){
			//5hr
			print ("wait 5 hours");

			unbiasedTimerEndTimestamp =  UnbiasedTime.Instance.Now().AddMinutes(300);
			this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);

			int NotificationId = UM_NotificationController.Instance.ScheduleLocalNotification("Buddy Toss",SmartLocalization_Nate.claimGiftTrans, 300*60);

			
			
		}
		if(PlayerPrefs.GetInt("Gifts")>=9 && unbiasedRemaining.TotalSeconds <= 0){
			//6hr
			print ("wait 6 hours");
			unbiasedTimerEndTimestamp =  UnbiasedTime.Instance.Now().AddMinutes(360);
			this.WriteTimestamp("unbiasedTimer", unbiasedTimerEndTimestamp);

			int NotificationId = UM_NotificationController.Instance.ScheduleLocalNotification("Buddy Toss",SmartLocalization_Nate.claimGiftTrans, 360*60);

			
		}
		PlayerPrefs.SetInt ("GiftReady", 0);

//		currentDate = System.DateTime.Now;
		PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());



		FreeItemCam.GetComponent<Camera>().enabled=true;
		FreeItemCamUI.SetActive(true);
		FreeItem_UI.SetActive(true);
		characterName.SetActive (false);
		pointsGift.SetActive (true);
		pointsGift.transform.localScale = Vector3.zero;

		playBttn.transform.parent.GetComponent<Button>().enabled =false;
		playBttn.transform.parent.GetComponent<Image>().enabled =false;
		closeBttn.transform.parent.GetComponent<Button>().enabled =false;
		closeBttn.transform.parent.GetComponent<Image>().enabled =false;

		
		
		playBttn.GetComponent<Button>().enabled =true;
		playBttn.GetComponent<Image>().enabled =true;
		closeBttn.GetComponent<Button>().enabled =true;
		closeBttn.GetComponent<Image>().enabled =true;
		inMenuShareBttn.SetActive(false);
		inMenuGiveAwayBttn.SetActive(false);

		//needed incause game is paused
		freeGiftStart.SetActive (false);
		isItNewGameObject.SetActive (false);

//		yield return new WaitForSeconds (1);

		Text pointEffectGift = GameObject.Find("PointsGiftTXT").GetComponent<Text>();

		GameObject UISound = GameObject.Find ("UIButtonSounds");
		UISound.GetComponent<AudioSource> ().PlayOneShot (punchSound);

		 int randomGift = UnityEngine.Random.Range(0, 100);


		if(randomGift >97){
			randomGiftAct = UnityEngine.Random.Range (110,200);
			PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits") + randomGiftAct);
		}
		if(randomGift >86 &&randomGift <=97){
			randomGiftAct = UnityEngine.Random.Range (60,110);
			PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits") + randomGiftAct);

			}
		if(randomGift<=86){
			randomGiftAct = UnityEngine.Random.Range (40,60);
			PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits") + randomGiftAct);
			}


		Text credits = GameObject.Find("Credits UI").GetComponent<Text>();

		credits.text  = PlayerPrefs.GetInt("myCredits").ToString("f0");
		pointEffectGift.GetComponent<Text> ().text = randomGiftAct.ToString ("f0");

		LeanTween.scale(pointEffectGift.gameObject, new Vector3(1f,1f,1f), .7f).setEase(LeanTweenType.easeOutElastic);
//		LeanTween.scale(pointEffectGift.gameObject, new Vector3(0f,0f,0f), .2f).setEase(LeanTweenType.easeOutExpo).setDelay(.7f);

		//make sure time is displayed after receiving wared
		checkTimeLeft ();

		GameObject firework = GameObject.Find ("CoinRewardGift");

		yield return new WaitForSeconds(.25f);
		firework.GetComponent<ParticleSystem> ().Play ();

		for (int i = 0; i < 20; i++) {
			firework.GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(.075f);

		}

	}






	private DateTime ReadTimestamp (string key, DateTime defaultValue) {
		long tmp = Convert.ToInt64(PlayerPrefs.GetString(key, "0"));
		if ( tmp == 0 ) {
			return defaultValue;
		}
		return DateTime.FromBinary(tmp);
	}
	
	private void WriteTimestamp (string key, DateTime time) {
		PlayerPrefs.SetString(key, time.ToBinary().ToString());
	}






}



