using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {

    public GameObject waterPrefab;

	// Use this for initialization
	void Start () {
        Board board = new Board();
        Board.SetWaterPrefab(waterPrefab);
        board.GenerateBoardOnScreen();
        board.GenerateMiniBoardOnScreen();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
