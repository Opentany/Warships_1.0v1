using System;

public class PlacementException : Exception
{
    int x, y;

    public PlacementException(int x, int y) : base("Cannot place warship at: "+x+" "+y)
    {
        this.x = x;
        this.y = y;
    }

    public PlacementException(string message)
        : base(message)
    {
    }

    public PlacementException(string message, Exception inner)
        : base(message, inner)
    {
    }
}