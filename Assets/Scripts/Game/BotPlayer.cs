using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPlayer : Player
{



    public override void ArrangeBoard()
    {
        
    }

    public void SetPlayerBoard(Board playerBoard)
    {
        this.playerBoard = playerBoard;
    }

    public void SetOpponentBoard(Board opponentBoard)
    {
        this.opponentBoard = opponentBoard;
    }

    public void TakeOpponentShot(ShotRaport shotRaport)
    {
        playerBoard.ApplyShot(shotRaport);
    }

    public void SetPlayerShotResult(ShotRaport shotRaport)
    {
        opponentBoard.ApplyShot(shotRaport);
    }
}
