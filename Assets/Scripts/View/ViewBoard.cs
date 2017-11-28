﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBoard : BaseBoard<ViewField> {

    private static GameObject waterPrefab;
    private static GameObject miniWaterPrefab;
	private static GameObject warshipOnMinimap;
    private List<List<ViewField>> miniBoard;

    public ViewBoard() {
        board = new List<List<ViewField>>();
        miniBoard = new List<List<ViewField>>();
        warshipList = new List<Warship>();
        fieldsOccupiedByWarships = 0;
    }
		

    public void GenerateBoardOnScreen() {
        board = new List<List<ViewField>>();
        float fieldSize = waterPrefab.GetComponent<BoxCollider2D>().size.x + Variables.fieldMargin;
        for (int i = 0; i < boardSize; i++) {
            List<ViewField> row = new List<ViewField>();
            for (int j = 0; j < boardSize; j++) {
                ViewField field = new ViewField();
				ViewFieldComponent component = GameObject.Instantiate (waterPrefab, new Vector2 (Variables.screenHorizontalOffset + i * fieldSize, Variables.screenVerticalOffset - j * fieldSize), Quaternion.Euler (new Vector2 ())).GetComponent<ViewFieldComponent>();
				field.SetViewFieldComponent(component);
                field.viewFieldComponent.gameObject.layer = 1;
                field.viewFieldComponent.gridPosition = new Vector2(i, j);
                row.Add(field);
            }
            board.Add(row);
        }
    }


    public void GenerateMiniBoardOnScreen()
    {
		miniWaterPrefab.SetActive(true);
        miniBoard = new List<List<ViewField>>();
        float fieldSize = waterPrefab.GetComponent<BoxCollider2D>().size.x / 2 + Variables.fieldMargin / 2;
        for (int i = 0; i < boardSize; i++)
        {
            List<ViewField> row = new List<ViewField>();
            for (int j = 0; j < boardSize; j++)
            {
                ViewField field = new ViewField();
				ViewFieldComponent component = GameObject.Instantiate(miniWaterPrefab, new Vector2(Variables.screenHorizontalOffset + i * fieldSize, Variables.miniBoardScreenVerticalOffset - j * fieldSize), Quaternion.Euler(new Vector2())).GetComponent<ViewFieldComponent>();
                field.SetViewFieldComponent(component);
                field.viewFieldComponent.gameObject.layer = 1;
                field.viewFieldComponent.gridPosition = new Vector2(i, j);
                field.isMini = true;
                row.Add(field);
            }
            miniBoard.Add(row);
        }
        miniWaterPrefab.SetActive(false);
    }

    public static void SetWaterPrefab(GameObject water) {
        waterPrefab = water;
        CreateMiniWaterPrefab(water);
    }

    private static void CreateMiniWaterPrefab(GameObject water) {
        miniWaterPrefab = GameObject.Instantiate(water) as GameObject;
        miniWaterPrefab.transform.localScale = miniWaterPrefab.transform.localScale / 2;
		miniWaterPrefab.SetActive(false);
    }

	public static void SetWarshipPrefab(GameObject warship){
		warshipOnMinimap = warship;
	}

    public List<List<ViewField>> GetMiniBoard() {
        return miniBoard;
    }

    public void ApplyShot(ShotRaport shotRaport) {
        int x = shotRaport.GetX();
        int y = shotRaport.GetY();
        board[x][y].SetShotResult(shotRaport.GetShotResult());
        if (CheckIfFieldWasShot(shotRaport)) {
            fieldsOccupiedByWarships--;
        }
    }

	private bool CheckIfFieldWasShot(ShotRaport shotRaport) {
		return (shotRaport.GetShotResult().Equals(DmgDone.HIT) || shotRaport.GetShotResult().Equals(DmgDone.SINKED));
	}

    public void ApplyMyShot(ShotRaport shotRaport) {
        int x = shotRaport.GetX();
        int y = shotRaport.GetY();
        board[x][y].SetShotResult(shotRaport.GetShotResult());
		board[x][y].SetEffectOnField(shotRaport.GetShotResult());
		if (shotRaport.GetShotResult().Equals(DmgDone.SINKED)){
			AddEffectOnWholeWarship(shotRaport.GetWarship());
		}
    }

	private void AddEffectOnWholeWarship(Warship warship){
		if (warship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL))
		{
			int x = warship.GetXPosition();
			for (int i = x; i < x + warship.GetSize(); i++){
				miniBoard [i] [warship.GetYPosition ()].SetEffect ();
			}
		}
		else {
			int y = warship.GetYPosition();
			for (int i = y; i < y + warship.GetSize (); i++) {
				miniBoard [warship.GetXPosition ()] [i].SetEffect();
			}
		}
	}

    public void ApplyOpponentShot(ShotRaport shotRaport) {
        int x = shotRaport.GetX();
        int y = shotRaport.GetY();
        Debug.Log("Bot " + x + " "+ y+ " "+ shotRaport.GetShotResult());
        miniBoard[x][y].SetShotResult(shotRaport.GetShotResult());
		miniBoard[x][y].SetColorOnField(shotRaport.GetShotResult());
		if (shotRaport.GetShotResult().Equals(DmgDone.SINKED)){
			AddColorOnWholeWarship(shotRaport.GetWarship());
		}
    }

	private void AddColorOnWholeWarship(Warship warship){
		if (warship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL))
		{
			int x = warship.GetXPosition();
			for (int i = x; i < x + warship.GetSize(); i++){
				miniBoard [i] [warship.GetYPosition ()].SetWarshipColor ();
			}
		}
		else {
			int y = warship.GetYPosition();
			for (int i = y; i < y + warship.GetSize (); i++) {
				miniBoard [warship.GetXPosition ()] [i].SetWarshipColor ();
			}
		}
	}
		

    public void DisplayBoard()
    {
        string row="";
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                row += " " + board[i][j].secureFieldCounter;
            }
            row = "";
        }
    }

    public void SetWarshipOnMiniBoard(List<Warship> warships) {
        foreach (Warship warship in warships) {
            if (warship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL))
            {
				AddWarshipFieldHorizontal(warship);
            }
            else {
				AddWarshipFieldVertical(warship);
            }
        }
    }

	private void AddWarshipFieldHorizontal(Warship warship) {
        int x = warship.GetXPosition();
        for (int i = x; i < x + warship.GetSize(); i++)
        {
			miniBoard [i] [warship.GetYPosition ()].viewFieldComponent.ChangeSprite(GetWarshipSpriteRenderer());
        }
    }

	private void AddWarshipFieldVertical(Warship warship){
        int y = warship.GetYPosition();
		for (int i = y; i < y + warship.GetSize (); i++) {
			miniBoard [warship.GetXPosition ()] [i].viewFieldComponent.ChangeSprite (GetWarshipSpriteRenderer ());
		}
	}

	private Sprite GetWarshipSpriteRenderer(){
		return warshipOnMinimap.GetComponent<SpriteRenderer>().sprite;
	}


}