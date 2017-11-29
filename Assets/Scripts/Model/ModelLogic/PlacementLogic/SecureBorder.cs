public class SecureBorder
{
    public int startX;
    public int startY;
    public int endX;
    public int endY;


    public SecureBorder(Warship warship)
    {
        int x = warship.GetX();
        int y = warship.GetY();
        int warshipSize = warship.GetSize();
        int boardSize = Variables.defaultBoardSize;
        startX = (x != 0) ? x - 1 : x;
        startY = (y != 0) ? y - 1 : y;
        if (warship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL))
        {
            endX = (x + warshipSize < boardSize) ? x + warshipSize : boardSize - 1;   
            endY = (y + 1 < boardSize) ? y + 1 : boardSize - 1;
        }
        else
        {
            endX = (x + 1 < boardSize) ? x + 1 : boardSize - 1;
            endY = (y + warshipSize < boardSize) ? y + warshipSize : boardSize - 1;
        }
    }


}