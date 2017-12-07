using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables {

	//BOARD
	public static readonly int defaultBoardSize = 10;
	public static float screenHorizontalOffset = -2f;
	public static float screenVerticalOffset = 4f;
	public static float miniBoardScreenVerticalOffset = -1.2f;

	//FIELD
	public static float fieldMargin = 0.05f;

	//WARSHIP
	public static int fieldsOccupiedByWarships = 20;
	public static WarshipOrientation defaultWarshipOrientation = WarshipOrientation.VERTICAL;
	public static int numberOfShipsOfSizeFour = 1;
	public static int numberOfShipsOfSizeThree = 2;
	public static int numberOfShipsOfSizeTwo = 3;
	public static int numberOfShipsOfSizeOne = 4;

	//ANIMATION
	public static string animationTriggerHit = "Hit";
	public static string animationTriggerMiss = "Miss";
	public static string animationTriggerBack = "Sinked";
	public static string animationTriggerFire = "Fire";

	//TIME
	public static int TIME_UNTIL_NEXT_SCENE_LOAD = 5;

    //AVARAGE BOT LVL
    public static float EASY_PRECISION = 0.5f;
    public static float MEDIUM_PRECISION = 0.75f;
    public static float HARD_PRECISION = 1.0f;

	//SOUND
	public static string BOOM_SOUND_PATH = "Sound/boom";
	public static string SPLASH_SOUND_PATH = "Sound/splash";
	public static string WIN_SOUND_PATH = "Sound/win";
	public static string DEFEAT_SOUND_PATH = "Sound/defeat";

	//PREFAB PATHS
	public static string WATER_PREFAB_PATH = "Prefab/Water";
	public static string WARSHIP_PREFAB_PATH = "Prefab/warship1";
	public static string ANIMATION_HOLDER_PATH = "Prefab/AnimationHolder";

	//ANDROID TOAST NAME
	public static string UNITY_PLAYER_PATH = "com.unity3d.player.UnityPlayer";
	public static string CURRENT_ACTIVITY = "currentActivity";
	public static string GET_APPLICATION_CONTEXT = "getApplicationContext";
	public static string TOAST_PATH = "android.widget.Toast";
	public static string JAVA_STRING_PATH = "java.lang.String";
	public static string TOAST_LENGTH = "LENGTH_SHORT";

	//TOAST MESSAGE
	public static string CHOOSE_DIFFRENT_FIELD = "Choose a different field";
	public static string NO_MORE_SHIPS_OF_SIZE = "There are no more ships size ";
	public static string NO_ALL_SHIPS_ON_BORAD = "You have not put all the ships on the board yet";
	public static string INCORRECT_SHOT = "This shot is incorrect";

	//SETTING FILE PATH
	public static string SETTINGS_PATH = Application.persistentDataPath + "/settings.dat";

	//MUSIC NAME
	public static string MENU_MUSIC = "MenuMusic";
	public static string PREPARATION_MUSIC = "PreparationMusic";
	public static string GAMEPLAY_MUSIC = "GameplayMusic";

	//MUSIC PATH
	public static string MENU_MUSIC_PATH = "Music/MenuScene";
	public static string PREPARATION_MUSIC_PATH = "Music/Preparation";
	public static string GAMEPLAY_MUSIC_PATH = "Music/Gameplay";
}
