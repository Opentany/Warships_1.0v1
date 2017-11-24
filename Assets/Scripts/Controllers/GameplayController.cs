using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {

    public GameObject waterPrefab;

    public static List<Player> players;

    private enum PlayerType {HUMAN, BOT };

    private static PlayerType activePlayer;

    public Board board;

    public bool activeHuman;

    public static bool ready = false;

    // Use this for initialization
    void Start() {
        Debug.Log("Prepare Game");
        foreach (Player player in players)
        {
            player.SetGameController(this);
        }
        Debug.Log("Controllers Set");
        board = new Board();
        Board.SetWaterPrefab(waterPrefab);
        board.GenerateBoardOnScreen();
        board.GenerateMiniBoardOnScreen();
        SetMyShips(players[0].GetPlayerShips().GetWarships());
        System.Random rnd = new System.Random();
        activePlayer = (PlayerType)0;
        activeHuman = (activePlayer.Equals(PlayerType.HUMAN));
        Debug.Log("Start Game");
        ready = true;
    }

    public void GameLoop(){

    	while (!players[0].CheckIfYouLose() || !players[1].CheckIfYouLose()){

    	}
    }


    public static void setPlayers(Player human, Player bot)
    {
        Debug.Log("Setting players");
        players = new List<Player>
        {
            human,
            bot
        };
    }

    private PlayerType NextPlayer(PlayerType player)
    {
        return (PlayerType)(((int)player + 1)%2);
    }

    //Tu przekierować kliknięcie w pole przeciwnika, argument dostosować, do potrzeb, byleby posiadał w środku współrzędne
    public void AttackEnemy(int x, int y)
    {
        Debug.Log(activeHuman);
        Debug.Log(activePlayer);
        if(activeHuman)
            ShotOpponent(x, y);

    }

    public void ShotOpponent(int x, int y)
    {
        Debug.Log("shot " + x + " " + y);
        int badShotCounter = 0; 
        PlayerType opponent = NextPlayer(activePlayer);
        try
        {
            ShotRaport raport = new ShotRaport(x, y, players[(int)opponent].playerBoard);
            players[(int)activePlayer].SetPlayerShotResult(raport);
            players[(int)opponent].TakeOpponentShot(raport);
            if (activeHuman)
                board.ApplyMyShot(raport);
            else
                board.ApplyOpponentShot(raport);
            if (players[(int)activePlayer].CheckIfYouLose())
                PlayerWon(players[(int)opponent]);
            if (raport.GetShotResult().Equals(DmgDone.MISS))
            {
                activePlayer = NextPlayer(activePlayer);
                opponent = NextPlayer(opponent);
                activeHuman = !activeHuman;
            }
            players[(int)activePlayer].YourTurn();

        }
        catch(IllegalShotException badShot)
        {
            Debug.Log(badShot);
            badShotCounter++;
            if (badShotCounter == 3)
            {
                activePlayer = NextPlayer(activePlayer);
                opponent = NextPlayer(opponent);
                activeHuman = !activeHuman;
            }
            players[(int)activePlayer].YourTurn();
        }
    }

    public void SetMyShips(List<Warship> ships)
    {
        board.SetWarshipOnMiniBoard(ships);
    }


    public void PlayerWon(Player player)
    {
        Debug.Log("Wygrana");
    }

}
