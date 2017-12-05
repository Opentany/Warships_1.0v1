using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PreparationController : MonoBehaviour {

    public static ViewBoard preparationBoard;
    public static PlacementBoard placementBoard;
	public static WarshipPlacer warshipPlacer;
	public GameObject warshipToMinimap;
    public GameObject fieldPrefab;
	public GameObject animationHolder;
    public static Player botPlayer;
    public static Player humanPlayer;
    public GameObject horizontalButton;
    public GameObject verticalButton;

    private static WarshipCreator warshipCreator;
    private WarshipsContainer botPlayerShips;
    private WarshipsContainer humanPlayerShips;

    void Start() {
		PrepareBoards ();
		CreateWarships ();
		CreatePlayersAndStartArrange();
    }

	private void PrepareBoards(){
		preparationBoard = new ViewBoard();
		placementBoard = new PlacementBoard();
		ViewBoard.SetWaterPrefab(fieldPrefab);
		ViewBoard.SetWarshipPrefab (warshipToMinimap);
		ViewBoard.SetAnimationHolder (animationHolder);
		preparationBoard.GenerateBoardOnScreen();
	}

	private void CreateWarships(){
		warshipCreator = new WarshipCreator();
		warshipPlacer = new global::WarshipPlacer ();
		ChooseWarship ((int)WarshipSize.FOUR);
		warshipPlacer.SetAllWarshipList(warshipCreator.GetWarshipsList ());
		warshipPlacer.SetWarshipPrefabList (CreatePrefabList ());
	}

	private void CreatePlayersAndStartArrange(){
		botPlayer = BotCreator.CreateBotPlayer();
		humanPlayer = new HumanPlayer();
		botPlayer.ArrangeBoard();
		botPlayerShips = botPlayer.GetPlayerShips();
	}

	private List<List<GameObject>> CreatePrefabList(){
		List<List<GameObject>> warshipPrefabList = new List<List<GameObject>> ();
		for (int i = 1; i <= 4; i++) {
			List<GameObject> prefabs = new List<GameObject> ();
			prefabs.Add (Resources.Load (GetPrefabPath (i, "vertical")) as GameObject);
			prefabs.Add (Resources.Load (GetPrefabPath (i, "horizontal")) as GameObject);
			warshipPrefabList.Add (prefabs);
		}

		return warshipPrefabList;
	}

	private string GetPrefabPath(int i, string type){
		return "Prefab/warship" + i.ToString() + "_" + type; 
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
        else
        {
            horizontalButton.SetActive(false);
            verticalButton.SetActive(true);
        }
    }

    public void StartGame(string sceneName) {
        Debug.Log("I wanna start");
        if (PlacementManager.CanGameStart(placementBoard.GetFieldsOccupiedByWarships())) {
            GameplayController.setPlayers(humanPlayer, botPlayer);
            Debug.Log("Set");
            //humanPlayer.SetPlayerBoard(humanPlayerShips);
            humanPlayer.SetPlayerBoard(placementBoard.GetWarshipList());
            Debug.Log("1st Player");
            botPlayer.SetPlayerBoard(botPlayerShips);
            Debug.Log("2nd Player");
            LoadScene(sceneName);    
        }        
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}



