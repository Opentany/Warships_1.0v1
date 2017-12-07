using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour {


	public static string previousScene = "MenuScene";
	private static bool isVibration = true;
	private static float volumeOfSounds = 1.0f;
	private static float volumeOfMusic = 1.0f;
	private static GameObject soundsSlider;
	private static GameObject musicSlider;
	private static GameObject vibrationToggle;

	void Start () {
		soundsSlider = GameObject.Find ("Music_Slider");
		musicSlider = GameObject.Find ("Sound_Slider");
		vibrationToggle = GameObject.Find ("Vibration");
		vibrationToggle.GetComponent<Toggle> ().isOn = isVibration;
	}


	public void SetVibration(){		
		isVibration = !isVibration;
	}

	public static bool IsVibrationEnabled(){
		return isVibration;
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}


}
