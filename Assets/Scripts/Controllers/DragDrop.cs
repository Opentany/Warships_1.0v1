using UnityEngine;
using System.Collections;


public class DragDrop : MonoBehaviour
{

    private Vector3 offset;
    private Vector3 positionOfField;
    private bool clickedNotDraged;


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
        transform.position = positionOfField;
        Debug.Log(clickedNotDraged);
        if (clickedNotDraged)
        {
            transform.Rotate(Vector3.forward * +90);
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Water(Clone)")
        {
            positionOfField = coll.gameObject.transform.position;
        }
    }
}
