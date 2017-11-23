using System;
using System.Collections.Generic;
using UnityEngine;

public class BotPlayer : Player
{



    public override void ArrangeBoard()
    {
        Board board = new Board();
        System.Random rnd = new System.Random();
        List<Warship> ships = getAllShips();
        bool lastShipOk = false;
        while (!lastShipOk)
        {
            board.GenerateBoard();
            for(int i = 0; i < 10; i++)
            {
                Warship ship = ships[i];
                int x;
                int y;
                int tries = 10;
                do
                {
                    getRandomWarship(ship);
                    lastShipOk = PlacementManager.CheckIfPlayerCanPutWarshipOnThisPosition(board, ship);
                } while (!lastShipOk && tries > 0);

                if (lastShipOk)
                    board.PlaceWarship(ship);
                else
                    break;
            }
        }
        this.SetPlayerBoard(board);
        for (int i = 0; i < 10; i++)
        {
            Warship ship = ships[i];
            Debug.Log(ship.toStringShort());
        }
    }

    private List<Warship> getAllShips()
    {
        int licznik=0;
        List<Warship> ships = new List<Warship>();
        for(int i = 4; i> 0; i--)
        {
            for(int j = 5-i; j > 0; j--)
            {
                ships.Add(new Warship((WarshipSize)i));
                licznik++;
            }
        }
        Debug.Log("ile statków");
        Debug.Log(licznik);
        return ships;
    }

    public static void getRandomWarship(Warship ship)
    {
        System.Random rnd = new System.Random();
        int x = rnd.Next(Board.boardSize);
        int y = rnd.Next(10);
        ship.SetPosition(x, y);
        Debug.Log("Tutaj");
        Debug.Log(ship.GetXPosition());
        WarshipOrientation wo = (WarshipOrientation)rnd.Next(2);
        ship.SetWarshipOrientation(wo);
        Debug.Log(ship.toStringShort());
    }

}
