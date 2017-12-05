using System;
using System.Collections.Generic;

public class BotPlayer : Player
{

    protected ComplexShootingBoard opponentBoard;
    private List<Position> likelyToHit;
    private readonly float PRECISION;
    private Random rnd;

    public BotPlayer(float precision):base()
    {
        rnd = new Random();
        this.PRECISION = precision;
        opponentBoard = new ComplexShootingBoard(precision);
        likelyToHit = opponentBoard.likelyToHit;
    }

    public override void ArrangeBoard()
    {
        PlacementBoard board = new PlacementBoard();
        System.Random rnd = new System.Random();
        WarshipCreator creator = new WarshipCreator();
        List<Warship> ships = creator.GetWarshipList();
        bool lastShipOk = false;
        while (!lastShipOk)
        {
            board.GenerateBoardModel();
            for(int i = 0; i < 10; i++)
            {
                Warship ship = ships[i];
                int tries = 10;
                do
                {
                    getRandomWarship(ship);
                    lastShipOk = PlacementManager.CheckIfPlayerCanPutWarshipOnThisPosition(board, ship);
                } while (!lastShipOk && tries > 0);

                if (lastShipOk)
                    board.SetWarship(ship);
                else
                    break;
            }
        }
        this.shipsContainer = new WarshipsContainer(ships);
        for (int i = 0; i < 10; i++)
        {
            Warship ship = ships[i];
            UnityEngine.Debug.Log(ship.toStringShort());
        }
    }

    private static void getRandomWarship(Warship ship)
    {
        System.Random rnd = new System.Random();
        int x = rnd.Next(BaseBoard<BaseField>.boardSize);
        int y = rnd.Next(10);
        ship.SetPosition(x, y);
        WarshipOrientation wo = (WarshipOrientation)rnd.Next(2);
        ship.SetWarshipOrientation(wo);
    }

    public override void SetPlayerBoard(WarshipsContainer warshipsContainer)
    {
        playerBoard = new ShootingBoard();
        this.shipsContainer = warshipsContainer;
        foreach (Warship ship in shipsContainer.GetWarships()){
            playerBoard.SetWarship(ship);
        }
    }

    public override void SetPlayerShotResult(ShotRaport shotRaport)
    {
        base.SetPlayerShotResult(shotRaport);
        opponentBoard.ApplyShot(shotRaport);
    }

    public override void YourTurn()
    {
        int x;
        int y;
        if (likelyToHit.Count > 0 && rnd.NextDouble() < PRECISION)
        {
            UnityEngine.Debug.Log("Likely!");
            string s = "";
            foreach (Position p in likelyToHit)
            {
                s += "[" + p.x + " " + p.y + "] ";
            }
            UnityEngine.Debug.Log(s);
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
            while (opponentBoard.GetBoard()[x][y].hasBeenShot);
        }
        controller.ShotOpponent(x, y);
    }

}
