using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PreparationController : MonoBehaviour {

    public Board preparationBoard;
    public GameObject fieldPrefab;
    public GameObject shotFieldPrefab;
    public Player botPlayer;
    public Player humanPlayer;

    private WarshipCreator warshipCreator;
    private WarshipOrientation chosenWarshipOrientation;
    private Field chosenField;
    private List<Warship> warships4;
    private List<Warship> warships3;
    private List<Warship> warships2;
    private List<Warship> warships1;

    public int chosenWarshipSize;
    // Use this for initialization
    void Start()
    {
        preparationBoard = new Board();
        Board.SetWaterPrefab(fieldPrefab);
        preparationBoard.GenerateBoard();
        warshipCreator = new WarshipCreator();
        botPlayer = BotCreator.CreateBotPlayer();
        humanPlayer = new HumanPlayer();
        chosenWarshipOrientation = WarshipOrientation.VERTICAL;
        warships4 = warshipCreator.GetWarships(WarshipSize.FOUR);
        warships3 = warshipCreator.GetWarships(WarshipSize.THREE);
        warships2 = warshipCreator.GetWarships(WarshipSize.TWO);
        warships1 = warshipCreator.GetWarships(WarshipSize.ONE);
    }

    void Update()
    {
        Debug.Log("chosenWorshipSize to " + chosenWarshipSize);
    }
    public void ChooseField(Field field)
    {
        chosenField = field;
        if (chosenField!=null)
        {
            Debug.Log("wybrano pole "+chosenField.gridPosition.x+";"+chosenField.gridPosition.y);
            var statek = CreateWarship();
            if (statek!=null)
            {
                preparationBoard.PlaceWarship(statek);
            }
            Debug.Log("pusto");
            
        }
        else
        {
            Debug.Log("nie wybrano pola");
        }
    }

    private Warship CreateWarship()
    {
        Warship statek;
        Debug.Log("chosenWorshipSize to "+chosenWarshipSize);
        switch (chosenWarshipSize)
        {
            case 4:
            {
                statek = warships4[1];
                warships4.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 3:
            {
                statek = warships3[1];
                warships3.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                    statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 2:
            {
                statek = warships2[1];
                warships2.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
            case 1:
            {
                statek = warships1[1];
                warships1.Remove(statek);
                statek.SetPosition(Convert.ToInt32(chosenField.gridPosition.x), Convert.ToInt32(chosenField.gridPosition.y));
                statek.SetWarshipOrientation(chosenWarshipOrientation);
                break;
            }
                default:
                    statek = null;
                    break;
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



