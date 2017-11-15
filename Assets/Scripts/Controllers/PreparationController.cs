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
    // Use this for initialization
    void Start()
    {
        preparationBoard = new Board();
        Board.SetWaterPrefab(fieldPrefab);
        preparationBoard.GenerateBoard();
        warshipCreator = new WarshipCreator();
        botPlayer = BotCreator.CreateBotPlayer();
        humanPlayer = new HumanPlayer();

    }

    private void CreateWarships() {

    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}



