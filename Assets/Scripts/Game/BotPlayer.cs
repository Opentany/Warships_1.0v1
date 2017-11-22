using System;
using System.Collections.Generic;
using UnityEngine;

public class BotPlayer : Player
{



    public override void ArrangeBoard()
    {
        Board board = new Board();
        System.Random rnd = new System.Random();
        WarshipCreator wc = new WarshipCreator();
        List<Warship> ships = wc.GetWarshipList();
        Debug.Log(ships.Count);
        bool lastShipOk = false;
        while (lastShipOk)
        {
            board.GenerateBoard();
            foreach (Warship ship in ships)
            {

                int x;
                int y;
                int tries = 10;
                do
                {
                    x = rnd.Next(Board.boardSize);
                    y = rnd.Next(Board.boardSize);
                    ship.SetPosition(x, y);
                    WarshipOrientation wo = (WarshipOrientation)rnd.Next(2);
                    ship.SetWarshipOrientation(wo);
                    lastShipOk = PlacementManager.CheckIfPlayerCanPutWarshipOnThisPosition(board, ship);
                } while (!lastShipOk && tries > 0);
                if (lastShipOk)
                    board.PlaceWarship(ship);
                else
                    break;
            }
        }
        this.SetPlayerBoard(board);
        Debug.Log("Done");
        foreach (Warship ship in ships)
        {
            Debug.Log(ship.toStringShort());
        }
        board.DisplayBoard();
    }
}
