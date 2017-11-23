using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PreparationController : MonoBehaviour {

    public static Board preparationBoard;
    public GameObject fieldPrefab;
    public GameObject warship4Prefab;
    public GameObject warship3Prefab;
    public GameObject warship2Prefab;
    public GameObject warship1Prefab;
    public GameObject shotFieldPrefab;
    public static Player botPlayer;
    public static Player humanPlayer;
    private static Board humanPlayerBoard;
    private static Board botPlayerBoard;

    private static WarshipCreator warshipCreator;
    private static WarshipOrientation chosenWarshipOrientation;
    private static Field chosenField;
    private static List<Warship> warships4;
    private static List<Warship> warships3;
    private static List<Warship> warships2;
    private static List<Warship> warships1;

    public static int chosenWarshipSize;
    // Use this for initialization
    void Start()
    {
        preparationBoard = new Board();
        Board.SetWaterPrefab(fieldPrefab);
        preparationBoard.GenerateBoardOnScreen();
        warshipCreator = new WarshipCreator();
        botPlayer = BotCreator.CreateBotPlayer();
        humanPlayer = new HumanPlayer();
        chosenWarshipOrientation = WarshipOrientation.VERTICAL;
        warships4 = warshipCreator.GetWarships(WarshipSize.FOUR);
        warships3 = warshipCreator.GetWarships(WarshipSize.THREE);
        warships2 = warshipCreator.GetWarships(WarshipSize.TWO);
        warships1 = warshipCreator.GetWarships(WarshipSize.ONE);
        botPlayer.ArrangeBoard();
        botPlayerBoard = botPlayer.GetPlayerBoard();
    }

    public void ChooseField(Field field)
    {
        chosenField = field;
        if (chosenField!=null)
        {
            Debug.Log("wybrano pole "+chosenField.gridPosition.x+";"+chosenField.gridPosition.y);
            var statek = CreateWarship(chosenWarshipSize);
            Debug.Log(statek.warshipSize);
                preparationBoard.PlaceWarship(statek);
            WarshipPlacer((int)statek.warshipSize,chosenField);
            preparationBoard.DisplayBoard();
        }
        else
        {
            Debug.Log("nie wybrano pola");
        }
    }

    private void WarshipPlacer(int shipSize, Field field)
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
                    pozycja.y = pozycja.y + 0.69f;
                    Warship obrazekWarship = GameObject.Instantiate(warship4Prefab, pozycja, rotacja).GetComponent<Warship>();
                    
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
                    pozycja.y = pozycja.y + 0.46f;
                    Warship obrazekWarship = GameObject.Instantiate(warship3Prefab, pozycja, rotacja).GetComponent<Warship>();

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
                    pozycja.y = pozycja.y + 0.24f;
                    Warship obrazekWarship = GameObject.Instantiate(warship2Prefab, pozycja, rotacja).GetComponent<Warship>();

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
                        Warship obrazekWarship = GameObject.Instantiate(warship1Prefab, pozycja, rotacja).GetComponent<Warship>();

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
    private Warship CreateWarship(int size)
    {
        Warship statek;
        Debug.Log("chosenWorshipSize to "+size);
        switch (size)
        {
            case 4:
            {
                statek = warships4[0];                    
                warships4.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 3:
            {
                statek = warships3[0];
                warships3.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 2:
            {
                statek = warships2[0];
                warships2.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 1:
            {
                statek = warships1[0];
                warships1.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            default:
            {
                statek = null;
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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}



