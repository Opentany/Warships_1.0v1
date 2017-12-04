using System.Collections.Generic;
using UnityEngine;

public abstract class BotPlayer : Player
{

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
            Debug.Log(ship.toStringShort());
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

}
