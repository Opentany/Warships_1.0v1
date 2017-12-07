using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MenuController : MonoBehaviour{

	private static bool isFirstTimeLoad = true;

	void Start(){
		LoadSettings ();
		SettingsController.SetMusicVolumeInScene ();
	}

	private void LoadSettings(){
		if (File.Exists (Variables.SETTINGS_PATH) && isFirstTimeLoad) {
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream file = File.Open (Variables.SETTINGS_PATH, FileMode.Open);
			Settings setting = (Settings)binaryFormatter.Deserialize (file);
			file.Close ();
			SetSettings (setting);
			isFirstTimeLoad = false;
		}
	}

	private void SetSettings(Settings setting){
		SettingsController.SetMusicVolume (setting.volumeOfMusic);
		SettingsController.SetSoundVolume (setting.volumeOfSound);
		SettingsController.SetVibration (setting.isVibration);
	}

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

	public void Quit(){
		SaveSettingsValues ();
		Application.Quit();
	}

	private void SaveSettingsValues(){
		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		FileStream file = File.Create (Variables.SETTINGS_PATH);
		Settings setting = CreateSettings ();
		binaryFormatter.Serialize (file, setting);
		file.Close ();
	}

	private Settings CreateSettings(){
		bool isVibration = SettingsController.IsVibrationEnabled ();
		float soundVolume = SettingsController.GetSoundVolume ();
		float musicVolume = SettingsController.GetMusicVolume ();
		return new Settings (isVibration, soundVolume, musicVolume);
	}
}

[System.Serializable]
class Settings{
	public bool isVibration;
	public float volumeOfSound;
	public float volumeOfMusic;

	public Settings(bool isVibration, float volumeOfSound, float volumeOfMusic){
		this.isVibration = isVibration;
		this.volumeOfSound = volumeOfSound;
		this.volumeOfMusic = volumeOfMusic;
	}
}