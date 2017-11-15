using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    public Vector2 gridPosition = Vector2.zero;
    private ShotResult shotResult;
    private PlacementResult placementResult;

    void Start(){
        shotResult = ShotResult.UNCHECK;
        placementResult = PlacementResult.AVAILABLE;
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

    public PlacementResult GetPlacementResult() {
        return placementResult;
    }

    public void SetPlacementResult(PlacementResult placementResult) {
        this.placementResult = placementResult;
    }

    private void CheckShot() {

    }

}
