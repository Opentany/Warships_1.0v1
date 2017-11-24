using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldModel {

    private int x;
    private int y;
    private DmgDone dmgDone;
    private PlacementResult placementResult;
    public Warship warship;
    public int secureFieldCounter;
    public bool shotted;

    public FieldModel(int i, int j)
    {
        x = i;
        y = j;
        shotted = false;
        warship = null;
        placementResult = PlacementResult.AVAILABLE;
        secureFieldCounter = 0;
    }

    public void SetShotResult(DmgDone result)
    {
        dmgDone = result;
    }

    public PlacementResult GetPlacementResult()
    {
        return placementResult;
    }

    public void SetPlacementResult(PlacementResult placementResult)
    {
        this.placementResult = placementResult;
        ChangeSecureCounterIfNeeded(placementResult);
    }

    private void ChangeSecureCounterIfNeeded(PlacementResult placementResult)
    {
        if (placementResult.Equals(PlacementResult.SECURE))
        {
            secureFieldCounter++;
        }
        else
        {
            secureFieldCounter = 0;
        }
    }

    public Warship GetWarship()
    {
        return warship;
    }

    public void SetWarship(Warship warship)
    {
        this.warship = warship;
    }


    public int GetSecureFieldCounter()
    {
        return secureFieldCounter;
    }

    public void DecreaseSecureFieldCounter()
    {
        secureFieldCounter--;
    }
}
