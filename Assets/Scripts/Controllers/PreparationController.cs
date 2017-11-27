using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PreparationController : MonoBehaviour {

    public static ViewBoard preparationBoard;
    public static PlacementBoard placementBoard;
    public GameObject fieldPrefab;
    public GameObject warship4Prefab;
    public GameObject warship3Prefab;
    public GameObject warship2Prefab;
    public GameObject warship1Prefab;
    public GameObject shotFieldPrefab;
    public static Player botPlayer;
    public static Player humanPlayer;

    private static WarshipCreator warshipCreator;
    private static WarshipOrientation chosenWarshipOrientation;
    private static ViewFieldComponent chosenField;
    private static List<Warship> warships4;
    private static List<Warship> warships3;
    private static List<Warship> warships2;
    private static List<Warship> warships1;
    private WarshipsContainer botPlayerShips;
    private WarshipsContainer humanPlayerShips;

    public static int chosenWarshipSize;
    // Use this for initialization
    void Start()
    {
        preparationBoard = new ViewBoard();
        placementBoard = new PlacementBoard();
        ViewBoard.SetWaterPrefab(fieldPrefab);
        preparationBoard.GenerateBoardOnScreen();
        warshipCreator = new WarshipCreator();
        botPlayer = BotCreator.CreateBotPlayer();

        //humanPlayer = BotCreator.CreateBotPlayer();
        humanPlayer = new HumanPlayer();

        chosenWarshipOrientation = WarshipOrientation.VERTICAL;
        warships4 = warshipCreator.GetWarships(WarshipSize.FOUR);
        warships3 = warshipCreator.GetWarships(WarshipSize.THREE);
        warships2 = warshipCreator.GetWarships(WarshipSize.TWO);
        warships1 = warshipCreator.GetWarships(WarshipSize.ONE);

        botPlayer.ArrangeBoard();
        //humanPlayer.ArrangeBoard();


        botPlayerShips = botPlayer.GetPlayerShips();
        //humanPlayerShips = humanPlayer.GetPlayerShips();

        ChooseWarship4();
	}

    public bool ChooseFieldComponent(ViewFieldComponent field)
    {
        chosenField = field;
        if (chosenField!=null)
        {
            Debug.Log("wybrano pole "+chosenField.gridPosition.x+";"+chosenField.gridPosition.y);
            var statek = CreateWarship(chosenWarshipSize);
            if (statek==null)
            {
                
                return false;
            }
            Debug.Log(statek.warshipSize);
            if (PlacementManager.CheckIfPlayerCanPutWarshipOnThisPosition(placementBoard, statek))
            {
                preparationBoard.SetWarship(statek);
                placementBoard.SetWarship(statek);
                WarshipPlacer((int)statek.warshipSize, chosenField);
                preparationBoard.DisplayBoard();
                return true;
            }
            else
            {
                Debug.Log("wybierz inne miejsce");
                PutBackWarship(statek);
                return false;
            }
        }
        else
        {
            Debug.Log("nie wybrano pola");
            return false;
        }
    }

    private void WarshipPlacer(int shipSize, ViewFieldComponent field)
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
                    rotacja.z = 0f;
                    rotacja.z = -1.0f;
                    GameObject.Instantiate(warship4Prefab, pozycja, rotacja);
                }
                else
                {
                    pozycja.y = pozycja.y - 0.69f;
                    GameObject.Instantiate(warship4Prefab, pozycja, rotacja);

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
                    GameObject.Instantiate(warship3Prefab, pozycja, rotacja);
                }
                else
                {
                    pozycja.y = pozycja.y - 0.46f;
                    GameObject.Instantiate(warship3Prefab, pozycja, rotacja);

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
                    GameObject.Instantiate(warship2Prefab, pozycja, rotacja);
                }
                else
                {
                    pozycja.y = pozycja.y - 0.24f;
                    GameObject.Instantiate(warship2Prefab, pozycja, rotacja);

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
                        GameObject.Instantiate(warship1Prefab, pozycja, rotacja);
                    }
                    else
                    {
                        GameObject.Instantiate(warship1Prefab, pozycja, rotacja);

                    }
                    break;
                }
            default:
            {
                Debug.Log("Co ja tutaj robię ????");
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
                warships4.Add(warship);
                break;
            }
            case 3:
            {
                warships3.Add(warship);
                    break;
            }
            case 2:
            {
                warships2.Add(warship);
                    break;
            }
            case 1:
            {
                warships1.Add(warship);
                    break;
            }
            default:
            {
                Debug.Log("That can't be DONE !!!");
                break;
            }
        }
    }


    private Warship CreateWarship(int size)
    {
        Warship statek = null;
        Debug.Log("chosenWorshipSize to "+size);
        switch (size)
        {
            case 4:
            {
                if (warships4.Count==0)
                {
                    Debug.Log("Skończyły się statki rozmiaru "+size);
                    break;
                }
                statek = warships4[0];                    
                warships4.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 3:
            {
                if (warships3.Count == 0)
                {
                    Debug.Log("Skończyły się statki rozmiaru " + size);
                    break;
                }
                    statek = warships3[0];
                warships3.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 2:
            {
                if (warships2.Count == 0)
                {
                    Debug.Log("Skończyły się statki rozmiaru " + size);
                    break;
                }
                    statek = warships2[0];
                warships2.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 1:
            {
                if (warships1.Count == 0)
                {
                    Debug.Log("Skończyły się statki rozmiaru " + size);
                    break;
                }
                    statek = warships1[0];
                warships1.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            default:
            {
                    Debug.Log("Co ja tutaj robię ????");
                break;
            }
        }      
        return statek;
    }

    public void ChooseWarship4()
    {
        chosenWarshipSize = (int)WarshipSize.FOUR;
        Debug.Log("Chosen Warship is "+chosenWarshipSize);
    }
    public void ChooseWarship3()
    {
        chosenWarshipSize = (int)WarshipSize.THREE;
        Debug.Log("Chosen Warship is " + chosenWarshipSize);
    }
    public void ChooseWarship2()
    {
        chosenWarshipSize = (int)WarshipSize.TWO;
        Debug.Log("Chosen Warship is " + chosenWarshipSize);
    }
    public void ChooseWarship1()
    {
        chosenWarshipSize = (int)WarshipSize.ONE;
        Debug.Log("Chosen Warship is " + chosenWarshipSize);
    }

    public void ChangeOrientation()
    {
        chosenWarshipOrientation= CheckIfOrientationIsHorizontal() ? WarshipOrientation.VERTICAL : WarshipOrientation.HORIZONTAL;
        Debug.Log("Orientacja statku zmieniona na "+chosenWarshipOrientation);
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



