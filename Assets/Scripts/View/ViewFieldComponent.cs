﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFieldComponent : MonoBehaviour {

    public GameObject GameController;
    public GameObject GameplayControl;
	private WarshipPlacer warshipPlacer;

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
		warshipPlacer = new WarshipPlacer ();
	}


    public void OnMouseDown()
    {
		Debug.Log ("pole " + gridPosition.x + " " + gridPosition.y);

        if (isMini)
        {
            return;
        }
		if (gameplayController != null && !GameplayController.IsGameEnd())
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
        GameplayControl = GameObject.FindGameObjectWithTag("GameplayController");
        if (GameController != null)
        {
            thsPreparationController = GameController.GetComponent<PreparationController>();
        }
        if (GameplayControl != null)
        {
            gameplayController = GameplayControl.GetComponent<GameplayController>();
        }
    }

    public void SetWarshipColor()
    {
        renderer = this.GetComponent<SpriteRenderer>();
		renderer.material.color = Color.red;
    }
		
	public void SetColorWhenWarshipSinked(){
		renderer = this.GetComponent<SpriteRenderer>();
		renderer.material.color = Color.white;
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
        gameplayController.AttackEnemy((int)gridPosition.x, (int)gridPosition.y);
    }

    private void OnClickInPreparation(){
		warshipPlacer.TryPutWarshipOnField(this);
	}


	public void ChangeSprite(Sprite warshipSprite){
		renderer = GetComponent<SpriteRenderer>();
		renderer.sprite = warshipSprite;
	}

}
