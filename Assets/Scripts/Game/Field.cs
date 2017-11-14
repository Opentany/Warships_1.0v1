using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    public Vector2 gridPosition = Vector2.zero;
    private ShotResult shotResult;

    void Start(){
        shotResult = ShotResult.UNCHECK;
    }


    void OnMouseDown()
    {
        if (this.enabled)
        {
            Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y);
            this.enabled = false;
        }
        else
        {
            Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " is not available");

        }

    }

    public void SetShotResult(ShotResult result) {
        shotResult = result;
    }

    private void CheckShot() {

    }

}
