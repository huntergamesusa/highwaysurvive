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
	public ParticleSystem myCoinExplode;
	public GameObject freeGiftStart;

	private int lastNotificationId = 0;
	private int randomGiftAct;




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
		if (!PlayerPrefs.HasKey ("Gifts")) {
			PlayerPrefs.SetInt ("Gifts", 0);
		}

		checkGiftReady ();

		if (PlayerPrefs.GetInt ("GiftReady") > 0 && PlayerPrefs.GetInt ("Gifts") >=0) {
			freeGiftStart.SetActive (true);

		} 
		else {
			freeGiftStart.SetActive (false);

		}

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

//			gameEndChangeButtons();
			checkTimeLeft();

			if (PlayerPrefs.GetInt ("GiftReady") > 0 && PlayerPrefs.GetInt ("Gifts") >=0) {
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

		if (unbiasedRemaining.Hours > 0) {

			String timeFormatted = string.Format("{0}:{1:D2}", unbiasedRemaining.Hours, (unbiasedRemaining.Minutes));


			timeLeft.text = SmartLocalization_Nate.giftInTrans+" " + timeFormatted;
		} 
		else {
			if(unbiasedRemaining.TotalHours>0){
				timeLeft.text = SmartLocalization_Nate.giftInTrans+" "  + (unbiasedRemaining.Minutes+1) + "M";
			}
		}
		if (PlayerPrefs.GetInt ("GiftReady") > 0) {
			timeLeft.text = "";
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

	public void giftExpensedResetTime(){


		TimeSpan unbiasedRemaining = unbiasedTimerEndTimestamp - UnbiasedTime.Instance.Now();


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



		//needed incause game is paused
		freeGiftStart.SetActive (false);

		myCoinExplode.Play ();

//		GameObject UISound = GameObject.Find ("UIButtonSounds");
//		UISound.GetComponent<AudioSource> ().PlayOneShot (punchSound);

		 int randomGift = UnityEngine.Random.Range(0, 100);


		if(randomGift >97){
			randomGiftAct = UnityEngine.Random.Range (110,200);
		}
		if(randomGift >86 &&randomGift <=97){
			randomGiftAct = UnityEngine.Random.Range (60,110);

			}
		if(randomGift<=86){
			randomGiftAct = UnityEngine.Random.Range (40,60);
			}
		ScoringManager.UpdateCoins (randomGiftAct);

		//make sure time is displayed after receiving wared
		checkTimeLeft ();


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



