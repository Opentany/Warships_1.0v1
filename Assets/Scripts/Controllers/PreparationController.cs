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
    public GameObject warship4HorizontalPrefab;
    public GameObject warship3HorizontalPrefab;
    public GameObject warship2HorizontalPrefab;
    public GameObject warship1HorizontalPrefab;
    public GameObject warship4VerticalPrefab;
    public GameObject warship3VerticalPrefab;
    public GameObject warship2VerticalPrefab;
    public GameObject warship1VerticalPrefab;
    public GameObject shotFieldPrefab;
    public static Player botPlayer;
    public static Player humanPlayer;
    public GameObject horizontalButton;
    public GameObject verticalButton;


    private static WarshipCreator warshipCreator;
    private static ViewFieldComponent chosenField;
    private WarshipsContainer botPlayerShips;
    private WarshipsContainer humanPlayerShips;

    public static int chosenWarshipSize;
    // Use this for initialization
    void Start()
    {
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
		List<GameObject> prefab1 = new List<GameObject> ();
		prefab1.Add (warship1VerticalPrefab);
		prefab1.Add (warship1HorizontalPrefab);
		warshipPrefabList.Add (prefab1);
		List<GameObject> prefab2 = new List<GameObject> ();
		prefab2.Add (warship2VerticalPrefab);
		prefab2.Add (warship2HorizontalPrefab);
		warshipPrefabList.Add (prefab2);
		List<GameObject> prefab3 = new List<GameObject> ();
		prefab3.Add (warship3VerticalPrefab);
		prefab3.Add (warship3HorizontalPrefab);
		warshipPrefabList.Add (prefab3);
		List<GameObject> prefab4 = new List<GameObject> ();
		prefab4.Add (warship4VerticalPrefab);
		prefab4.Add (warship4HorizontalPrefab);
		warshipPrefabList.Add (prefab4);
		return warshipPrefabList;
	}

    public void UndoneLastWarship(){
		if (warshipPlacer.GetNumberOfWarshipOnBoard() != 0){
			warshipPlacer.RemoveWarshipFromBoard ();
        }
    }

	public void ChooseWarship(int size){
		warshipPlacer.SetCurrentlySelectedSize (size);
		warshipPlacer.SetWarshipChosenOrientation(WarshipOrientation.VERTICAL);
		UpdateOrientation ();
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



