using System;
using System.Collections.Generic;

public class ComplexShootingBoard : ShootingBoard
{
    public List<Position> likelyToHit;
    private bool alwaysWise;
    Random rnd;

    public ComplexShootingBoard(bool alwaysWisely) :base()
    {
        likelyToHit = new List<Position>();
        this.alwaysWise = alwaysWisely;
        rnd = new Random();
    }

    public override void ApplyShot(ShotRaport shotRaport)
    {
        base.ApplyShot(shotRaport);
        UnityEngine.Debug.Log("Complex");
        if(!shotRaport.GetShotResult().Equals(DmgDone.MISS))
            OrganiseBorder(shotRaport);
        DeleteFromLikelyList();
    }

    private void OrganiseBorder(ShotRaport raport)
    {
        ShipRevealer revealer = new ShipRevealer(this, raport);
        Warship ship = revealer.GetWarship();
        ship.toStringShort();
        SecureBorder border = new SecureBorder(ship);
        if (alwaysWise || rnd.NextDouble() < Variables.BOT_PRECISION)
        {
            if (raport.GetShotResult().Equals(DmgDone.SINKED) && (alwaysWise || rnd.NextDouble() < Variables.BOT_PRECISION))
            {
                CompleteBorder(border);
            }
            else
            {
                IncompleteBorder(border);
            }
        }
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
            likelyToHit.RemoveAll(x => x.Equals(pos));
        }
    }

}