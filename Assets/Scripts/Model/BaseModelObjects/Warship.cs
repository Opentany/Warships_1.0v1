
using UnityEngine;

public class Warship{

    private int x;
    private int y;
    public WarshipSize warshipSize;
    private WarshipOrientation warshipOrientation;
    private bool isSinked;
    private int durability;

    public Warship(WarshipSize warshipSize) {
        x = 0;
        y = 0;
        this.warshipSize = warshipSize;
		warshipOrientation = Variables.defaultWarshipOrientation;
        isSinked = false;
        durability = (int)warshipSize;
    }

    public WarshipOrientation GetOrientation() {
        return warshipOrientation;
    }

    public int GetX() {
        return x;
    }

    public int GetY() {
        return y;
    }

    public void SetPosition(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public bool IsSinked() {
        return isSinked;
    }

    public void ChangeDurability() {
        durability--;
        if (durability == 0) {
            isSinked = true;
        }
    }

    public int GetDurability()
    {
        return durability;
    }

    public int GetSize() {
        return (int)warshipSize;
    }

    public void SetWarshipOrientation(WarshipOrientation orientation)
    {
        warshipOrientation = orientation;
    }
    private void ChangeOrientation() {
        warshipOrientation = IsHorizontal() ? WarshipOrientation.VERTICAL : WarshipOrientation.HORIZONTAL;
    }

    private bool IsHorizontal() {
        return warshipOrientation.Equals(WarshipOrientation.HORIZONTAL);
    }

    public bool CanRotate(int boardSize) {
        if (warshipOrientation == WarshipOrientation.HORIZONTAL)
        {
            return (y + GetSize() - 1 < boardSize);
        }
        else
        {
            return (x + GetSize() - 1 < boardSize); 
        }
    }

    public string toStringShort()
    {
        return "Size: " + warshipSize + " x: " + x + " y: " + y + " " + warshipOrientation; 
    }
}
