using System;
using System.Collections.Generic;

public class ShipRevealer
{
    List<List<ShootingField>> board;
    private int startX;
    private int startY;
    private WarshipSize size;
    ShotRaport shotRaport;
    WarshipOrientation orientation;

    public ShipRevealer(ShootingBoard board, ShotRaport shotRaport)
    {
        this.board = board.GetBoard();
        this.shotRaport = shotRaport;
        CheckWarshipOrientation();
        GetStartinPoint();
        GetSize();
    }

    public Warship GetWarship()
    {
        Warship ship = new Warship(size);
        ship.SetPosition(startX, startY);
        ship.SetWarshipOrientation(orientation);
        return ship;
    }

    private WarshipOrientation CheckWarshipOrientation()
    {
        if (IsHorizontal(shotRaport)){
            return WarshipOrientation.HORIZONTAL;
        }
        if(IsVertical(shotRaport))
        {
            return WarshipOrientation.VERTICAL;
        }
        return PossibleOrientation();
    }

    private bool IsHorizontal(ShotRaport shotRaport)
    {
        int x = shotRaport.GetX();
        int y = shotRaport.GetY();
        if (x != 0 && !board[x - 1][y].dmgDone.Equals(DmgDone.MISS))
            return true;
        if (x != Variables.defaultBoardSize - 1 && !board[x + 1][y].dmgDone.Equals(DmgDone.MISS))
            return true;
        return false;
    }

    private bool IsVertical(ShotRaport shotRaport)
    {
        int x = shotRaport.GetX();
        int y = shotRaport.GetY();
        if (y != 0 && !board[x][y - 1].dmgDone.Equals(DmgDone.MISS))
            return true;
        if (y != Variables.defaultBoardSize - 1 && !board[x][y + 1].dmgDone.Equals(DmgDone.MISS))
            return true;
        return false;
    }

    private void GetStartinPoint()
    {
        int startX = shotRaport.GetX();
        int startY = shotRaport.GetY();
        if (orientation.Equals(WarshipOrientation.HORIZONTAL))
        {
            while (startX > 0 && !board[startX - 1][startY].dmgDone.Equals(DmgDone.MISS))
                startX--;

        }
        else
        {
            while (startY > 0 && !board[startX][startY - 1].dmgDone.Equals(DmgDone.MISS))
                startY--;
        }
    }

    private void GetSize()
    {
        if (orientation.Equals(WarshipOrientation.HORIZONTAL))
        {
            int endX = startX;
            while (endX < Variables.defaultBoardSize && !board[endX + 1][startY].dmgDone.Equals(DmgDone.MISS))
                endX++;
            size = (WarshipSize)(endX - startX + 1);
        }
        else
        {
            int endY = startY;
            while (endY < Variables.defaultBoardSize && !board[startX][endY + 1].dmgDone.Equals(DmgDone.MISS))
                endY++;
            size = (WarshipSize)(endY - startY + 1);
        }
        UnityEngine.Debug.Log("Size " + size);
    }

    public WarshipOrientation PossibleOrientation()
    {
        int x = shotRaport.GetX();
        int y = shotRaport.GetY();
        bool canVer = false;
        bool canHor = false;
        if (x > 0 && !board[x - 1][y].hasBeenShot)
            canHor = true;
        if (x < Variables.defaultBoardSize - 1 && !board[x + 1][y].hasBeenShot)
            canHor = true;
        if (y > 0 && !board[x][y - 1].hasBeenShot)
            canVer = true;
        if (y < Variables.defaultBoardSize - 1 && !board[x][y + 1].hasBeenShot)
            canVer = true;
        if(canHor && canVer)
        {
            Random rnd = new Random();
            if (rnd.Next(2) == 0)
                return WarshipOrientation.HORIZONTAL;
            else
                return WarshipOrientation.VERTICAL;
        }
        if (canVer)
            return WarshipOrientation.VERTICAL;
        if (canHor)
            return WarshipOrientation.HORIZONTAL;
        if (!shotRaport.GetShotResult().Equals(DmgDone.SINKED))
        {
            string row = "";
            for (int i = 0; i < Variables.defaultBoardSize; i++)
            {
                for (int j = 0; j < Variables.defaultBoardSize; j++)
                    row +=( board[j][i].hasBeenShot ? 1 : 0 ) + " ";
                row += "\n";
            }
            UnityEngine.Debug.Log(row);
            //throw new BotLogicException("Not sinked ship surrounded by shot fields");
        }
        return WarshipOrientation.VERTICAL;
    }

}