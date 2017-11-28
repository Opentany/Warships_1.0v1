﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewField : BaseField
{
    private DmgDone dmgDone;
    private PlacementResult placementResult;
    public int secureFieldCounter;
    public bool isMini = false;
    public ViewFieldComponent viewFieldComponent;
	public ViewAnimationComponent viewAnimationComponent;

    public ViewField() :base() {
        warship = null;
        placementResult = PlacementResult.AVAILABLE;
        secureFieldCounter = 0;
    }

    public void SetViewFieldComponent(ViewFieldComponent component)
    {
        this.viewFieldComponent = component;
    }

	public void SetViewAnimationComponent(ViewAnimationComponent animationComponent){
		this.viewAnimationComponent = animationComponent;
	}

    public void SetWarshipColor() {
        viewFieldComponent.SetWarshipColor();
    }

	public void SetEffect(){
		viewAnimationComponent.SetEffect (Variables.animationTriggerHit);
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

    public bool IsPressed() {
        return viewFieldComponent.IsPressed();
    }

    public int GetSecureFieldCounter() {
        return secureFieldCounter;
    }

    public void DecreaseSecureFieldCounter() {
        secureFieldCounter--;
    }

    public void SetColorOnField(DmgDone shotResult)
    {
        viewFieldComponent.SetColorOnField(shotResult);
    }
		
    public void SetEffectOnField(DmgDone shotResult) {
        viewAnimationComponent.SetEffectOnField(shotResult);       
    }

}
