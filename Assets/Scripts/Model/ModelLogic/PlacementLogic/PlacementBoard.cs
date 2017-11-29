using UnityEngine;

public class PlacementBoard : BaseBoard<PlacementField>{

    public override void SetWarship(Warship warship)
    {
        if (PlacementManager.CheckIfPlayerCanPutWarshipOnThisPosition(this, warship))
        {
            base.SetWarship(warship);
            SetSecuredFields(warship);
        }
        else
            throw new PlacementException(warship.GetX(), warship.GetY());
    }

    private void SetSecuredFields(Warship warship)
    {
        SecureBorder border = new SecureBorder(warship);
        for(int i = border.startX; i <= border.endX; i++)
            for(int j = border.startY; j <= border.endY; j++)
                board[i][j].securePoints++;
    }

    public override void RemoveWarship(Warship warship)
    {
        base.RemoveWarship(warship);
        RemoveSecuredFields(warship);
    }

    
    private void RemoveSecuredFields(Warship warship)
    {
        SecureBorder border = new SecureBorder(warship);
        for (int i = border.startX; i <= border.endX; i++)
            for (int j = border.startY; j <= border.endY; j++)
                board[i][j].securePoints--;
    }

    public void IsEverythingOk()
    {
        string row = "";
        for (int i = 0; i < Variables.defaultBoardSize; i++)
        {
            for (int j = 0; j < Variables.defaultBoardSize; j++)
                row += board[j][i].securePoints + " ";
            row += "\n";
        }
        Debug.Log(row);
    }
}
