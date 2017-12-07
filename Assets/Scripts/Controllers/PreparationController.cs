using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PreparationController : MonoBehaviour {

    public static ViewBoard preparationBoard;
    public static PlacementBoard placementBoard;
	public static WarshipPlacer warshipPlacer;
    public static Player botPlayer;
    public static Player humanPlayer;
    public GameObject horizontalButton;
    public GameObject verticalButton;

    private WarshipsContainer botPlayerShips;
    private WarshipsContainer humanPlayerShips;

    void Start() {
		SettingsController.SetMusicVolumeInScene ();
		MusicController.SetActualMusic (Variables.PREPARATION_MUSIC);
		PrepareBoards ();
		CreateWarships ();
		CreatePlayersAndStartArrange();
    }

	private void PrepareBoards(){
		preparationBoard = new ViewBoard();
		placementBoard = new PlacementBoard();
		preparationBoard.GenerateBoardOnScreen();
	}

	private void CreateWarships(){
		warshipPlacer = new global::WarshipPlacer ();
		ChooseWarship ((int)WarshipSize.FOUR);
		warshipPlacer.CreatePrefabList ();
	}

	private void CreatePlayersAndStartArrange(){
		botPlayer = BotCreator.CreateBotPlayer();
		humanPlayer = new HumanPlayer();
		botPlayer.ArrangeBoard();
		botPlayerShips = botPlayer.GetPlayerShips();
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
		if (PlacementManager.CanGameStart (placementBoard.GetFieldsOccupiedByWarships ())) {
			GameplayController.setPlayers (humanPlayer, botPlayer);
			Debug.Log ("Set");
			//humanPlayer.SetPlayerBoard(humanPlayerShips);
			humanPlayer.SetPlayerBoard (placementBoard.GetWarshipList ());
			Debug.Log ("1st Player");
			botPlayer.SetPlayerBoard (botPlayerShips);
			Debug.Log ("2nd Player");
			LoadScene (sceneName);    
		} else {
			warshipPlacer.androidToast.CreateToastWithMessage (Variables.NO_ALL_SHIPS_ON_BORAD);
		}      
    }
		
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
	