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
            for (int j = 0; j < boardSize; j++) {
                Field field = GameObject.Instantiate(waterPrefab, new Vector2(screenHorizontalOffset + i * fieldSize, j * fieldSize), Quaternion.Euler(new Vector2())).GetComponent<Field>();
                field.gameObject.layer = 1;
                field.gridPosition = new Vector2(i, j);
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
        SetSecuredFields(warship);
    }

    private void SetWarship(Warship warship) {
        if (warship.GetOrientation() == WarshipOrientation.HORIZONTAL)
        {
            SetWarshipHorizontal(warship);        }
        else {
            SetWarshipVertical(warship);
        }
    }

    private void SetWarshipHorizontal(Warship warship) {
        int x = warship.GetXPosition();
        for (int i = x; i < x + warship.GetSize(); i++)
        {
            board[i][warship.GetYPosition()].SetPlacementResult(PlacementResult.INACCESSIBLE);
            board[i][warship.GetYPosition()].SetWarship(warship);
        }
    }

    private void SetWarshipVertical(Warship warship) {
        int y = warship.GetYPosition();
        for (int i = y; i < y + warship.GetSize(); i++)
        {
            board[warship.GetXPosition()][i].SetPlacementResult(PlacementResult.INACCESSIBLE);
            board[warship.GetXPosition()][i].SetWarship(warship);
        }
    }


    private void SetSecuredFields(Warship warship) {
        int x = warship.GetXPosition();
        int y = warship.GetYPosition();
        int warshipSize = warship.GetSize();
        int startHorizontal, endHorizontal;
        int startVertical, endVertical;
        if (warship.GetOrientation() == WarshipOrientation.HORIZONTAL) {
            startHorizontal = (x != 0) ? x - 1 : x;
            endHorizontal = (x + warshipSize < boardSize) ? x + warshipSize : boardSize - 1;
            startVertical = (y != 0) ? y - 1 : y;
            endVertical = (y + 1 < boardSize) ? y + 1 : boardSize - 1;
        }
        else {
            startHorizontal = (x != 0) ? x - 1 : x;
            endHorizontal = (x + 1 < boardSize) ? x + 1 : boardSize - 1;
            startVertical = (y != 0) ? y - 1 : y;
            endVertical = (y + warshipSize < boardSize) ? y + warshipSize : boardSize - 1;
        }
        SetSecuredFieldsAroundWarship(startVertical, endVertical, startHorizontal, endHorizontal);
    }

    private void SetSecuredFieldsAroundWarship(int startVertical, int endVertical, int startHorizontal, int endHorizontal) {
        for (int i = startVertical; i <= endVertical; i++)
        {
            for (int j = startHorizontal; j <= endHorizontal; j++)
            {
                if (!CheckIfFieldHasWarshipOnCurrentIndexs(i, j))
                {
                    board[i][j].SetPlacementResult(PlacementResult.SECURE);
                }
            }
        }
    }

    private bool CheckIfFieldHasWarshipOnCurrentIndexs(int i, int j) {
        return PlacementResult.INACCESSIBLE.Equals(board[i][j].GetPlacementResult());
    }


}
