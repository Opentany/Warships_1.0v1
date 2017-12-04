using System;
using System.Collections.Generic;

public class GreaterBot : BotPlayer
{

    protected new ComplexShootingBoard opponentBoard;
    private List<Position> likelyToHit;
    private Random rnd;
    public bool alwaysWise;

    public GreaterBot(BotLevel level)
    {
        rnd = new Random();
        isYourTurn = false;
        alwaysWise = (level.Equals(BotLevel.GREAT)) ? true : false;
        opponentBoard = new ComplexShootingBoard(alwaysWise);
        likelyToHit = opponentBoard.likelyToHit;    
    }

    public override void YourTurn()
    {
        int x;
        int y;
        if (likelyToHit.Count > 0 && (alwaysWise || rnd.NextDouble() < Variables.BOT_PRECISION))
        {
            Position pos = likelyToHit[rnd.Next(likelyToHit.Count)];
            x = pos.x;
            y = pos.y;
        }
        else
        {
            do
            {
                x = rnd.Next(BaseBoard<BaseField>.boardSize);
                y = rnd.Next(BaseBoard<BaseField>.boardSize);
            }
            while (!opponentBoard.GetBoard()[x][y].hasBeenShot);
        }
        controller.ShotOpponent(x, y);
    }

    /*
    public override void SetPlayerShotResult(ShotRaport shotRaport)
    {
        base.SetPlayerShotResult(shotRaport);
    }
    */

}