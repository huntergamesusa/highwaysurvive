using UnityEngine;
using System.Collections;

public class UIController_SJM : MonoBehaviour {



	public RectTransform[] MainMenuRects;
	public RectTransform[] PauseMenuRects;
	public RectTransform[] SettingsMenuRects;
	public RectTransform[] VehicleMenuRects;
	public RectTransform[] CoinsMenuRects;
	public RectTransform[] GameOverMenuRects;
	public RectTransform[] InGameMenuRects;
	public float AnimTime;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Q)){
			SlideMainMenuIn ();
		}
		if(Input.GetKeyDown(KeyCode.W)){
			SlideMainMenuOut ();
		}
		if(Input.GetKeyDown(KeyCode.E)){
			SlidePauseMenuIn ();
		}
		if(Input.GetKeyDown(KeyCode.R)){
			SlidePauseMenuOut ();
		}
		if(Input.GetKeyDown(KeyCode.T)){
			SlideSettingsMenuIn ();
		}
		if(Input.GetKeyDown(KeyCode.Y)){
			SlideSettingsMenuOut ();
		}
		if(Input.GetKeyDown(KeyCode.U)){
			SlideVehicleMenuIn ();
		}
		if(Input.GetKeyDown(KeyCode.I)){
			SlideVehicleMenuOut ();
		}
		if(Input.GetKeyDown(KeyCode.O)){
			SlideCoinsMenuIn ();
		}
		if(Input.GetKeyDown(KeyCode.P)){
			SlideCoinsMenuOut ();
		}
		if(Input.GetKeyDown(KeyCode.A)){
			SlideGameOverMenuIn ();
		}
		if(Input.GetKeyDown(KeyCode.S)){
			SlideGameOverMenuOut ();
		}		
		if(Input.GetKeyDown(KeyCode.D)){
			SlideInGameMenuIn ();
		}
		if(Input.GetKeyDown(KeyCode.F)){
			SlideInGameMenuOut ();
		}

	}

	public void SlideMainMenuIn () {
		LeanTween.move(MainMenuRects[0], new Vector3(0f,0f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(MainMenuRects[1], new Vector3(0f,83f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(MainMenuRects[2], new Vector3(0f,-272f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(MainMenuRects[3], new Vector3(80f,60f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(MainMenuRects[4], new Vector3(0f,60f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(MainMenuRects[5], new Vector3(-80f,60f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );

	}
	public void SlideMainMenuOut () {
		LeanTween.move(MainMenuRects[0], new Vector3(0f,200f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(MainMenuRects[1], new Vector3(0f,1000f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(MainMenuRects[2], new Vector3(0f,-1000f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(MainMenuRects[3], new Vector3(80f,-100f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(MainMenuRects[4], new Vector3(0f,-100f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(MainMenuRects[5], new Vector3(-80f,-100f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
	}

	public void SlidePauseMenuIn () {
		LeanTween.move(PauseMenuRects[0], new Vector3(0f,57f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(PauseMenuRects[1], new Vector3(-150f,-175f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(PauseMenuRects[2], new Vector3(0f,-175f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(PauseMenuRects[3], new Vector3(150f,-175f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
	}
	public void SlidePauseMenuOut () {
		LeanTween.move(PauseMenuRects[0], new Vector3(0f,1000f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(PauseMenuRects[1], new Vector3(-150f,-1000f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(PauseMenuRects[2], new Vector3(0f,-1000f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(PauseMenuRects[3], new Vector3(150f,-1000f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
	}
	public void SlideSettingsMenuIn () {
		LeanTween.move(SettingsMenuRects[0], new Vector3(0f,0f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );

	}
	public void SlideSettingsMenuOut () {
		LeanTween.move(SettingsMenuRects[0], new Vector3(-Screen.width*2,0f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		
	}
	public void SlideVehicleMenuIn () {
		LeanTween.move(VehicleMenuRects[0], new Vector3(20f,-175f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(VehicleMenuRects[1], new Vector3(0f,0f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(VehicleMenuRects[2], new Vector3(0f,0f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );

	}
	public void SlideVehicleMenuOut () {
		LeanTween.move(VehicleMenuRects[0], new Vector3(-Screen.width*2,-175f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(VehicleMenuRects[1], new Vector3(0f,1000f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(VehicleMenuRects[2], new Vector3(0f,-1000f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
	}
	public void SlideCoinsMenuIn () {
		LeanTween.move(CoinsMenuRects[0], new Vector3(20f,-175f,0f), AnimTime).setEase( LeanTweenType.easeOutSine ).setDelay(0.6f);
		LeanTween.move(CoinsMenuRects[1], new Vector3(0f,0f,0f), AnimTime).setEase( LeanTweenType.easeOutSine ).setDelay(0.5f);
		LeanTween.move(CoinsMenuRects[2], new Vector3(0f,-250f,0f), AnimTime).setEase( LeanTweenType.easeOutSine ).setDelay(0.45f);
		LeanTween.move(CoinsMenuRects[3], new Vector3(0f,-350f,0f), AnimTime).setEase( LeanTweenType.easeOutSine ).setDelay(0.4f);
		LeanTween.move(CoinsMenuRects[4], new Vector3(0f,-450f,0f), AnimTime).setEase( LeanTweenType.easeOutSine ).setDelay(0.35f);
		LeanTween.move(CoinsMenuRects[5], new Vector3(0f,-550f,0f), AnimTime).setEase( LeanTweenType.easeOutSine ).setDelay(0.3f);
		LeanTween.move(CoinsMenuRects[6], new Vector3(0f,-650f,0f), AnimTime).setEase( LeanTweenType.easeOutSine ).setDelay(0.25f);
		LeanTween.move(CoinsMenuRects[7], new Vector3(0f,-750f,0f), AnimTime).setEase( LeanTweenType.easeOutSine ).setDelay(0.2f);
		LeanTween.move(CoinsMenuRects[8], new Vector3(0f,-850f,0f), AnimTime).setEase( LeanTweenType.easeOutSine ).setDelay(0.15f);
		LeanTween.move(CoinsMenuRects[9], new Vector3(0f,-950f,0f), AnimTime).setEase( LeanTweenType.easeOutSine ).setDelay(0.1f);


	}
	public void SlideCoinsMenuOut () {
		LeanTween.move(CoinsMenuRects[0], new Vector3(-Screen.width*2,-175f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(CoinsMenuRects[1], new Vector3(0f,500f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(CoinsMenuRects[2], new Vector3(0f,500f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(CoinsMenuRects[3], new Vector3(0f,500f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(CoinsMenuRects[4], new Vector3(0f,500f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(CoinsMenuRects[5], new Vector3(0f,500f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(CoinsMenuRects[6], new Vector3(0f,500f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(CoinsMenuRects[7], new Vector3(0f,500f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(CoinsMenuRects[8], new Vector3(0f,500f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(CoinsMenuRects[9], new Vector3(0f,500f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );

	}
	public void SlideGameOverMenuIn () {
		LeanTween.move(GameOverMenuRects[0], new Vector3(0f,57f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(GameOverMenuRects[1], new Vector3(-150f,-175f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(GameOverMenuRects[2], new Vector3(0f,-175f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
		LeanTween.move(GameOverMenuRects[3], new Vector3(150f,-175f,0f), AnimTime).setEase( LeanTweenType.easeOutBack );
	}
	public void SlideGameOverMenuOut () {
		LeanTween.move(GameOverMenuRects[0], new Vector3(0f,1000f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(GameOverMenuRects[1], new Vector3(-150f,-1000f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(GameOverMenuRects[2], new Vector3(0f,-1000f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
		LeanTween.move(GameOverMenuRects[3], new Vector3(150f,-1000f,0f), AnimTime).setEase( LeanTweenType.easeInBack );
	}
	public void SlideInGameMenuIn () {
		LeanTween.move(InGameMenuRects[0], new Vector3(-50f,-50f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(InGameMenuRects[1], new Vector3(0f,0f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(InGameMenuRects[2], new Vector3(0f,-100f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(InGameMenuRects[3], new Vector3(0f,-150f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(InGameMenuRects[4], new Vector3(0f,0f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(InGameMenuRects[5], new Vector3(0f,-200f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );

	}
	public void SlideInGameMenuOut () {
		LeanTween.move(InGameMenuRects[0], new Vector3(500f,-50f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(InGameMenuRects[1], new Vector3(-500f,0f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(InGameMenuRects[2], new Vector3(-500f,-100f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(InGameMenuRects[3], new Vector3(-500f,-150f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(InGameMenuRects[4], new Vector3(0f,-500f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );
		LeanTween.move(InGameMenuRects[5], new Vector3(-500f,-200f,0f), AnimTime).setEase( LeanTweenType.easeOutSine );

	}
}
