using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    public int boardSize = 10;

    private static GameObject waterPrefab;
    private List<List<Field>> board;
    private float screenHorizontalOffset = -2f;
    private float fieldMargin = 0.05f;
    
    public Board() {
        board = new List<List<Field>>();
    }

    public void GenerateBoard() {
        board = new List<List<Field>>();
        float fieldSize = waterPrefab.GetComponent<BoxCollider2D>().size.x + fieldMargin; 
        for (int i = 0; i < boardSize; i++) {
            List<Field> row = new List<Field>();
            for (int j = 0; j < boardSize; j++)            {
                Field field = GameObject.Instantiate(waterPrefab, new Vector2(screenHorizontalOffset + i * fieldSize, j * fieldSize), Quaternion.Euler(new Vector2())).GetComponent<Field>();
                field.gameObject.layer = 1;
                field.gridPosition = new Vector2(i,j);
                row.Add(field);
            }
            board.Add(row);
        }
    }

    public static void SetWaterPrefab(GameObject water) {
        waterPrefab = water;
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
        if (PlacementManager.CheckIfPlayerCanPutWarshipOnThisPosition(this, warship)) {
            SetWarshipOnBoard(warship);
        }
    }

    private void SetWarshipOnBoard(Warship warship) {
        SetWarship(warship);
        SetSecuredFieldAroundWarship(warship);
    }

    private void SetWarship(Warship warship) {
        board[warship.GetXPosition()][warship.GetYPosition()].SetPlacementResult(PlacementResult.INACCESSIBLE);
        if (warship.GetOrientation() == WarshipOrientation.HORIZONTAL)
        {
            int x = warship.GetXPosition();
            for (int i = x; i < x + warship.GetSize(); i++)
            {
                board[i][warship.GetYPosition()].SetPlacementResult(PlacementResult.INACCESSIBLE);
                board[i][warship.GetYPosition()].SetWarship(warship);
            }
        }
        else {
            int y = warship.GetYPosition();
            for (int i = y; i < y + warship.GetSize(); i++){
                board[warship.GetXPosition()][i].SetPlacementResult(PlacementResult.INACCESSIBLE);
                board[warship.GetXPosition()][i].SetWarship(warship);
            }
        }
    }


    private void SetSecuredFieldAroundWarship(Warship warship) {
        if (warship.GetOrientation() == WarshipOrientation.HORIZONTAL) {
            SetSecuredFieldForHorizontal(warship);
        }
        else {
            SetSecuredFieldForVertical(warship);
        }
    }

    private void SetSecuredFieldForHorizontal(Warship warship) {
        int x = warship.GetXPosition();
        int y = warship.GetYPosition();
        int warshipSize = warship.GetSize();

        for (int i = x - 1; i < (x + warshipSize + 1); i++) {
            try
            {
                board[i][y - 1].SetPlacementResult(PlacementResult.SECURE);
            }
            catch (ArgumentOutOfRangeException) { continue; }
            try
            {
                board[i][y + 1].SetPlacementResult(PlacementResult.SECURE);
            }
            catch (ArgumentOutOfRangeException) { continue; }
        }
        try
        {
            board[x - 1][y].SetPlacementResult(PlacementResult.SECURE);
        }
        catch (ArgumentOutOfRangeException) { }

        try
        {
            board[x + warshipSize][y].SetPlacementResult(PlacementResult.SECURE);
        }
        catch (ArgumentOutOfRangeException) { }
    }

    private void SetSecuredFieldForVertical(Warship warship) {

    }



    private bool CheckIfWarshipIsOnBoardEdge(int x, int y) {
        return ((x == 0 || x == boardSize - 1) && (y == 0 || y == boardSize - 1));
    }

}
