using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	private static MusicController instance;
	private static string actualScene;

	public static MusicController GetInstance(){
		return instance;
	}
		
	void Awake(){
		if (instance != null && instance != this) {
			Destroy(this.gameObject);			
			return;
		} 
		else {
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}

	public static void SetActualMusic(string sceneName){
		if (actualScene != sceneName) {
			actualScene = sceneName;		
			LoadNewMusicClip ();
		}
	}

	private static void LoadNewMusicClip(){
		AudioSource audio = instance.GetComponent<AudioSource> ();
		if (actualScene == Variables.PREPARATION_MUSIC) {
			audio.clip = Resources.Load (Variables.PREPARATION_MUSIC_PATH) as AudioClip;
			audio.Play ();
		} else if (actualScene == Variables.GAMEPLAY_MUSIC) {
			audio.clip = Resources.Load (Variables.GAMEPLAY_MUSIC_PATH) as AudioClip;
			audio.Play ();
		} else if (actualScene == (Variables.MENU_MUSIC)) {
			audio.clip = Resources.Load (Variables.MENU_MUSIC_PATH) as AudioClip;	
			audio.Play ();
		}
	}
}
