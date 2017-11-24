using System;
using System.Collections.Generic;
using UnityEngine;

public class BotPlayer : Player
{



    public override void ArrangeBoard()
    {
        BoardModel board = new BoardModel();
        System.Random rnd = new System.Random();
        List<Warship> ships = getAllShips();
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
                    board.PlaceWarship(ship);
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
        return ships;
    }

    public static void getRandomWarship(Warship ship)
    {
        System.Random rnd = new System.Random();
        int x = rnd.Next(Board.boardSize);
        int y = rnd.Next(10);
        ship.SetPosition(x, y);
        WarshipOrientation wo = (WarshipOrientation)rnd.Next(2);
        ship.SetWarshipOrientation(wo);

    }

    public override void SetPlayerBoard(WarshipsContainer warshipsContainer)
    {
        playerBoard = new BoardModel();
        opponentBoard = new BoardModel();
        this.shipsContainer = warshipsContainer;
        foreach (Warship ship in shipsContainer.GetWarships()){
            playerBoard.PlaceWarship(ship);
        }
        playerBoard.SetFieldsOccupiedByWarships(20);
    }

    public override void YourTurn()
    {
        Debug.Log("Bot Turn");
        System.Random rnd = new System.Random();
        int x = rnd.Next(Board.boardSize);
        int y = rnd.Next(Board.boardSize);
        controller.ShotOpponent(x, y);
    }


}
