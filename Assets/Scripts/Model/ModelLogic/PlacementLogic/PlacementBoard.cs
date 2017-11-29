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
            throw new PlacementException(warship.GetXPosition(), warship.GetYPosition());
    }

    private void SetSecuredFields(Warship warship)
    {
        int x = warship.GetXPosition();
        int y = warship.GetYPosition();
		int warshipSize = warship.GetSize();
        int startHorizontal, endHorizontal;
        int startVertical, endVertical;
        if (warship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL))
        {
            startHorizontal = (x != 0) ? x - 1 : x;
            endHorizontal = (x + warshipSize < boardSize) ? x + warshipSize : boardSize - 1;
            startVertical = (y != 0) ? y - 1 : y;
            endVertical = (y + 1 < boardSize) ? y + 1 : boardSize - 1;
        }
        else
        {
            startHorizontal = (x != 0) ? x - 1 : x;
            endHorizontal = (x + 1 < boardSize) ? x + 1 : boardSize - 1;
            startVertical = (y != 0) ? y - 1 : y;
            endVertical = (y + warshipSize < boardSize) ? y + warshipSize : boardSize - 1;
        }
        SetSecuredFieldsAroundWarship(startVertical, endVertical, startHorizontal, endHorizontal);
    }

    private void SetSecuredFieldsAroundWarship(int startVertical, int endVertical, int startHorizontal, int endHorizontal)
    {
        for (int i = startHorizontal; i <= endHorizontal; i++)
        {
            for (int j = startVertical; j <= endVertical; j++)
            {
                if (!CheckIfFieldHasWarshipOnCurrentIndexs(i, j))
                {
                    board[i][j].SetPlacementResult(PlacementResult.SECURE);
					board[i][j].securePoints++;
                }
            }
        }
    }

    public override void RemoveWarship(Warship warship)
    {
        base.RemoveWarship(warship);
        RemoveSecuredFields(warship);
    }

    
     private void RemoveSecuredFields(Warship warship)
     {
         int x = warship.GetXPosition();
         int y = warship.GetYPosition();
         int warshipSize = warship.GetSize();
         int startHorizontal, endHorizontal;
         int startVertical, endVertical;
         if (warship.GetOrientation() == WarshipOrientation.HORIZONTAL)
         {
             startHorizontal = (x != 0) ? x - 1 : x;
             endHorizontal = (x + warshipSize < boardSize) ? x + warshipSize : boardSize - 1;
             startVertical = (y != 0) ? y - 1 : y;
             endVertical = (y + 1 < boardSize) ? y + 1 : boardSize - 1;
         }
         else
         {
             startHorizontal = (x != 0) ? x - 1 : x;
             endHorizontal = (x + 1 < boardSize) ? x + 1 : boardSize - 1;
             startVertical = (y != 0) ? y - 1 : y;
             endVertical = (y + warshipSize < boardSize) ? y + warshipSize : boardSize - 1;
         }
         RemoveSecuredFieldsAroundWarship(startVertical, endVertical, startHorizontal, endHorizontal);
     }

    private void RemoveSecuredFieldsAroundWarship(int startVertical, int endVertical, int startHorizontal, int endHorizontal)
    {
        for (int i = startHorizontal; i <= endHorizontal; i++)
        {
            for (int j = startVertical; j <= endVertical; j++)
            {
				Debug.Log (i + " " + j + " " + board [i] [j].GetSecureFieldCounter ());
                if (board[i][j].GetSecureFieldCounter() <= 1)
                {
                    board[i][j].SetPlacementResult(PlacementResult.AVAILABLE);
                    board[i][j].securePoints = 0;
                }
                else
                {
                    board[i][j].DecreaseSecureFieldCounter();
                }
            }
        }
    }

}
