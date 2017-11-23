using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    public static readonly int boardSize = 10;

    private static GameObject waterPrefab;
    private static GameObject miniWaterPrefab;
    private List<List<Field>> board;
    private List<List<Field>> miniBoard;
    private float screenHorizontalOffset = -2f;
    private float fieldMargin = 0.05f;
    private float screenVerticalOffset = -3f;
    private int fieldsOccupiedByWarships;
    private List<Warship> warshipList;

    public Board() {
        board = new List<List<Field>>();
        miniBoard = new List<List<Field>>();
        warshipList = new List<Warship>();
        fieldsOccupiedByWarships = 0;
    }

    public void GenerateBoard() {
        board = new List<List<Field>>();
        for (int i = 0; i < boardSize; i++)
        {
            List<Field> row = new List<Field>();
            for (int j = 0; j < boardSize; j++)
            {
                Field field = GameObject.Instantiate(waterPrefab, new Vector2(), Quaternion.Euler(new Vector2())).GetComponent<Field>();
                field.enabled = false;
                field.gridPosition = new Vector2(i, j);
                row.Add(field);
            }
            board.Add(row);
        }
    }


    public void GenerateBoardOnScreen() {
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


    public void GenerateMiniBoardOnScreen()
    {
        miniBoard = new List<List<Field>>();
        float fieldSize = waterPrefab.GetComponent<BoxCollider2D>().size.x / 2 + fieldMargin / 2;
        for (int i = 0; i < boardSize; i++)
        {
            List<Field> row = new List<Field>();
            for (int j = 0; j < boardSize; j++)
            {
                Field field = GameObject.Instantiate(miniWaterPrefab, new Vector2(screenHorizontalOffset + i * fieldSize, screenVerticalOffset + j * fieldSize), Quaternion.Euler(new Vector2())).GetComponent<Field>();
                field.gameObject.layer = 1;
                field.gridPosition = new Vector2(i, j);
                field.isMini = true;
                row.Add(field);
            }
            miniBoard.Add(row);
        }
        miniWaterPrefab.SetActive(false);

    }

    public static void SetWaterPrefab(GameObject water) {
        waterPrefab = water;
        CreateMiniWaterPrefab(water);
    }

    private static void CreateMiniWaterPrefab(GameObject water) {
        miniWaterPrefab = GameObject.Instantiate(water) as GameObject;
        miniWaterPrefab.transform.localScale = miniWaterPrefab.transform.localScale / 2;
    }

    public List<List<Field>> GetBoard() {
        return board;
    }

    public List<List<Field>> GetMiniBoard() {
        return miniBoard;
    }

    public Field GetField(int column, int row) {
        return board[column][row];
    }

    public void PlaceWarship(Warship warship) {
        if (PlacementManager.CheckIfPlayerCanPutWarshipOnThisPosition(this, warship)) {
            warshipList.Add(warship);
            SetWarshipOnBoard(warship);
        }
    }

    public void ApplyShot(ShotRaport shotRaport) {
        int x = shotRaport.GetX();
        int y = shotRaport.GetY();
        board[x][y].SetShotResult(shotRaport.GetShotResult());
        if (CheckIfFieldWasShot(shotRaport)) {
            fieldsOccupiedByWarships--;
        }
    }

    public void ApplyMyShot(ShotRaport shotRaport) {
        int x = shotRaport.GetX();
        int y = shotRaport.GetY();
        board[x][y].SetShotResult(shotRaport.GetShotResult());
        board[x][y].SetEffectOnField(shotRaport.GetShotResult());
    }


    public void ApplyOpponentShot(ShotRaport shotRaport) {
        int x = shotRaport.GetX();
        int y = shotRaport.GetY();
        miniBoard[x][y].SetShotResult(shotRaport.GetShotResult());
        miniBoard[x][y].SetColorOnField(shotRaport.GetShotResult());
    }

    private bool CheckIfFieldWasShot(ShotRaport shotRaport) {
        return (shotRaport.GetShotResult().Equals(DmgDone.HIT) || shotRaport.GetShotResult().Equals(DmgDone.SINKED));
    }

    private void SetWarshipOnBoard(Warship warship) {
        SetWarship(warship);
        SetSecuredFields(warship);
    }

    private void SetWarship(Warship warship) {
        if (warship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL))
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
        fieldsOccupiedByWarships += warship.GetSize();
    }

    private void SetWarshipVertical(Warship warship) {
        int y = warship.GetYPosition();
        for (int i = y; i < y + warship.GetSize(); i++)
        {
            board[warship.GetXPosition()][i].SetPlacementResult(PlacementResult.INACCESSIBLE);
            board[warship.GetXPosition()][i].SetWarship(warship);
        }
        fieldsOccupiedByWarships += warship.GetSize();
    }


    private void SetSecuredFields(Warship warship) {
        int x = warship.GetXPosition();
        int y = warship.GetYPosition();
        int warshipSize = warship.GetSize();
        int startHorizontal, endHorizontal;
        int startVertical, endVertical;
        if (warship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL)) {
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
        for (int i = startHorizontal; i <= endHorizontal; i++)
        {
            for (int j = startVertical; j <= endVertical; j++)
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

    public void RemoveWarship(Warship warship) {
        warshipList.Remove(warship);
        if (warship.GetOrientation() == WarshipOrientation.HORIZONTAL)
        {
            RemoveWarshipHorizontal(warship);
        }
        else
        {
            RemoveWarshipVertical(warship);
        }
        RemoveSecuredFields(warship);
    }


    private void RemoveWarshipHorizontal(Warship warship)
    {
        int x = warship.GetXPosition();
        for (int i = x; i < x + warship.GetSize(); i++)
        {
            board[i][warship.GetYPosition()].SetPlacementResult(PlacementResult.AVAILABLE);
            board[i][warship.GetYPosition()].SetWarship(null);
        }
        fieldsOccupiedByWarships -= warship.GetSize();
    }

    private void RemoveWarshipVertical(Warship warship)
    {
        int y = warship.GetYPosition();
        for (int i = y; i < y + warship.GetSize(); i++)
        {
            board[warship.GetXPosition()][i].SetPlacementResult(PlacementResult.AVAILABLE);
            board[warship.GetXPosition()][i].SetWarship(null);
        }
        fieldsOccupiedByWarships -= warship.GetSize();
    }

    private void RemoveSecuredFields(Warship warship)
    {
        int x = warship.GetXPosition();
        int y = warship.GetYPosition();
        int warshipSize = warship.GetSize();
        int startHorizontal, endHorizontal;
        int startVertical, endVertical;
        if (warship.GetOrientation() == WarshipOrientation.HORIZONTAL)
        {
            startHorizontal = (x != 0) ? x - 1 : x;
            endHorizontal = (x + warshipSize < boardSize) ? x + warshipSize : boardSize - 1;
            startVertical = (y != 0) ? y - 1 : y;
            endVertical = (y + 1 < boardSize) ? y + 1 : boardSize - 1;
        }
        else
        {
            startHorizontal = (x != 0) ? x - 1 : x;
            endHorizontal = (x + 1 < boardSize) ? x + 1 : boardSize - 1;
            startVertical = (y != 0) ? y - 1 : y;
            endVertical = (y + warshipSize < boardSize) ? y + warshipSize : boardSize - 1;
        }
        RemoveSecuredFieldsAroundWarship(startVertical, endVertical, startHorizontal, endHorizontal);
    }

    private void RemoveSecuredFieldsAroundWarship(int startVertical, int endVertical, int startHorizontal, int endHorizontal)
    {
        for (int i = startVertical; i <= endVertical; i++)
        {
            for (int j = startHorizontal; j <= endHorizontal; j++)
            {
                if (board[i][j].GetSecureFieldCounter() == 1)
                {
                    board[i][j].SetPlacementResult(PlacementResult.AVAILABLE);
                }
                else {
                    board[i][j].DecreaseSecureFieldCounter();
                }
            }
        }
    }

    public void DisplayBoard()
    {
        string row="";
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                row += " " + board[i][j].secureFieldCounter;
            }
            Debug.Log(row);
            row = "";
        }
    }

    public int GetFieldsOccupiedByWarships() {
        return fieldsOccupiedByWarships;
    }

    public void SetFieldsOccupiedByWarships(int fields)
    {
        fieldsOccupiedByWarships = fields;
    }


    public WarshipsContainer GetWarshipList() {
        return new WarshipsContainer(warshipList);
    }

    public void SetWarshipOnMiniBoard(List<Warship> warships) {
        foreach (Warship warship in warships) {
            if (warship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL))
            {
                AddColorHorizontal(warship);
            }
            else {
                AddColorVertical(warship);
            }
        }
    }

    private void AddColorHorizontal(Warship warship) {
        int x = warship.GetXPosition();
        for (int i = x; i < x + warship.GetSize(); i++)
        {
            miniBoard[i][warship.GetYPosition()].SetWarshipColor();
        }
    }

    private void AddColorVertical(Warship warship){
        int y = warship.GetYPosition();
        for (int i = y; i < y + warship.GetSize(); i++)
        {
            miniBoard[warship.GetXPosition()][i].SetWarshipColor();
        }
    }



}
