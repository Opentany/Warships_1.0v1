using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour {

    private Board opponentBoard;
    private Board playerBoard;
    private ShotRaport shotRaport;
    private bool isYourTurn;

    public abstract void ArrangeBoard();

    private bool IsYourTurn() {
        return isYourTurn;
    }

    void SetOpponentBoard(Board opponentBoard) {
        this.opponentBoard = opponentBoard;
    }

    void GetOpponentShot(int column, int row) {
        playerBoard.SetShotResult(column, row, null);
    }



}
