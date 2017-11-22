using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warship : MonoBehaviour {

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


    private Vector3 offset;
    private Vector3 positionOfField;
    private bool clickedNotDraged;
    private Vector2 fieldGridPosition;


    void OnMouseDown()
    {

        offset = gameObject.transform.position -
                 Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        clickedNotDraged = true;
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        var nextPosition = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        if (nextPosition != transform.position)
        {
            clickedNotDraged = false;
        }
        transform.position = nextPosition;

    }

    void OnMouseUp()
    {
        if (GetSize() == 2 || GetSize() == 4)
        {
            if (CheckIfOrientationIsHorizontal())
            {
                positionOfField.x -= 0.25f;
            }
            else
            {
                positionOfField.y -= 0.25f;
            }
        }
        transform.position = positionOfField;
        Debug.Log(fieldGridPosition.x +";"+fieldGridPosition.y);
        //Debug.Log(clickedNotDraged);
        if (clickedNotDraged)
        {
            transform.Rotate(Vector3.back * -90);
            ChangeOrientation();
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Water(Clone)")
        {
            Field fieldunder = coll.gameObject.GetComponent<Field>();
            positionOfField = coll.gameObject.transform.position;
            fieldGridPosition = fieldunder.gridPosition;
            coll.gameObject.GetComponent(typeof(Field));
        }
    }

    public string toStringShort()
    {
        return "Size: " + warshipSize + " x: " + x + " y: " + y + " " + warshipOrientation; 
    }
}
