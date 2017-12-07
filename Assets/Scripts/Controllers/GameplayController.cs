﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameplayController: MonoBehaviour{

    public GameObject waterPrefab;
	public GameObject animationHolder;
	public GameObject winText;
	public GameObject loseText;
    private int player1WarshipsLeftCounter;
	public GameObject player1WarshipsLeft;
    private int player2WarshipsLeftCounter;
	public GameObject player2WarshipsLeft;
	public GameObject victoryPicture;
	public GameObject deafeatPicture;

	private AndroidToast androidToast;

    public static List<Player> players;

    private enum PlayerType {DEVICE_HUMAN, OTHER_PLAYER };

    private static PlayerType activePlayer;

    public bool activeDeviceHuman;

    public static bool ready = false;

	private static bool isEndOfGame = false;

    // Use this for initialization
    void Start() {
		SettingsController.SetMusicVolumeInScene ();
		MusicController.SetActualMusic (Variables.GAMEPLAY_MUSIC);
        Debug.Log("Prepare Game");
		isEndOfGame = false;
		androidToast = new AndroidToast ();
        ViewBoard viewBoard = new ViewBoard();
        ViewBoard.SetWaterPrefab(waterPrefab);
        ViewBoard.SetAnimationHolder(animationHolder);
        viewBoard.GenerateBoardOnScreen();
        viewBoard.GenerateMiniBoardOnScreen();
        player1WarshipsLeftCounter = Variables.fieldsOccupiedByWarships;
        player2WarshipsLeftCounter = Variables.fieldsOccupiedByWarships;
        foreach (DevicePlayer player in players)
        {
            player.SetGameController(this);
            player.playerBoard.IsEverythingOk();
        }
        Debug.Log("Controllers Set");
        HumanPlayer.SetViewBoard(viewBoard);
        ((HumanPlayer) players[0]).SetShipsOnBoard();     
        System.Random rnd = new System.Random();
        activePlayer = (PlayerType) rnd.Next(2);
        activeDeviceHuman = (activePlayer.Equals(PlayerType.DEVICE_HUMAN));
        Debug.Log("Start Game");
        ready = true;
        players[(int)activePlayer].YourTurn();
    }

    public static void setPlayers(HumanPlayer deviceHuman, Player otherPlayer)
    {
        Debug.Log("Setting players");
        players = new List<Player>
        {
            deviceHuman,
            otherPlayer
        };
    }

    private PlayerType NextPlayer(PlayerType player)
    {
        return (PlayerType)(((int)player + 1)%2);
    }

    //Tu przekierować kliknięcie w pole przeciwnika, argument dostosować, do potrzeb, byleby posiadał w środku współrzędne
    public void AttackEnemy(int x, int y)
    {
        Debug.Log(activeDeviceHuman);
        Debug.Log(activePlayer);
        if(activeDeviceHuman)
            ShotOpponent(x, y);
    }

    public void ShotOpponent(int x, int y)
    {
        int badShotCounter = 0; 
        PlayerType opponent = NextPlayer(activePlayer);
        try
        {
            ShotRaport raport = players[(int)opponent].TakeOpponentShot(new Position(x,y));
            players[(int)activePlayer].SetPlayerShotResult(raport);
            bool activeWon = UpdatePlayerCounter(players[(int)opponent], raport.GetShotResult());
            if (activeWon){
                PlayerWon(players[(int)activePlayer]);
                return;
            }
            if (raport.GetShotResult().Equals(DmgDone.MISS))
            {
                activePlayer = NextPlayer(activePlayer);
                opponent = NextPlayer(opponent);
                activeDeviceHuman = !activeDeviceHuman;
            }
            players[(int)activePlayer].YourTurn();

        }
        catch(IllegalShotException badShot)
        {
			androidToast.CreateToastWithMessage (Variables.INCORRECT_SHOT);
            Debug.Log(badShot);
            badShotCounter++;
            if (badShotCounter == 3)
            {
                activePlayer = NextPlayer(activePlayer);
                opponent = NextPlayer(opponent);
                activeDeviceHuman = !activeDeviceHuman;
            }
            players[(int)activePlayer].YourTurn();
        }
    }

    public bool UpdatePlayerCounter(Player player, DmgDone shotResult){
		if (shotResult.Equals (DmgDone.HIT) || shotResult.Equals (DmgDone.SINKED)) {
			if(player.Equals(players[0])){
                player1WarshipsLeftCounter--;
                Debug.Log(player1WarshipsLeft);
                Debug.Log(player1WarshipsLeft.GetComponent<Text>());
                Debug.Log(player1WarshipsLeft.GetComponent<Text>().text);
                player1WarshipsLeft.GetComponent<Text>().text = ""+player1WarshipsLeftCounter;
                return player1WarshipsLeftCounter == 0;
			}
			else{
                player2WarshipsLeftCounter--;
                Debug.Log(player2WarshipsLeft);
                Debug.Log(player2WarshipsLeft.GetComponent<Text>());
                Debug.Log(player2WarshipsLeft.GetComponent<Text>().text);
                player2WarshipsLeft.GetComponent<Text>().text = "" + player2WarshipsLeftCounter;
                return player2WarshipsLeftCounter == 0;
            }
		}
        return false;
	}

	public void PlayerWon(Player player)
    {
		Canvas canvas = this.GetComponent<Canvas> ();
		isEndOfGame = true;
		if (players [0].Equals (player)) {
			victoryPicture.SetActive (true);
			AudioClip audio = Resources.Load (Variables.WIN_SOUND_PATH) as AudioClip;
			AudioSource.PlayClipAtPoint (audio, Vector2.zero, SettingsController.GetSoundVolume());
		} else {
			deafeatPicture.SetActive (true);
			AudioClip audio = Resources.Load (Variables.DEFEAT_SOUND_PATH) as AudioClip;
			AudioSource.PlayClipAtPoint (audio, Vector2.zero, SettingsController.GetSoundVolume());
		}
        Debug.Log("Wygrana");
		StartCoroutine (Won ());
    }

	private IEnumerator Won(){
		yield return new WaitForSeconds(Variables.TIME_UNTIL_NEXT_SCENE_LOAD);
		SceneManager.LoadScene("MenuScene");
	}

	public static bool IsGameEnd(){
		return isEndOfGame;
	}

}
