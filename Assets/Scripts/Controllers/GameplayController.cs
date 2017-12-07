using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameplayController: MonoBehaviour{

    public GameObject waterPrefab;
	public GameObject animationHolder;
	public GameObject winText;
	public GameObject loseText;
	public GameObject player1WarshipsLeft;
	public GameObject player2WarshipsLeft;
	private AndroidToast androidToast;

    public static List<Player> players;

    private enum PlayerType {HUMAN, BOT };

    private static PlayerType activePlayer;

    public ViewBoard board;

    public bool activeHuman;

    public static bool ready = false;

    // Use this for initialization
    void Start() {
		SettingsController.SetMusicVolumeInScene ();
        Debug.Log("Prepare Game");
		androidToast = new AndroidToast ();
        foreach (Player player in players)
        {
            player.SetGameController(this);
        }
        Debug.Log("Controllers Set");
        board = new ViewBoard();
        ViewBoard.SetWaterPrefab(waterPrefab);
		ViewBoard.SetAnimationHolder (animationHolder);
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
			if (activeHuman){
                board.ApplyMyShot(raport);
			}else{
                board.ApplyOpponentShot(raport);
			}
            if (players[(int)opponent].CheckIfYouLose()){
                PlayerWon(players[(int)activePlayer]);
                return;
            }
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
			androidToast.CreateToastWithMessage (Variables.INCORRECT_SHOT);
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

	public void UpdatePlayerCounter(Player player, DmgDone shotResult){
		if (shotResult.Equals (DmgDone.HIT) || shotResult.Equals (DmgDone.SINKED)) {
			if(player.Equals(players[0])){
				player1WarshipsLeft.GetComponent<Text> ().text = player.GetNumberOfRemainingWarship ().ToString();
			}
			else{
				player2WarshipsLeft.GetComponent<Text> ().text = player.GetNumberOfRemainingWarship ().ToString();

			}
		}
	}

    public void SetMyShips(List<Warship> ships)
    {
        board.SetWarshipOnMiniBoard(ships);
    }


	public void PlayerWon(Player player)
    {
		Canvas canvas = this.GetComponent<Canvas> ();
	
		if (players [0].Equals (player)) {
			winText.SetActive (true);
			AudioClip audio = Resources.Load (Variables.WIN_SOUND_PATH) as AudioClip;
			AudioSource.PlayClipAtPoint (audio, Vector2.zero, SettingsController.GetSoundVolume());
		} else {
			loseText.SetActive (true);
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

}
