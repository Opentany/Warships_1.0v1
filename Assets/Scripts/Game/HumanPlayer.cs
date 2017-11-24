using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{

    public override void ArrangeBoard()
    {
        throw new System.NotImplementedException();
    }

    public override void SetPlayerBoard(WarshipsContainer warshipsContainer)
    {
        playerBoard = new BoardModel();
        opponentBoard = new BoardModel();
        opponentBoard.SetFieldsOccupiedByWarships(20);
        this.shipsContainer = warshipsContainer;
        foreach (Warship ship in shipsContainer.GetWarships()){
            playerBoard.PlaceWarship(ship);
        }
    }

    public override void YourTurn()
    {
        controller.activeHuman = true;
    }
}
