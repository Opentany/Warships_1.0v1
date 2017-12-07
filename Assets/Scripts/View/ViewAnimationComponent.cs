using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAnimationComponent : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetEffect(string animationName){
		Animator animator = this.GetComponent<Animator> ();
		animator.SetTrigger(animationName);
	}

	public void SetEffectOnField(DmgDone shotResult){
		if (shotResult.Equals(DmgDone.HIT)) 
		{
			SetEffect (Variables.animationTriggerHit);
			SetEffect (Variables.animationTriggerFire);
			if (SettingsController.IsVibrationEnabled ()) {
				Handheld.Vibrate ();			
			}
			AudioClip audio = Resources.Load (Variables.BOOM_SOUND_PATH) as AudioClip;
			AudioSource.PlayClipAtPoint (audio, Vector2.zero, SettingsController.GetSoundVolume());
		}
		else if (shotResult.Equals(DmgDone.MISS))
		{
			SetEffect (Variables.animationTriggerMiss);
			AudioClip audio = Resources.Load (Variables.SPLASH_SOUND_PATH) as AudioClip;
			AudioSource.PlayClipAtPoint (audio, Vector2.zero, SettingsController.GetSoundVolume());
		}
		else if (shotResult.Equals(DmgDone.SINKED)){
			SetEffect (Variables.animationTriggerHit);
			SetEffect(Variables.animationTriggerBack);
			if (SettingsController.IsVibrationEnabled ()) {
				Handheld.Vibrate ();			
			}
			AudioClip audio = Resources.Load (Variables.BOOM_SOUND_PATH) as AudioClip;
			AudioSource.PlayClipAtPoint (audio, Vector2.zero, SettingsController.GetSoundVolume());
		}
	}
}
