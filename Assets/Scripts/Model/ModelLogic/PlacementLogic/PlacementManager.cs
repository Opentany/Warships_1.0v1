using UnityEngine;

public class PlacementManager{
    private static int fieldsOccupiedByWarships = 20;

    public static bool CheckIfPlayerCanPutWarshipOnThisPosition(PlacementBoard board, Warship warship)
    {
        int x = warship.GetXPosition();
        int y = warship.GetYPosition();
        Debug.Log(x + " " + y);
        if (board.GetBoard()[x][y].GetPlacementResult() == PlacementResult.AVAILABLE)
        {
            if (warship.GetOrientation() == WarshipOrientation.HORIZONTAL)
            {
                return CheckIfCanPutWarshipHorizontal(board, warship);
            }
            else
            {
                return CheckIfCanPutWarshipVertical(board, warship);
            }
        }
        return false;
    }

    private static bool CheckIfCanPutWarshipHorizontal(PlacementBoard board, Warship warship)
    {
        int x = warship.GetXPosition();

        if (x + warship.GetSize() > BaseBoard<BaseField>.boardSize)
            return false;
        for (int i = x; i < x + warship.GetSize(); i++)
        {
            if (board.GetBoard()[i][warship.GetYPosition()].GetPlacementResult() != PlacementResult.AVAILABLE)
            {
                return false;
            }
        }
        return true;
    }

    private static bool CheckIfCanPutWarshipVertical(PlacementBoard board, Warship warship)
    {
        int y = warship.GetYPosition();

        if (y + warship.GetSize() > BaseBoard<BaseField>.boardSize)
            return false;
        for (int i = y; i < y + warship.GetSize(); i++)
        {
            if (board.GetBoard()[warship.GetXPosition()][i].GetPlacementResult() != PlacementResult.AVAILABLE)
            {
                return false;
            }
        }
        return true;
    }


    public static bool CanGameStart(int warshipsField) {
        return warshipsField == fieldsOccupiedByWarships;
    }
 
}
