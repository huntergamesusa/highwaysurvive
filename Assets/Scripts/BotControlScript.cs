using UnityEngine;
using System.Collections;
using CnControls;
using UnityEngine.UI;
// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
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
	public static GameObject baseController;
	public static GameObject joystickController;
	public GameObject myWeaponParent;
	Vector3 dir;
	float dist;
	AudioClip woosh;
	AudioClip sonicAudio;
	public GameObject sonicsmash;
	EnableRagdoll ragdollenable;
	bool isBig=false;
	public static bool magnetPowerUp = false;
	public void ReceiveJoystick(GameObject joy, GameObject basecont){
		baseController = basecont;
		joystickController=joy;
		joystickController.transform.parent.GetComponent<Button>().onClick.AddListener(()=>Attack(PlayerColliderManager.powerup));


	}

	void Awake(){
		woosh = Resources.Load ("Sounds/woosh_2")as AudioClip;
		sonicAudio = Resources.Load ("Sounds/Blaster")as AudioClip;
		ragdollenable=transform.Find ("RagdollEnable").GetComponent<EnableRagdoll> ();

	}
	void Update(){
		if (baseController == null)
			return;
		dir = (baseController.transform.position - joystickController.transform.position).normalized;
		dist = Vector3.Distance (baseController.transform.position, joystickController.transform.position)/26.625f;
//		print (dist);
		float zRotation_i = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
		if (dist > .01f) {
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, -1 * zRotation_i - 90, transform.eulerAngles.z);
		}

	}

	void Start ()
	{
		//.2
		Vector3 adjustSlash = new Vector3(myWeaponParent.transform.GetChild (0).GetComponent<MeshFilter> ().mesh.bounds.size.z/.19f,myWeaponParent.transform.GetChild (0).GetComponent<MeshFilter> ().mesh.bounds.size.z/.19f,myWeaponParent.transform.GetChild (0).GetComponent<MeshFilter> ().mesh.bounds.size.z/.19f);
		slashAnim.transform.localScale = adjustSlash;
		print(myWeaponParent.transform.GetChild (0).GetComponent<MeshFilter> ().mesh.bounds.size.z);
		// initialising reference variables
		myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider>().enabled=false;
		anim = GetComponent<Animator>();					  
		col = GetComponent<CapsuleCollider>();				
//		enemy = GameObject.Find("Enemy").transform;	
		if(anim.layerCount ==2)
			anim.SetLayerWeight(1, 1);
	}
	



	public void Attack(string type){
		type = "magnet";
		if (anim.enabled == false)
			return;


		switch(type){
		default:
			
			SonicBoom ();

			break;

		case "bigguy":
			StartCoroutine (BigGuyEnable ());
			break;

		case "magnet":
			StartCoroutine (MagnetStart ());

			break;
		
		case "sonicboom":
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
		PlayerColliderManager.powerup = "";


	}

IEnumerator MagnetStart(){
		magnetPowerUp = true;
	yield return new WaitForSeconds (10);
		magnetPowerUp = false;

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
			// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
//		anim.SetFloat("Direction", h); 	
		// set our animator's float parameter 'Direction' equal to the horizontal input axis
		if (isBig) {
			anim.speed = PlayerPrefs.GetFloat ("animspeed")/1.5f;		
		} else {
			anim.speed = PlayerPrefs.GetFloat ("animspeed");		

		}
		// set the speed of our animator to the public variable 'animSpeed'
		anim.SetLookAtWeight(lookWeight);					// set the Look At Weight - amount to use look at IK vs using the head's animation
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation


		if (v == 0) {
//			transform.Rotate (transform.up * h*1.18f);
		}

		if(anim.layerCount ==2)		
			layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);
		// set our layer2CurrentState variable to the current state of the second Layer (1) of animation
		if (currentBaseState.fullPathHash == locoState) {
			if(myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider>().enabled==true){
				myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider>().enabled=false;

			}
			if (Input.GetKeyUp (KeyCode.LeftShift)) {
				slashAnim.SetActive (true);
				anim.SetBool ("Attack", true);
				slashAnim.GetComponent<ParticleSystem>().Play();
						
			}
		}

		if (currentBaseState.fullPathHash == idleState) {
			if(myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider>().enabled==true){
				myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider>().enabled=false;

			}
				
			if (Input.GetKeyUp (KeyCode.LeftShift)) {
				anim.SetBool ("IdleAttack", true);
				slashAnim.SetActive (true);
				slashAnim.GetComponent<ParticleSystem>().Play();
			}
//			if(slashAnim.GetComponent<ParticleSystem>().isPlaying==true){
//				slashAnim.GetComponent<ParticleSystem>().Stop();
//			}
		}

		if (currentBaseState.fullPathHash == idleattackState) {
			if(myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider>().enabled==false){
				myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider>().enabled=true;

			}
			anim.SetBool("IdleAttack", false);
		}
		if (currentBaseState.fullPathHash == attackState) {
			if(myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider>().enabled==false){
				myWeaponParent.transform.GetChild (0).GetComponent<MeshCollider>().enabled=true;

			}
			anim.SetBool("Attack", false);

		}
	
		// LOOK AT ENEMY
		
		// if we hold Alt..
		if(Input.GetButton("Fire2"))
		{
			// ...set a position to look at with the head, and use Lerp to smooth the look weight from animation to IK (see line 54)
			anim.SetLookAtPosition(enemy.position);
			lookWeight = Mathf.Lerp(lookWeight,1f,Time.deltaTime*lookSmoother);
		}
		// else, return to using animation for the head by lerping back to 0 for look at weight
		else
		{
			lookWeight = Mathf.Lerp(lookWeight,0f,Time.deltaTime*lookSmoother);
		}
		
		// STANDARD JUMPING
		
		// if we are currently in a state called Locomotion (see line 25), then allow Jump input (Space) to set the Jump bool parameter in the Animator to true
		if (currentBaseState.nameHash == locoState)
		{
			if(Input.GetButtonDown("Jump"))
			{
				anim.SetBool("Jump", true);
			}
		}
		
		// if we are in the jumping state... 
		else if(currentBaseState.nameHash == jumpState)
		{
			//  ..and not still in transition..
			if(!anim.IsInTransition(0))
			{
				if(useCurves)
					// ..set the collider height to a float curve in the clip called ColliderHeight
					col.height = anim.GetFloat("ColliderHeight");
				
				// reset the Jump bool so we can jump again, and so that the state does not loop 
				anim.SetBool("Jump", false);
			}
			
			// Raycast down from the center of the character.. 
			Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
			RaycastHit hitInfo = new RaycastHit();
			
			if (Physics.Raycast(ray, out hitInfo))
			{
				// ..if distance to the ground is more than 1.75, use Match Target
				if (hitInfo.distance > 1.75f)
				{
					
					// MatchTarget allows us to take over animation and smoothly transition our character towards a location - the hit point from the ray.
					// Here we're telling the Root of the character to only be influenced on the Y axis (MatchTargetWeightMask) and only occur between 0.35 and 0.5
					// of the timeline of our animation clip
					anim.MatchTarget(hitInfo.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), 0.35f, 0.5f);
				}
			}
		}
		
		
		// JUMP DOWN AND ROLL 
		
		// if we are jumping down, set our Collider's Y position to the float curve from the animation clip - 
		// this is a slight lowering so that the collider hits the floor as the character extends his legs
		else if (currentBaseState.nameHash == jumpDownState)
		{
			col.center = new Vector3(0, anim.GetFloat("ColliderY"), 0);
		}
		
		// if we are falling, set our Grounded boolean to true when our character's root 
		// position is less that 0.6, this allows us to transition from fall into roll and run
		// we then set the Collider's Height equal to the float curve from the animation clip
		else if (currentBaseState.nameHash == fallState)
		{
			col.height = anim.GetFloat("ColliderHeight");
		}
		
		// if we are in the roll state and not in transition, set Collider Height to the float curve from the animation clip 
		// this ensures we are in a short spherical capsule height during the roll, so we can smash through the lower
		// boxes, and then extends the collider as we come out of the roll
		// we also moderate the Y position of the collider using another of these curves on line 128
		else if (currentBaseState.nameHash == rollState)
		{
			if(!anim.IsInTransition(0))
			{
				if(useCurves)
					col.height = anim.GetFloat("ColliderHeight");
				
				col.center = new Vector3(0, anim.GetFloat("ColliderY"), 0);
				
			}
		}
		// IDLE
		
		// check if we are at idle, if so, let us Wave!
		else if (currentBaseState.nameHash == idleState)
		{
			if(Input.GetButtonUp("Jump"))
			{
				anim.SetBool("Wave", true);
			}
		}
		// if we enter the waving state, reset the bool to let us wave again in future
		if(layer2CurrentState.nameHash == waveState)
		{
			anim.SetBool("Wave", false);
		}
	

		}
}
