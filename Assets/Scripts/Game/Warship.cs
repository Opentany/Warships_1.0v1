using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warship : MonoBehaviour {

    private int x;
    private int y;
    private WarshipSize warshipSize;
    private WarshipOrientation warshipOrientation;
    private bool isSinked;
    private int durability;

    public Warship(WarshipSize warshipSize) {
        x = 0;
        y = 0;
        this.warshipSize = warshipSize;
        warshipOrientation = WarshipOrientation.HORIZONTAL;
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

    public void ChangeDurability() {
        durability--;
        if (durability == 0) {
            isSinked = true;
        }
    }

    public int GetSize() {
        return (int)warshipSize;
    }

    private void ChangeOrientation() {
        warshipOrientation = CheckIfOrientationIsHorizontal() ? WarshipOrientation.VERTICAL : WarshipOrientation.HORIZONTAL;
    }

    private bool CheckIfOrientationIsHorizontal() {
        return warshipOrientation.Equals(WarshipOrientation.HORIZONTAL);
    }
}
