using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PreparationController : MonoBehaviour {

	public static WarshipPlacer warshipPlacer;
    public static Player otherPlayer;
    public static HumanPlayer humanDevicePlayer;
    public GameObject horizontalButton;
    public GameObject verticalButton;
    public bool otherPlayerReady;

    void Start() {
        SettingsController.SetMusicVolumeInScene();
        MusicController.SetActualMusic(Variables.PREPARATION_MUSIC);
        otherPlayerReady = false;
        PreparePlayers();
        PrepareBoards();
        CreateWarships();
    }

	private void PreparePlayers(){
        humanDevicePlayer.SetPreparationController(this);
        HumanPlayer.placementBoard.IsEverythingOk();
        otherPlayer.SetPreparationController(this);
    }

    private void PrepareBoards()
    {
        ViewBoard viewBoard = new ViewBoard();
        viewBoard.GenerateBoardOnScreen();
        HumanPlayer.SetViewBoard(viewBoard);
        otherPlayer.ArrangeBoard();
    }

	private void CreateWarships(){
		warshipPlacer = new global::WarshipPlacer ();
		ChooseWarship ((int)WarshipSize.FOUR);
		warshipPlacer.CreatePrefabList();
	}

    public void UndoneLastWarship(){
		if (warshipPlacer.GetNumberOfWarshipOnBoard() != 0){
			warshipPlacer.RemoveWarshipFromBoard ();
        }
    }

	public void ChooseWarship(int size){
		warshipPlacer.SetCurrentlySelectedSize (size);
		ChangeOrientation ();
	}

   
    public void ChangeOrientation(){
		warshipPlacer.SetWarshipChosenOrientation (warshipPlacer.CheckIfOrientationIsHorizontal () ? WarshipOrientation.VERTICAL : WarshipOrientation.HORIZONTAL);
        UpdateOrientation();
    }

    private void UpdateOrientation() {
		if (warshipPlacer.CheckIfOrientationIsHorizontal()) {
            verticalButton.SetActive(false);
            horizontalButton.SetActive(true);
        }
        else{
            horizontalButton.SetActive(false);
            verticalButton.SetActive(true);
        }
    }

    public void StartGame(string sceneName) {
        Debug.Log("I wanna start");
		if (warshipPlacer.isReady() && otherPlayerReady) {
			GameplayController.setPlayers (humanDevicePlayer, otherPlayer);
			Debug.Log ("Set");
			humanDevicePlayer.SetPlayerBoard ();
            otherPlayer.SetPlayerBoard();
            Debug.Log("Set");
            LoadScene (sceneName);    
		} else {
			warshipPlacer.androidToast.CreateToastWithMessage (Variables.NO_ALL_SHIPS_ON_BORAD);
		}     
    }
		
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
	