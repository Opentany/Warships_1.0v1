﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerController : MonoBehaviour {

	void Start(){
		MusicController.SetActualMusic (Variables.PREPARATION_MUSIC);
		SettingsController.SetMusicVolumeInScene ();
	}

	public void LoadEasyLevel(){
		BotLevelInfo.botLevel = BotLevel.EASY;
		LoadPreparationScene ();
	}

	public void LoadMediumLevel(){
		BotLevelInfo.botLevel = BotLevel.AVERAGE;
		LoadPreparationScene ();
	}

	public void LoadHardLevel(){
		BotLevelInfo.botLevel = BotLevel.GREAT;
		LoadPreparationScene ();
	}

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

	private void LoadPreparationScene(){
        PreparationController.humanDevicePlayer = new HumanPlayer();
        PreparationController.otherPlayer = BotCreator.CreateBotPlayer();
        SceneManager.LoadScene ("Preparation");
	}
}
