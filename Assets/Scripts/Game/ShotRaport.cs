using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRaport  {

    private int x;
    private int y;
    private Warship warship;
    private DmgDone shotResult;


    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public DmgDone GetShotResult()
    {
        return shotResult;
    }

    public ShotRaport (int x, int y, Board board) {
        this.x = x;
        this.y = y;
        Field field = board.GetBoard()[x][y];
        warship = field.GetWarship();
        if (warship != null)
        {
            warship.ChangeDurability();
            if (warship.GetIsSinked())
            {
                shotResult =  DmgDone.SINKED;
            }
            else
            {
                shotResult = DmgDone.HIT;
            }

        }
        shotResult = DmgDone.MISS;
        throw new IllegalShotException(x, y);
    }

    public ShotRaport(int x, int y, BoardModel board)
    {
        this.x = x;
        this.y = y;
        FieldModel field = board.GetBoard()[x][y];
        if (field.shotted)
            throw new IllegalShotException(x, y);
        else
            field.shotted = true;
        warship = field.GetWarship();
        if (warship != null)
        {
            warship.ChangeDurability();
            if (warship.GetIsSinked())
            {
                shotResult = DmgDone.SINKED;
            }
            else
            {
                shotResult = DmgDone.HIT;
            }

        }
        shotResult = DmgDone.MISS;
    }


}
