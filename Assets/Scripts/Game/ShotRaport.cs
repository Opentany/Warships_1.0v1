using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRaport  {

    private int x;
    private int y;
    private Warship warship;
    private ShotResult shotResult;

    public ShotRaport() {
        shotResult = ShotResult.UNCHECK;
    }

    public ShotResult GetShotResult(int column, int row, Board board) {
        Field field = board.GetBoard()[column][row];
        if (!field.IsPressed())
        {
            warship = field.GetWarship();
            if (warship != null)
            {
                warship.ChangeDurability();
                if (warship.GetIsSinked())
                {
                    return ShotResult.SINKED;
                }
                else
                {
                    return ShotResult.HIT;
                }

            }
            return ShotResult.MISS;
        }
        return ShotResult.ILLEGAL;
    }

}
