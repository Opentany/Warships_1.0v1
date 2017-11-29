using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFieldComponent : MonoBehaviour {

    public GameObject GameController;
    public GameObject GameplayController;

	public Vector2 gridPosition = Vector2.zero;
    public Vector3 realPosition;
    public Quaternion realRotation;
    private SpriteRenderer renderer;
    private PreparationController thsPreparationController;
    private GameplayController gameplayController;
	public bool isMini;

    void Start () {
        FindViews();
        realPosition = transform.position;
        realRotation = transform.rotation;
		isMini = false;
	}


    public void OnMouseDown()
    {
		Debug.Log ("pole " + gridPosition.x + " " + gridPosition.y);

        if (isMini)
        {
            return;
        }
        if (gameplayController != null)
        {
            OnClickInGameplay();
        }
        else if (thsPreparationController != null)
        {
            OnClickInPreparation();
        }
    }

    private void FindViews()
    {
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

    public void SetWarshipColor()
    {
        renderer = this.GetComponent<SpriteRenderer>();
        renderer.material.color = Color.black;
    }
		

    public void SetColorOnField(DmgDone shotResult)
    {
        renderer = this.GetComponent<SpriteRenderer>();
		if (shotResult.Equals(DmgDone.HIT) || shotResult.Equals(DmgDone.SINKED))
        {
            renderer.material.color = Color.red;
        }
        else if (shotResult.Equals(DmgDone.MISS))
        {
            renderer.material.color = Color.grey;
        }
    }

    public bool IsPressed()
    {
        return this.enabled;
    }

    private void OnClickInGameplay()
    {
       if (IsPressed())
        {
           // Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " placement: " + placementResult.ToString());
        }
        else
        {
            //Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " is not available" + " placement: " + placementResult.ToString() + " " + warship.GetOrientation().ToString());
        }
        gameplayController.AttackEnemy((int)gridPosition.x, (int)gridPosition.y);
    }

    private void OnClickInPreparation()
    {
		thsPreparationController.SetWarshipOnField (this);


	/*	if (IsPressed())
        {
         //   Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " placement: " + placementResult.ToString());
            if (thsPreparationController.SetWarshipOnField(this))
            {
                this.enabled = false;
            }
        }
        else
        {
            //Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " is not available" + " placement: " + placementResult.ToString() + " " + warship.GetOrientation().ToString());


        }*/
    }


	public void ChangeSprite(Sprite warshipSprite){
		renderer = GetComponent<SpriteRenderer>();
		renderer.sprite = warshipSprite;
	}

}
