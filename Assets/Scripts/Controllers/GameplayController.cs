using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {

    public GameObject waterPrefab;

    public static List<Player> players;

    private enum PlayerType {HUMAN, BOT };



	// Use this for initialization
	void Start () {
        Board board = new Board();
        Board.SetWaterPrefab(waterPrefab);
        board.GenerateBoardOnScreen();
        board.GenerateMiniBoardOnScreen();
        System.Random rnd = new System.Random();
        PlayerType startingPlayer = (PlayerType)rnd.Next(2);
        players[(int)startingPlayer].YourTurn();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void setPlayers(Player human, Player bot)
    {
        players = new List<Player>
        {
            human,
            bot
        };
    }

    private PlayerType NextPlayer(PlayerType player)
    {
        return (PlayerType)((int)player + 1);
    }

    //Tu przekierować kliknięcie w pole przeciwnika, argument dostosować, do potrzeb, byleby posiadał w środku współrzędne
    public void AttackEnemy(int x, int y)
    {

    }

    //Funkcja do malowania statków na małej planszy
    public void SetMyShips(List<Warship> ship)
    {

    }

}
