using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player {

    public Board opponentBoard;
    public Board playerBoard;
    private bool isYourTurn;

    public Player() {
    }

    public abstract void ArrangeBoard();

    bool IsYourTurn() {
        return isYourTurn;
    }

    public void YourTurn()
    {
        isYourTurn = true;
    }

    public void SetOpponentBoard(Board opponentBoard) {
        this.opponentBoard = opponentBoard;
    }

    void TakeOpponentShot(ShotRaport shotRaport) {
        playerBoard.ApplyShot(shotRaport);
    }

    public bool CheckIfYouWin() {
        return opponentBoard.GetFieldsOccupiedByWarships() == 0;
    }


}
