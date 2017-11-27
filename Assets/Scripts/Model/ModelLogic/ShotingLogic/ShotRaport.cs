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

    public ShotRaport(int x, int y, ShootingBoard board)
    {
        this.x = x;
        this.y = y;
        ShootingField field = board.GetBoard()[x][y];
        if (field.hasBeenShot)
            throw new IllegalShotException(x, y);
        else
            field.hasBeenShot = true;
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
        else{
            shotResult = DmgDone.MISS;
        }
    }


}
