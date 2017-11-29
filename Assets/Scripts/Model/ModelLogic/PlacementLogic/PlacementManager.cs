using UnityEngine;

public class PlacementManager{
	private static int FULL_PLACEMENT = Variables.fieldsOccupiedByWarships;
    private static int BOARD_SIZE = Variables.defaultBoardSize;

    public static bool CheckIfPlayerCanPutWarshipOnThisPosition(PlacementBoard board, Warship warship)
    {
        int x = warship.GetX();
        int y = warship.GetY();

        if (warship.GetOrientation() == WarshipOrientation.HORIZONTAL)
        {
            return CheckIfCanPutWarshipHorizontal(board, warship);
        }
        else
        {
            return CheckIfCanPutWarshipVertical(board, warship);
        }
    }

    private static bool CheckIfCanPutWarshipHorizontal(PlacementBoard board, Warship warship)
    {
        int x = warship.GetX();

        if (x + warship.GetSize() > BOARD_SIZE)
            return false;
        for (int i = x; i < x + warship.GetSize(); i++)
        {
            if (board.GetBoard()[i][warship.GetY()].isSecure())
            {
                return false;
            }
        }
        return true;
    }

    private static bool CheckIfCanPutWarshipVertical(PlacementBoard board, Warship warship)
    {
        int y = warship.GetY();

        if (y + warship.GetSize() > BOARD_SIZE)
            return false;
        for (int i = y; i < y + warship.GetSize(); i++)
        {
            if (board.GetBoard()[warship.GetX()][i].isSecure())
            {
                return false;
            }
        }
        return true;
    }


    public static bool CanGameStart(int warshipsField) {
        return warshipsField == FULL_PLACEMENT;
    }
 
}
