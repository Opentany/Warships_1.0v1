using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{

    public GameObject GameController;
    public GameObject GameplayController;
    public Vector2 gridPosition = Vector2.zero;
    public Vector3 realPosition;
    public Quaternion realRotation;
    private DmgDone dmgDone;
    private PlacementResult placementResult;
    public Warship warship;
    private Renderer renderer;
    public int secureFieldCounter;
    private PreparationController thsPreparationController;
    private GameplayController gameplayController;
    public bool isMini = false;

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
        FindViews();
        realPosition = transform.position;
        realRotation = transform.rotation;
    }

    private void FindViews() {
        GameController = GameObject.FindGameObjectWithTag("GameController");
        GameplayController = GameObject.FindGameObjectWithTag("GameplayController");
        if (GameController != null)
        {
            thsPreparationController = GameController.GetComponent<PreparationController>();
        }
        if (GameplayController != null)
        {
           gameplayController = GameplayController.GetComponent<GameplayController>();
        }

    }

 
    void OnMouseDown()
    {
        if (isMini)
        {
            return;
        }
        if (gameplayController != null)
        {
            OnClickInGameplay();
        }
        else if (thsPreparationController != null) {
            OnClickInPreparation();
        }

       

    }

    private void OnClickInPreparation()
    {
        if (IsPressed())
        {
            Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " placement: " + placementResult.ToString());
            if (thsPreparationController.ChooseField(this))
            {
                this.enabled = false;
                this.renderer.material.color = Color.grey;
            }
        }
        else
        {
            Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " is not available" + " placement: " + placementResult.ToString() + " " + warship.GetOrientation().ToString());


        }
    }

    private void OnClickInGameplay()
    {
        if (IsPressed())
        {
            Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " placement: " + placementResult.ToString());
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
