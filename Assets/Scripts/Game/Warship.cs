using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warship : MonoBehaviour {

    enum WarshipSize { ONE, TWO, THREE, FOUR }
    enum WarshipOrientation { VERTICAL, HORIZONTAL }

    private int x;
    private int y;
    private WarshipSize warshipSize;
    private WarshipOrientation warshipOrientation;
    private bool isSinked;
    private int durability;



    private void ChangeWarshipOrientation() {
        warshipOrientation = CheckIfWarshipOrientationIsHorizontal() ? WarshipOrientation.VERTICAL : WarshipOrientation.HORIZONTAL;
    }

    private bool CheckIfWarshipOrientationIsHorizontal() {
        return warshipOrientation.Equals(WarshipOrientation.HORIZONTAL);
    }
}
