using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    public Vector2 gridPosition = Vector2.zero;
    private ShotResult shotResult;
    private PlacementResult placementResult;
    private Warship warship;
    private Renderer renderer;

    void Start(){
        shotResult = ShotResult.UNCHECK;
        placementResult = PlacementResult.AVAILABLE;
        renderer = GetComponent<Renderer>();
    }


    void OnMouseDown()
    {
        if (this.enabled)
        {
            Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y);
            this.enabled = false;
            this.renderer.material.color = Color.grey;
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

    public Warship GetWarship() {
        return warship;
    }

    public void SetWarship(Warship warship) {
        this.warship = warship;
    }

}
