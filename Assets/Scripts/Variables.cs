using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables {

	//BOARD
	public static readonly int defaultBoardSize = 10;
	public static float screenHorizontalOffset = -2f;
	public static float screenVerticalOffset = 4f;
	public static float miniBoardScreenVerticalOffset = -3f;

	//FIELD
	public static float fieldMargin = 0.05f;

	//WARSHIP
	public static int fieldsOccupiedByWarships = 20;
	public static WarshipOrientation defaultWarshipOrientation = WarshipOrientation.VERTICAL;
	public static int numberOfShipsOfSizeFour = 1;
	public static int numberOfShipsOfSizeThree = 2;
	public static int numberOfShipsOfSizeTwo = 3;
	public static int numberOfShipsOfSizeOne = 4;

}
