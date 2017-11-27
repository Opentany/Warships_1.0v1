using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseField {

    private int x;
    private int y;
    public Warship warship;

    public BaseField(int i, int j)
    {
        x = i;
        y = j;
        warship = null;
    }

    public BaseField()
    {
        x = 0;
        y = 0;
        warship = null;
    }

    public virtual void SetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Warship GetWarship()
    {
        return warship;
    }

    public virtual void SetWarship(Warship warship)
    {
        this.warship = warship;
    }

    public virtual void RemoveWarship()
    {
        this.warship = null;
    }
}
