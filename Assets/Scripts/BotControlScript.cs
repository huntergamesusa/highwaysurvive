using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// Require these components when using this script

public class BotControlScript : MonoBehaviour
{
	[System.NonSerialized]					
	public float lookWeight;					// the amount to transition when using head look
	
	[System.NonSerialized]
	public Transform enemy;						// a transform to Lerp the camera to during head look
	
	public float animSpeed = 1.5f;				// a public setting for overall animator animation speed
	public float lookSmoother = 3f;				// a smoothing setting for camera motion
	public bool useCurves;						// a setting for teaching purposes to show use of curves

	
	private Animator anim;							// a reference to the animator on the character
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	private AnimatorStateInfo layer2CurrentState;	// a reference to the current state of the animator, used for layer 2
	private CapsuleCollider col;					// a reference to the capsule collider of the character
	

	static int idleState = Animator.StringToHash("Base Layer.Idle");	
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");			// these integers are references to our animator's states
	static int jumpState = Animator.StringToHash("Base Layer.Jump");				// and are used to check state for various actions to occur
	static int jumpDownState = Animator.StringToHash("Base Layer.JumpDown");		// within our FixedUpdate() function below
	static int fallState = Animator.StringToHash("Base Layer.Fall");
	static int rollState = Animator.StringToHash("Base Layer.Roll");
	static int waveState = Animator.StringToHash("Layer2.Wave");
	static int attackState = Animator.StringToHash("Base Layer.Attack");
	static int idleattackState = Animator.StringToHash("Base Layer.Idle Attack");

	public GameObject slashAnim;
	 Transform baseController;
	 Transform joystickController;
	public GameObject myWeaponParent;
	Vector3 dir;
	float dist;
	AudioClip woosh;
	AudioClip sonicAudio;
	public GameObject sonicsmash;
	public GameObject bomb;
	EnableRagdoll ragdollenable;
	bool isBig=false;
	public static bool magnetPowerUp = false;
	public static bool doublecoins = false;
	public static int bombsLeft=0;
	GameObject powerupParent;
	bool delayPowerup;
	GameObject bombsUI;
	public AudioSource footstep;
	ScoringManager theScoringManager;
	public void ReceiveJoystick(Transform joy, Transform basecont){
		baseController = basecont;
		joystickController=joy;
//	
		joystickController.transform.parent.GetComponent<Button>().onClick.AddListener(()=>Attack(PlayerColliderManager.powerup));


	}

