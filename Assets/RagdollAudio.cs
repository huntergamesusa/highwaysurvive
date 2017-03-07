using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollAudio : MonoBehaviour {
	AudioClip lightImpact;
	AudioClip heavyImpact;
	AudioSource source;
	private float lowPitchRange  = .75F;
	private float  highPitchRange  = 1.5F;
	private float velToVol = .2F;
	private float velocityClipSplit = 10f;
	int hit = 0;
	void Awake(){
		lightImpact = Resources.Load ("Sounds/ThudLight")as AudioClip;
		heavyImpact = Resources.Load ("Sounds/ThudMedium")as AudioClip;
		source = GetComponent<AudioSource> ();
	}
	// Use this for initialization
	void OnCollisionEnter(Collision coll){
		if (hit >= 3) {
			if (coll.gameObject.tag == "roadparent" || coll.gameObject.tag == "blockers") {
				hit++;
				source.pitch = Random.Range (lowPitchRange, highPitchRange);
				float hitVol = coll.relativeVelocity.magnitude * velToVol;
				hitVol = Mathf.Clamp (hitVol, 0f, .75f);
				print (coll.relativeVelocity.magnitude);
				if (coll.relativeVelocity.magnitude < velocityClipSplit)
					source.PlayOneShot (lightImpact, hitVol);
				else
					source.PlayOneShot (heavyImpact, hitVol);
			}
		}
	}
}
