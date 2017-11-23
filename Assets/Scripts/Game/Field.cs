using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{

    public GameObject GameController;
    public Vector2 gridPosition = Vector2.zero;
    public Vector3 realPosition;
    public Quaternion realRotation;
    private DmgDone dmgDone;
    private PlacementResult placementResult;
    public Warship warship;
    private Renderer renderer;
    public int secureFieldCounter;
    private PreparationController thsPreparationController;

    public Field() {
        warship = null;
        placementResult = PlacementResult.AVAILABLE;
        secureFieldCounter = 0;
    }


    void Start(){
        warship = null;
        placementResult = PlacementResult.AVAILABLE;
        renderer = GetComponent<Renderer>();
        secureFieldCounter = 0;
        thsPreparationController = GameController.GetComponent<PreparationController>();
        realPosition = transform.position;
        realRotation = transform.rotation;
    }


    void OnMouseDown()
    {
        
        if (IsPressed())
        {
            Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " placement: " + placementResult.ToString());
            this.enabled = false;
            this.renderer.material.color = Color.grey;
            thsPreparationController.ChooseField(this);

        }
        else
        {
            Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " is not available" + " placement: " + placementResult.ToString() + " " + warship.GetOrientation().ToString());
            

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
