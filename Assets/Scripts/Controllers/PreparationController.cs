using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PreparationController : MonoBehaviour {

    public static ViewBoard preparationBoard;
    public static PlacementBoard placementBoard;
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


	private static List<List<Warship>> allWarships;
    private static List<GameObject> listOfWarshipsOnBoard;
    private static List<Warship> listOfWarships;
    private static WarshipCreator warshipCreator;
    private static WarshipOrientation chosenWarshipOrientation;
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
		chosenWarshipOrientation = WarshipOrientation.VERTICAL;
		allWarships = warshipCreator.GetWarshipsList ();
		ChooseWarship4();
        listOfWarshipsOnBoard = new List<GameObject>();
        listOfWarships =new List<Warship>();
	}

	private void CreatePlayersAndStartArrange(){
		botPlayer = BotCreator.CreateBotPlayer();
		humanPlayer = new HumanPlayer();
		botPlayer.ArrangeBoard();
		botPlayerShips = botPlayer.GetPlayerShips();
	}

    public bool SetWarshipOnField(ViewFieldComponent field)
    {
        //placementBoard.IsEverythingOk();
        chosenField = field;
        if (chosenField!=null)
        {
            //Debug.Log("wybrano pole "+chosenField.gridPosition.x+";"+chosenField.gridPosition.y);
            var statek = CreateWarship(chosenWarshipSize);
            if (statek==null)
            {  
                return false;
            }
			int x = statek.GetX ();
			int y = statek.GetY ();
			Debug.Log (x + " " + y + " " + placementBoard.GetBoard()[x][y].securePoints);
            if (PlacementManager.CheckIfPlayerCanPutWarshipOnThisPosition(placementBoard, statek))
            {
                placementBoard.SetWarship(statek);
                preparationBoard.SetWarship(statek);
                WarshipPlacer((int)statek.warshipSize, chosenField);
                listOfWarships.Add(statek);
                UpdateStatus(statek.warshipSize);
                return true;
            }
            else
            {
                Debug.Log("wybierz inne miejsce");
				Debug.Log (x + " " + y + " " + placementBoard.GetBoard()[x][y].securePoints);
                PutBackWarship(statek);
                return false;
            }
        }
        else
        {
            //Debug.Log("nie wybrano pola");
            return false;
        }
    }

    public void UndoneLastWarship()
    {
        //placementBoard.IsEverythingOk();
        if (listOfWarshipsOnBoard.Count != 0 && listOfWarships.Count!=0)
        {
            RemoveWarshipFromList();
            PutBackWarshipFromBoard();
        }
    }
    private void PutBackWarshipFromBoard()
    {
        GameObject statek = listOfWarshipsOnBoard[listOfWarshipsOnBoard.Count - 1];
        listOfWarshipsOnBoard.RemoveAt(listOfWarshipsOnBoard.Count-1);
        Destroy(statek);
    }

    private void RemoveWarshipFromList()
    {
        Warship statek = listOfWarships[listOfWarships.Count - 1];
        if (statek != null)
        {
            Debug.Log(statek.GetX()+";"+statek.GetY()+ " usuwam...");
            preparationBoard.RemoveWarship(statek);
            placementBoard.RemoveWarship(statek);
            PutBackWarship(statek);
            UpdateStatus(statek.warshipSize);
            Debug.Log("Statek został usunięty");
        }
        listOfWarships.RemoveAt(listOfWarships.Count-1);


    }
    public void WarshipPlacer(int shipSize, ViewFieldComponent field)
    {
        switch (shipSize)
        {
            case 4:
            {
                Vector3 pozycja = field.realPosition;
                var rotacja = field.realRotation;
                if (CheckIfOrientationIsHorizontal())
                {
                    pozycja.x = pozycja.x + 0.69f;
                    pozycja.y += 0.1f;
                    rotacja.z = 0f;
                    rotacja.z = -1.0f;
                    listOfWarshipsOnBoard.Add(GameObject.Instantiate(warship4HorizontalPrefab, pozycja, rotacja));
                }
                else
                {
                    pozycja.y = pozycja.y - 0.69f;
                    listOfWarshipsOnBoard.Add(GameObject.Instantiate(warship4VerticalPrefab, pozycja, rotacja));

                }
                break;
            }
            case 3:
            {
                Vector3 pozycja = field.realPosition;
                var rotacja = field.realRotation;
                if (CheckIfOrientationIsHorizontal())
                {
                    pozycja.x = pozycja.x + 0.46f;
                    rotacja.z = 0f;
                    rotacja.z = -1.0f;
                    listOfWarshipsOnBoard.Add(GameObject.Instantiate(warship3HorizontalPrefab, pozycja, rotacja));
                }
                else
                {
                    pozycja.y = pozycja.y - 0.46f;
                    listOfWarshipsOnBoard.Add(GameObject.Instantiate(warship3VerticalPrefab, pozycja, rotacja));

                }
                break;
                }
            case 2:
            {
                Vector3 pozycja = field.realPosition;
                var rotacja = field.realRotation;
                if (CheckIfOrientationIsHorizontal())
                {
                    pozycja.x = pozycja.x + 0.24f;
                    rotacja.z = 0f;
                    rotacja.z = -1.0f;
                    listOfWarshipsOnBoard.Add(GameObject.Instantiate(warship2HorizontalPrefab, pozycja, rotacja));
                }
                else
                {
                    pozycja.y = pozycja.y - 0.24f;
                    listOfWarshipsOnBoard.Add(GameObject.Instantiate(warship2VerticalPrefab, pozycja, rotacja));

                }
                break;
                }
            case 1:
            {
                    Vector3 pozycja = field.realPosition;
                    var rotacja = field.realRotation;
                    if (CheckIfOrientationIsHorizontal())
                    {
                        rotacja.z = 0f;
                        rotacja.z = -1.0f;
                        listOfWarshipsOnBoard.Add(GameObject.Instantiate(warship1HorizontalPrefab, pozycja, rotacja));
                    }
                    else
                    {
                        listOfWarshipsOnBoard.Add(GameObject.Instantiate(warship1VerticalPrefab, pozycja, rotacja));

                    }
                    break;
                }
            default:
            {
                //Debug.Log("Co ja tutaj robię ????");
                break;
            }
        }
    }
    private void PutBackWarship(Warship warship)
    {
        switch ((int)warship.warshipSize)
        {
            case 4:
            {
				allWarships [0].Add (warship);
                break;
            }
            case 3:
            {
				allWarships [1].Add (warship);
                break;
            }
            case 2:
            {
				allWarships [2].Add (warship);
                break;
            }
            case 1:
            {
				allWarships [3].Add (warship);
                break;
            }
            default:
            {
                //Debug.Log("That can't be DONE !!!");
                break;
            }
        }
    }


    private Warship CreateWarship(int size)
    {
        Warship statek = null;
        //Debug.Log("chosenWorshipSize to "+size);
        switch (size)
        {
            case 4:
            {
				if (allWarships[0].Count == 0)// warships4.Count==0)
                {
                    Debug.Log("Skończyły się statki rozmiaru "+size);
                    break;
                }
				statek = allWarships [0][0];//warships4[0];                    
				allWarships[0].Remove(statek);
      //          warships4.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 3:
            {
				if (allWarships[1].Count == 0)//  if (warships3.Count == 0)
                {
                    //Debug.Log("Skończyły się statki rozmiaru " + size);
                    break;
                }
				statek = allWarships [1][0];//warships3[0];
				allWarships[1].Remove(statek);
    //            warships3.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 2:
            {
				if (allWarships[2].Count == 0) //if (warships2.Count == 0)
                {
                    //Debug.Log("Skończyły się statki rozmiaru " + size);
                    break;
                }
				statek = allWarships [2][0];//warships2[0];
				allWarships[2].Remove(statek);
  //              warships2.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 1:
            {
				if (allWarships[3].Count == 0) //if (warships1.Count == 0)
                {
                    //Debug.Log("Skończyły się statki rozmiaru " + size);
                    break;
                }
				statek = allWarships [3][0];//warships1[0];
				allWarships[3].Remove(statek);
//				warships1.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            default:
            {
                //    Debug.Log("Co ja tutaj robię ????");
                break;
            }
        }      
        return statek;
    }

    public void ChooseWarship4()
    {
        chosenWarshipSize = (int)WarshipSize.FOUR;
        chosenWarshipOrientation = WarshipOrientation.VERTICAL;
        UpdateOrientation();
        // Debug.Log("Chosen Warship is "+chosenWarshipSize);
    }
    public void ChooseWarship3()
    {
        chosenWarshipSize = (int)WarshipSize.THREE;
        chosenWarshipOrientation = WarshipOrientation.VERTICAL;
        UpdateOrientation();
        //Debug.Log("Chosen Warship is " + chosenWarshipSize);
    }
    public void ChooseWarship2()
    {
        chosenWarshipSize = (int)WarshipSize.TWO;
        chosenWarshipOrientation = WarshipOrientation.VERTICAL;
        UpdateOrientation();
        //Debug.Log("Chosen Warship is " + chosenWarshipSize);
    }
    public void ChooseWarship1()
    {
        chosenWarshipSize = (int)WarshipSize.ONE;
        chosenWarshipOrientation = WarshipOrientation.VERTICAL;
        UpdateOrientation();
        //Debug.Log("Chosen Warship is " + chosenWarshipSize);
    }

    public void ChangeOrientation()
    {
        chosenWarshipOrientation= CheckIfOrientationIsHorizontal() ? WarshipOrientation.VERTICAL : WarshipOrientation.HORIZONTAL;
        UpdateOrientation();
        // Debug.Log("Orientacja statku zmieniona na "+chosenWarshipOrientation);
    }

    private void UpdateOrientation()
    {
        if (chosenWarshipOrientation.Equals(WarshipOrientation.HORIZONTAL)) {
            verticalButton.SetActive(false);
            horizontalButton.SetActive(true);
        }
        else
        {
            horizontalButton.SetActive(false);
            verticalButton.SetActive(true);
        }
    }

    private void UpdateStatus(WarshipSize size)
    {
        switch ((int)size)
        {
            case 4:
            {
                GameObject text = GameObject.FindWithTag("Warship4Text");
				text.GetComponent<Text>().text =  allWarships[0].Count + "/1";
                break;
            }
            case 3:
            {
                GameObject text = GameObject.FindWithTag("Warship3Text");
				text.GetComponent<Text>().text = allWarships[1].Count + "/2";
                    break;
            }
            case 2:
            {
                GameObject text = GameObject.FindWithTag("Warship2Text");
				text.GetComponent<Text>().text = allWarships[2].Count + "/3";
                    break;
            }
            case 1:
            {
                GameObject text = GameObject.FindWithTag("Warship1Text");
				text.GetComponent<Text>().text = allWarships[3].Count + "/4";
                    break;
            }
            default:
            {
                //Debug.Log("That can't be DONE !!!");
                break;
            }
        }
    }

    private bool CheckIfOrientationIsHorizontal()
    {
        return chosenWarshipOrientation.Equals(WarshipOrientation.HORIZONTAL);
    }


    public void StartGame(string sceneName) {
            Debug.Log("I wanna start");

        //if (true) {
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



