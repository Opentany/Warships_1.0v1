using System;
using System.Collections.Generic;

public class ComplexShootingBoard : ShootingBoard
{
    public List<Position> likelyToHit;
    private bool alwaysWisely;
    Random rnd;

    public ComplexShootingBoard(bool alwaysWisely) :base()
    {
        likelyToHit = new List<Position>();
        this.alwaysWisely = alwaysWisely;
        rnd = new Random();
    }

    public void ApplyShotWisely(ShotRaport shotRaport)
    {
        ApplyShot(shotRaport);
        OrganiseBorder(shotRaport);
    }

    private void OrganiseBorder(ShotRaport raport)
    {
        ShipRevealer revealer = new ShipRevealer(this, raport);
        Warship ship = revealer.GetWarship();
        SecureBorder border = new SecureBorder(ship);
        if (raport.GetShotResult().Equals(DmgDone.SINKED) && (alwaysWisely || rnd.NextDouble() < Variables.BOT_PRECISION) )
        {
            CompleteBorder(border);
        }
        else
        {
            IncompleteBorder(border);
        }
        DeleteFromLikelyList();
    }

    private void CompleteBorder(SecureBorder border)
    {
        for (int i = border.startX; i <= border.endX; i++)
            for (int j = border.startY; j <= border.endY; j++)
                board[i][j].hasBeenShot = true;
    }

    private void IncompleteBorder(SecureBorder border)
    {
        List<Position> secureList = border.IncompleteBorder();
        foreach(Position pos in secureList)
        {
            board[pos.x][pos.y].hasBeenShot = true;
        }
        likelyToHit.AddRange(border.LikelyList());
    }

    private void DeleteFromLikelyList()
    {
        List<Position> toRemove = new List<Position>();
        foreach (Position pos in likelyToHit)
        {
            if (board[pos.x][pos.y].hasBeenShot)
                toRemove.Add(pos);
        }
        foreach(Position pos in toRemove)
        {
            likelyToHit.Remove(pos);
        }
    }

}