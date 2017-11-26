
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
        warshipOrientation = WarshipOrientation.VERTICAL;
        isSinked = false;
        durability = (int)warshipSize;
    }

    public WarshipOrientation GetOrientation() {
        return warshipOrientation;
    }

    public int GetXPosition() {
        return x;
    }

    public int GetYPosition() {
        return y;
    }

    public void SetPosition(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public bool GetIsSinked() {
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
        warshipOrientation = CheckIfOrientationIsHorizontal() ? WarshipOrientation.VERTICAL : WarshipOrientation.HORIZONTAL;
    }

    private bool CheckIfOrientationIsHorizontal() {
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

    //do przeniesienia w inne miejsce 
    private Vector3 offset;
    private Vector3 positionOfField;
    private bool clickedNotDraged;
    private Vector2 fieldGridPosition;
    private int i;

    public string toStringShort()
    {
        return "Size: " + warshipSize + " x: " + x + " y: " + y + " " + warshipOrientation; 
    }
}
