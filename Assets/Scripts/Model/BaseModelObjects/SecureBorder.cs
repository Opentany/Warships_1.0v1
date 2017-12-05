using System.Collections.Generic;

public class SecureBorder
{
    public int startX;
    public int startY;
    public int endX;
    public int endY;
    public Warship ship;
    private List<Position> secureFields;

    public SecureBorder(Warship warship)
    {
        this.ship = warship;
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

    public List<Position> IncompleteBorder()
    {
        List<Position> list = new List<Position>();
        list.AddRange(Corners());
        if ((int)ship.GetSize() != 1)
        {
            UnityEngine.Debug.Log("Size "+ship.GetSize());
            if (ship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL))
                list.AddRange(HorizontalEdge());
            else
                list.AddRange(VerticalEdge());
        }
        string s = "EdgeBorder ";
        foreach (Position p in list)
            s += " [ " + p.x + " " + p.y + " ]";
        UnityEngine.Debug.Log(s);
        return list;
    }

    public List<Position> LikelyList()
    {
        List<Position> list = new List<Position>();
        if ((int)ship.GetSize() == 1 || ship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL))
            list.AddRange(LikelyHorizontal());
        if ((int)ship.GetSize() == 1 || ship.GetOrientation().Equals(WarshipOrientation.VERTICAL))
            list.AddRange(LikelyVertical());
        UnityEngine.Debug.Log("New Likely!");
        string s = "";
        foreach (Position p in list)
        {
            s += "[" + p.x + " " + p.y + "] ";
        }
        UnityEngine.Debug.Log(s);
        return list;
    }

    private List<Position> HorizontalEdge()
    {
        List<Position> list = new List<Position>();
        if (startY != ship.GetY())
            for (int i = ship.GetX(); i < ship.GetX() + (int)ship.GetSize(); i++)
                list.Add(new Position(i, startY));
        if (endY != ship.GetY())
            for (int i = ship.GetX(); i < ship.GetX() + (int)ship.GetSize(); i++)
                list.Add(new Position(i, endY));
        return list;
    }

    private List<Position> VerticalEdge()
    {
        List<Position> list = new List<Position>();
        if (startX != ship.GetX())
            for (int i = ship.GetY(); i < ship.GetY() + (int)ship.GetSize(); i++)
                list.Add(new Position(startX, i));
        if (endX != ship.GetX())
            for (int i = ship.GetY(); i < ship.GetY() + (int)ship.GetSize(); i++)
                list.Add(new Position(endX, i));
        return list;
    }

    private List<Position> Corners()
    {
        List<Position> list = new List<Position>();
        if(ship.GetX() != startX)
        {
            if(ship.GetY() != startY)
                list.Add(new Position(startX, startY));
            if (ship.GetY() != endY)
                list.Add(new Position(startX, endY));
        }
        if(ship.GetX() != endX)
        {
            if (ship.GetY() != startY)
                list.Add(new Position(endX, startY));
            if (ship.GetY() != endY)
                list.Add(new Position(endX, endY));
        }
        return list;
    }

    private List<Position> LikelyHorizontal()
    {
        List <Position> list = new List<Position>();
        if (startX != ship.GetX())
            list.Add(new Position(startX, ship.GetY()));
        if (endX != ship.GetX() + (int)ship.GetSize() - 1)
            list.Add(new Position(endX, ship.GetY()));
        return list;
    }

    private List<Position> LikelyVertical()
    {
        List<Position> list = new List<Position>();
        if (startY != ship.GetY())
            list.Add(new Position(ship.GetX(), startY));
        if (endY != ship.GetY() + (int)ship.GetSize() - 1)
            list.Add(new Position(ship.GetX(), endY));
        return list;
    }

}