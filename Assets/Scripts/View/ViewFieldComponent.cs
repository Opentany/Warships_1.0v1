﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFieldComponent : MonoBehaviour {

    public GameObject GameController;
    public GameObject GameplayController;
    public Vector2 gridPosition = Vector2.zero;
    public Vector3 realPosition;
    public Quaternion realRotation;
    private Renderer renderer;
    private PreparationController thsPreparationController;
    private GameplayController gameplayController;
	public bool isMini;

    void Start () {
        renderer = GetComponent<Renderer>();
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
        renderer = this.GetComponent<Renderer>();
        renderer.material.color = Color.green;
    }

    public void SetEffectOnField(DmgDone shotResult)
    {
        Debug.Log(gridPosition.x + " " + gridPosition.y + " " + shotResult);


        renderer = this.GetComponent<Renderer>();
        if (shotResult.Equals(DmgDone.HIT))
        {
            renderer.material.color = Color.red;
        }
        else if (shotResult.Equals(DmgDone.SINKED))
        {
            renderer.material.color = Color.black;
        }
        else if (shotResult.Equals(DmgDone.MISS))
        {
            renderer.material.color = Color.grey;
        }
    }

    public void SetColorOnField(DmgDone shotResult)
    {
        Debug.Log(gridPosition.x + " " + gridPosition.y + " " + shotResult);
        renderer = this.GetComponent<Renderer>();
        if (shotResult.Equals(DmgDone.HIT))
        {
            renderer.material.color = Color.red;
        }
        else if (shotResult.Equals(DmgDone.SINKED))
        {
            renderer.material.color = Color.black;
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
        if (IsPressed())
        {
         //   Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " placement: " + placementResult.ToString());
            if (thsPreparationController.ChooseFieldComponent(this))
            {
                this.enabled = false;
                this.renderer.material.color = Color.grey;
            }
        }
        else
        {
            //Debug.Log("Field: " + gridPosition.x + " " + gridPosition.y + " is not available" + " placement: " + placementResult.ToString() + " " + warship.GetOrientation().ToString());


        }
    }

}
