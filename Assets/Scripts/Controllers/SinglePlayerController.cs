using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerController : MonoBehaviour {

	public void LoadEasyLevel(string botLevelName){
		BotLevelInfo.botLevel = botLevelName;
		LoadPreparationScene ();
	}

	public void LoadMediumLevel(string botLevelName){
		BotLevelInfo.botLevel = botLevelName;
		LoadPreparationScene ();
	}

	public void LoadHardLevel(string botLevelName){
		BotLevelInfo.botLevel = botLevelName;
		LoadPreparationScene ();
	}

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

	private void LoadPreparationScene(){
		SceneManager.LoadScene ("Preparation");
	}
}
