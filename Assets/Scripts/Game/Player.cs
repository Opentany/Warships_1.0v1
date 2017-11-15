using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour {

    public Board opponentBoard;
    public Board playerBoard;
    public ShotRaport shotRaport;
    public bool isYourTurn;

    public Player() {
        shotRaport = new ShotRaport();
    }

    public abstract void ArrangeBoard();

    bool IsYourTurn() {
        return isYourTurn;
    }

    public void SetOpponentBoard(Board opponentBoard) {
        this.opponentBoard = opponentBoard;
    }

    void GetOpponentShot(int column, int row) {
        ShotResult shotResult = shotRaport.GetShotResult(column, row, playerBoard);
        playerBoard.SetShotResult(column, row, shotRaport);
    }



}
