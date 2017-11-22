using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    public Vector2 gridPosition = Vector2.zero;
    private DmgDone dmgDone;
    private PlacementResult placementResult;
    private Warship warship;
    private Renderer renderer;
    private int secureFieldCounter;

    void Start(){
        warship = null;
        placementResult = PlacementResult.AVAILABLE;
        renderer = GetComponent<Renderer>();
        secureFieldCounter = 0;
    }


    void OnMouseDown()
    {
        if (IsPressed())
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

    public void SetShotResult(DmgDone result) {
        dmgDone = result;
    }

    public PlacementResult GetPlacementResult() {
        return placementResult;
    }

    public void SetPlacementResult(PlacementResult placementResult) {
        this.placementResult = placementResult;
        ChangeSecureCounterIfNeeded(placementResult);
    }

    private void ChangeSecureCounterIfNeeded(PlacementResult placementResult) {
        if (placementResult.Equals(PlacementResult.SECURE))
        {
            secureFieldCounter++;
        }
        else {
            secureFieldCounter = 0;
        }
    }

    public Warship GetWarship() {
        return warship;
    }

    public void SetWarship(Warship warship) {
        this.warship = warship;
    }

    public bool IsPressed() {
        return this.enabled;
    }

    public int GetSecureFieldCounter() {
        return secureFieldCounter;
    }

    public void DecreaseSecureFieldCounter() {
        secureFieldCounter--;
    }

}
