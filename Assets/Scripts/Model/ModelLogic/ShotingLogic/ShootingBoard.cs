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


    public void IsEverythingOk()
    {
        string row = "";
        for (int i = 0; i < Variables.defaultBoardSize; i++)
        {
            for (int j = 0; j < Variables.defaultBoardSize; j++)
                if(board[j][i].warship!=null)
                    row += board[j][i].warship.GetSize() + " ";
                else
                    row += "0 ";
            row += "\n";
        }
        UnityEngine.Debug.Log(row);
    }

}
