using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarshipPlacer  {

	public static List<GameObject> warshipsOnBoard;
	public static List<List<GameObject>> warshipPrefabList;
	public AndroidToast androidToast;

	public static WarshipOrientation chosenWarshipOrientation = WarshipOrientation.VERTICAL;
	public static int chosenWarshipSize;

	public static List<List<Warship>> allWarships;
	public static List<Warship> warshipsAddedToBoard;
	private static WarshipCreator warshipCreator;

	private float[] warshipOffsetHorizontal = {0.0f, 0.24f, 0.46f, 0.69f}; 
	private float[] warshipOffsetVertical = {0.0f, -0.24f, -0.46f, -0.69f}; 


	public WarshipPlacer(){
		warshipsOnBoard = new List<GameObject>();
		warshipsAddedToBoard = new List<Warship>();
		warshipCreator = new WarshipCreator ();
		androidToast = new AndroidToast ();
		allWarships = warshipCreator.GetWarshipsList ();
	}

	public void SetCurrentlySelectedSize(int warshipSize){
		chosenWarshipSize = warshipSize;
	}

	public void SetWarshipPrefabList(List<List<GameObject>> warshipsPrefab){
		warshipPrefabList = warshipsPrefab;
	}

	public void SetWarshipChosenOrientation(WarshipOrientation orientation){
		chosenWarshipOrientation = orientation;
	}

	public WarshipOrientation GetWarshipChosenOrientation(){
		return chosenWarshipOrientation;
	}

	public int GetNumberOfWarshipOnBoard(){
		return warshipsOnBoard.Count;
	}

	public void CreatePrefabList(){
		warshipPrefabList = new List<List<GameObject>> ();
		for (int i = 1; i <= 4; i++) {
			List<GameObject> prefabs = new List<GameObject> ();
			prefabs.Add (Resources.Load (GetPrefabPath (i, "vertical")) as GameObject);
			prefabs.Add (Resources.Load (GetPrefabPath (i, "horizontal")) as GameObject);
			warshipPrefabList.Add (prefabs);
		}
	}

	private string GetPrefabPath(int i, string type){
		return "Prefab/warship" + i.ToString() + "_" + type; 
	}
		
	public void TryPutWarshipOnField(ViewFieldComponent field){
		if (field!=null){
			Warship warship = GetWarshipOfChosenSize();
			if (warship != null) {
				warship.SetPosition (Convert.ToInt32 (field.gridPosition.x), Convert.ToInt32 (field.gridPosition.y));
				warship.SetWarshipOrientation (chosenWarshipOrientation);
				if (PlacementManager.CheckIfPlayerCanPutWarshipOnThisPosition (PreparationController.placementBoard, warship)) {
					PutWarshipOnBoard (warship, field);
					allWarships [chosenWarshipSize - 1].RemoveAt (0);
					UpdateStatus (warship.warshipSize);
				} else {
					androidToast.CreateToastWithMessage (Variables.CHOOSE_DIFFRENT_FIELD);
				}
			} else {
				androidToast.CreateToastWithMessage (Variables.NO_MORE_SHIPS_OF_SIZE + chosenWarshipSize.ToString ());
			}
		}
	}


	public Warship GetWarshipOfChosenSize(){
		int index = chosenWarshipSize - 1;
		return allWarships[index].Count != 0 ? allWarships[index][0] : null;
	}

	public void PutWarshipOnBoard(Warship warship, ViewFieldComponent field){
		PreparationController.placementBoard.SetWarship(warship);
		PreparationController.preparationBoard.SetWarship(warship);
		PutWarship(warship, field);
		warshipsAddedToBoard.Add (warship);
	}

	public void PutWarship(Warship warship, ViewFieldComponent field){
		Vector2 position = field.realPosition;
		Quaternion rotation = field.realRotation;
		foreach (WarshipSize size in System.Enum.GetValues(typeof(WarshipSize))) {
			if (warship.warshipSize.Equals(size)){
				int index = warship.GetSize() - 1;
				if (warship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL)) {
					position.x += warshipOffsetHorizontal [index];
					rotation.z = -1f;
					warshipsOnBoard.Add (GameObject.Instantiate (warshipPrefabList [index] [1], position, rotation));
				} else {
					position.y += warshipOffsetVertical [index];
					warshipsOnBoard.Add (GameObject.Instantiate (warshipPrefabList [index] [0], position, rotation));
				}
			}
		}
	}
		
		
	public bool CheckIfOrientationIsHorizontal(){
		return chosenWarshipOrientation.Equals(WarshipOrientation.HORIZONTAL);
	}

	private void UpdateStatus(WarshipSize warshipSize){
		foreach(WarshipSize size in System.Enum.GetValues(typeof(WarshipSize))){
			if (warshipSize.Equals (size)) {
				UpdateInfo (warshipSize);
			}
		}
	}

	private void UpdateInfo(WarshipSize warshipSize){
		int index = (int)warshipSize - 1;
		string tagToFind = "Warship" + (index+1).ToString () + "Text";
		GameObject text = GameObject.FindWithTag (tagToFind);
		text.GetComponent<Text>().text = allWarships[index].Count + "/" + (-index+4).ToString();
	}

	public void RemoveWarshipFromBoard(){
		int index = warshipsAddedToBoard.Count - 1;
		Warship warship = GetLastAddedWarship (index);
		RemoveFromModelBoard (warship, index);
		RemoveFromViewBoard (index);
		UpdateStatus (warship.warshipSize);
	}

	private Warship GetLastAddedWarship(int index){
		return index >= 0 ? warshipsAddedToBoard [index] : null;
	}

	private void RemoveFromModelBoard(Warship warship, int index){
		PreparationController.preparationBoard.RemoveWarship(warship);
		PreparationController.placementBoard.RemoveWarship(warship);
		warshipsAddedToBoard.RemoveAt (index);
		UpdateAllWarshipList (warship);
	}

	private void UpdateAllWarshipList(Warship warship){
		int index = warship.GetSize () - 1;
		allWarships [index].Add (warship);
	}

	private void RemoveFromViewBoard(int index){
		GameObject warship = warshipsOnBoard [index];
		warshipsOnBoard.RemoveAt (index);
		GameObject.Destroy (warship);
	}

}
