using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    public int boardSize = 10;

    private GameObject waterPrefab;
    private List<List<Field>> board;
    private float screenHorizontalOffset = -2f;
    private float fieldMargin = 0.05f;

    public Board(GameObject water) {
        waterPrefab = water;
        GenerateBoard();
    }

    private void GenerateBoard() {
        board = new List<List<Field>>();
        float fieldSize = waterPrefab.GetComponent<BoxCollider2D>().size.x + fieldMargin; 
        for (int i = 0; i < boardSize; i++) {
            List<Field> row = new List<Field>();
            for (int j = 0; j < boardSize; j++)            {
                Field field = GameObject.Instantiate(waterPrefab, new Vector2(screenHorizontalOffset + i * fieldSize, j * fieldSize), Quaternion.Euler(new Vector2())).GetComponent<Field>();
                field.gridPosition = new Vector2(i,j);
                row.Add(field);
            }
            board.Add(row);
        }
    }

    public List<List<Field>> GetBoard() {
        return board;
    }

    public Field GetField(int column, int row) {
        return board[column][row];
    }

    public void SetShotResult(int column, int row, ShotRaport shotRaport) {
       
    }

    public void PlaceWarship(Warship warship) {

    }


}