	void Start(){

		woosh = Resources.Load ("Sounds/woosh_2")as AudioClip;
		sonicAudio = Resources.Load ("Sounds/Blaster")as AudioClip;
		ragdollenable=transform.Find ("RagdollEnable").GetComponent<EnableRagdoll> ();
		powerupParent = GameObject.Find ("BubblePowerUp");
		footstep = transform.Find ("Footsteps").GetComponent<AudioSource>();
		theScoringManager = GameObject.Find ("SaveScores").GetComponent<ScoringManager>();

		if(myWeaponParent.transform.GetChild (0)!=null){
		//.2
		Vector3 adjustSlash = new Vector3(myWeaponParent.transform.GetChild (0).GetComponent<MeshFilter> ().mesh.bounds.size.z/.19f,myWeaponParent.transform.GetChild (0).GetComponent<MeshFilter> ().mesh.bounds.size.z/.19f,myWeaponParent.transform.GetChild (0).GetComponent<MeshFilter> ().mesh.bounds.size.z/.19f);
		slashAnim.transform.localScale = adjustSlash;
		print(myWeaponParent.transform.GetChild (0).GetComponent<MeshFilter> ().mesh.bounds.size.z);
		// initialising reference variables

		myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider>().enabled=false;
		}
		anim = GetComponent<Animator>();					  
		col = GetComponent<CapsuleCollider>();				
		//		enemy = GameObject.Find("Enemy").transform;	
		if(anim.layerCount ==2)
			anim.SetLayerWeight(1, 1);

	}
	void Update(){
		if (baseController == null)
			return;
		dir = (baseController.position - joystickController.position).normalized;
		dist = Vector3.Distance (baseController.position, joystickController.position)/26.625f;
//		print (dist);
		float zRotation_i = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
		if (dist > .01f) {
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, -1 * zRotation_i - 90, transform.eulerAngles.z);
		}

	}


	



	public void Attack(string type){
				if (anim.enabled == false|| myWeaponParent.transform.GetChild (0)==null)
					
			return;


		switch(PlayerColliderManager.powerup){
		case "sonicboom":
			
			SonicBoom ();

			break;

		case "bomb":
			bombsLeft = 1;
			break;
		case "3bomb":
			bombsLeft = 3;
			bombsUI = GameObject.Find ("3bomb");
			bombsUI.GetComponent<Image> ().enabled = false;
			break;
		case "giant":
			StartCoroutine (BigGuyEnable ());
			break;

		case "magnet":
			StartCoroutine (MagnetStart ());

			break;
		case "2X":
			StartCoroutine (DoubleCoins ());

			break;
		default:
			
			PlayerColliderManager.powerup = "";
		if (currentBaseState.fullPathHash == locoState) {

				slashAnim.SetActive (true);
				anim.SetBool ("Attack", true);
				slashAnim.GetComponent<ParticleSystem>().Play();
			GetComponent<AudioSource>().PlayOneShot(woosh,Random.Range(.8f,1));


		}
		if (currentBaseState.fullPathHash == idleState) {

				anim.SetBool ("IdleAttack", true);
				slashAnim.SetActive (true);
				slashAnim.GetComponent<ParticleSystem>().Play();
			GetComponent<AudioSource>().PlayOneShot(woosh,Random.Range(.8f,1));

			}
	
		break;
		}


		if (PlayerColliderManager.powerup == "bomb") {
			DropBomb (false);

		} else {
			if (bombsLeft > 0) {
				DropBomb (true);

			} 
			if (bombsLeft == 0) {
				LeanTween.moveY (powerupParent.transform.parent.GetComponent<RectTransform> (), 200f, .33f).setEaseInOutBounce ().setOnComplete (PowerupOff);
			}

	
		}

		if (PlayerColliderManager.powerup != "") {
			if (bombsLeft < 1) {
				LeanTween.moveY (powerupParent.transform.parent.GetComponent<RectTransform> (), 200f, .33f).setEaseInOutBounce ().setOnComplete(PowerupOff);

			}

				
			}
	
			
			PlayerColliderManager.powerup = "";


	}

	void PowerupOff(){
		
		for (int i = 0; i < powerupParent.transform.childCount; i++) {
			powerupParent.transform.GetChild (i).gameObject.SetActive (false);
		}
	}

	public void DropBomb(bool triple){
		print("bombs left "+bombsLeft);
		if (bombsLeft > 0) {
			if (triple) {
				switch (bombsLeft) {
				case 3:
					bombsUI.transform.GetChild (0).gameObject.SetActive (true);

					break;
				case 2:
					bombsUI.transform.GetChild (0).gameObject.SetActive (false);
					bombsUI.transform.GetChild (1).gameObject.SetActive (true);
					break;
				case 1:
					bombsUI.transform.GetChild (0).gameObject.SetActive (false);
					bombsUI.transform.GetChild (1).gameObject.SetActive (false);
					bombsUI.SetActive (false);
					bombsUI.GetComponent<Image> ().enabled = true;

					break;

				}
			}

			bombsLeft--;
			GameObject bombFab = Instantiate (bomb, transform.position, Quaternion.identity) as GameObject;
		}

	}

