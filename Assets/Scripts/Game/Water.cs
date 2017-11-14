using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public Vector2 gridPosition = Vector2.zero;

    void Start(){

    }

    void OnMouseDown()
    {
        if (this.enabled)
        {
            Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y);
            this.enabled = false;
        }
        else {
            Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " is not available");

        }

    }

    private void CheckShot() {

    }
}
