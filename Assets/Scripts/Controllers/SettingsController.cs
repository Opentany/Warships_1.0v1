using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour {

	public static string previousScene = "MenuScene";
	private static bool isVibration = true;
	private static float volumeOfSounds = 1.0f;
	private static float volumeOfMusic = 0.7f;
	private static GameObject soundsSlider;
	private static GameObject musicSlider;
	private static GameObject vibrationToggle;

	void Start () {
		soundsSlider = GameObject.Find ("Sound_Slider");
		musicSlider = GameObject.Find ("Music_Slider");
		vibrationToggle = GameObject.Find ("Vibration");
		Debug.Log (volumeOfMusic);
		Debug.Log (volumeOfSounds);
		vibrationToggle.GetComponent<Toggle> ().isOn = isVibration;
		soundsSlider.GetComponent<Slider> ().value = volumeOfSounds;
		musicSlider.GetComponent<Slider> ().value = volumeOfMusic;
		GameObject.Find ("Music").GetComponent<AudioSource> ().volume = volumeOfMusic;
	}

	public void SetSoundVolume(){
		volumeOfSounds = soundsSlider.GetComponent<Slider> ().value;
	}

	public static float GetSoundVolume(){
		return volumeOfSounds;
	}

	public void SetMusicVolume(){
		volumeOfMusic = musicSlider.GetComponent<Slider> ().value;
		SetMusicVolumeInScene ();
	}
		
	public void SetVibration(){		
		isVibration = !isVibration;
	}

	public static bool IsVibrationEnabled(){
		return isVibration;
	}

	public static void SetMusicVolumeInScene(){
		GameObject.Find ("Music").GetComponent<AudioSource> ().volume = volumeOfMusic;
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

}
