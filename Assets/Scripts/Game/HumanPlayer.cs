using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    public HumanPlayer() {
        playerBoard = new Board();
        playerBoard.GenerateBoard();

    }
    
    public override void ArrangeBoard()
    {
        throw new System.NotImplementedException();
    }
}