IEnumerator MagnetStart(){
		magnetPowerUp = true;
	yield return new WaitForSeconds (10);
		magnetPowerUp = false;

}
	IEnumerator DoubleCoins(){
		doublecoins = true;
		yield return new WaitForSeconds (10);
		doublecoins = false;

	}

	IEnumerator BigGuyEnable(){
		//ignores car from throwing player up in the are
		isBig=true;
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezeRotation;

		ragdollenable.PauseZombie (true);
		transform.Find ("BigPowerup").gameObject.SetActive (true);
		LeanTween.scale (gameObject, new Vector3 (34, 34, 34), .3f).setEaseInOutCirc ();
		yield return new WaitForSeconds (10);
		isBig=false;
		ragdollenable.PauseZombie (false);
		transform.Find ("BigPowerup").gameObject.SetActive (false);
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;

		LeanTween.scale (gameObject, new Vector3 (11.42057f, 11.42057f, 11.42057f), .3f).setEaseInOutCirc ();

	}

	public void SonicBoom(){
		//		var speed = 4;
		GameObject sonicBoomFab = Instantiate(sonicsmash, transform.position,Quaternion.identity) as GameObject;
		//		sonicBoomFab.GetComponent<ParticleSystem>().main.simulationSpeed = speed;
		for(int i = 0; i < sonicBoomFab.transform.childCount; i++){
			//			sonicBoomFab.transform.GetChild(i).GetComponent<ParticleSystem>().main.simulationSpeed = speed;
		}

		//			Camera.main.SendMessage("Shake");
		GetComponent<AudioSource>().PlayOneShot(sonicAudio);

		// Applies an explosion force to all nearby rigidbodies
		Vector3 explosionPos  = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, 25);

		foreach (Collider hit2 in colliders) {


			switch (hit2.tag) {
			case "zombieparent":
				GameObject ragenable = hit2.transform.Find ("RagdollEnable").gameObject;
				ragenable.GetComponent<EnableRagdoll> ().EngageRagdollZombie (Vector3.zero, true, transform.position);

				break;

			
			case "car":
				hit2.GetComponent<CarDrive> ().CarHit (true,transform.position,8000000f,50,5f);
				break;
			}
		
		}

	}


	void FixedUpdate ()
	{

		float v = dist;
		anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
		if (isBig) {
			anim.speed = PlayerPrefs.GetFloat ("animspeed")/1.5f;		
		} else {
			anim.speed = PlayerPrefs.GetFloat ("animspeed");		

		}
//		theScoringManager.UpdateDistance (anim.GetFloat("Speed")/50);
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation


		if (v == 0) {
//			transform.Rotate (transform.up * h*1.18f);
		}

		if(anim.layerCount ==2)		
			layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);
		if (currentBaseState.fullPathHash == locoState) {
//			footstep.volume = 1;
			if (isBig) {
//				footstep.pitch = anim.GetFloat ("Speed")/1.5f;
			} else {
//				footstep.pitch = anim.GetFloat ("Speed");

			}
			if (myWeaponParent.transform.GetChild (0) != null) {
				if (myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider> ().enabled == true) {
					myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider> ().enabled = false;

				}
			}

		} else {
			footstep.volume = 0;

		}


		if (currentBaseState.fullPathHash == idleState) {
			if (myWeaponParent.transform.GetChild (0) != null) {
				
				if (myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider> ().enabled == true) {
					myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider> ().enabled = false;
				}
			}

		}

		if (currentBaseState.fullPathHash == idleattackState) {
			if (myWeaponParent.transform.GetChild (0) != null) {
				
				if (myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider> ().enabled == false) {
					myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider> ().enabled = true;

				}
			}
			anim.SetBool("IdleAttack", false);
		}
		if (currentBaseState.fullPathHash == attackState) {
			if (myWeaponParent.transform.GetChild (0) != null) {
				
				if (myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider> ().enabled == false) {
					myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider> ().enabled = true;

				}
			}
			anim.SetBool("Attack", false);

		}
	
		

		
		// if we are in the jumping state... 
	
		
		

	

		}
}
