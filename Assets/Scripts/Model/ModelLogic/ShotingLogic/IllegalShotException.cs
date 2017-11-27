using System;

public class IllegalShotException : Exception
{
    int x, y;

    public IllegalShotException(int x, int y) : base("Cannot shot at: " + x + " " + y)
    {
        this.x = x;
        this.y = y;
    }

    public IllegalShotException(string message)
        : base(message)
    {
    }

    public IllegalShotException(string message, Exception inner)
        : base(message, inner)
    {
    }
}