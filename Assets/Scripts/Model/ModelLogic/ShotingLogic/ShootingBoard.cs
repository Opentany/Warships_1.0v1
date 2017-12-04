public class ShootingBoard : BaseBoard<ShootingField> {

    public virtual void ApplyShot(ShotRaport shotRaport)
    {
        int x = shotRaport.GetX();
        int y = shotRaport.GetY();
        board[x][y].SetShotResult(shotRaport.GetShotResult());
        if (CheckIfFieldWasShot(shotRaport))
        {
            fieldsOccupiedByWarships--;
        }
    }

    private bool CheckIfFieldWasShot(ShotRaport shotRaport)
    {
        return (!shotRaport.GetShotResult().Equals(DmgDone.MISS));
    }

}
