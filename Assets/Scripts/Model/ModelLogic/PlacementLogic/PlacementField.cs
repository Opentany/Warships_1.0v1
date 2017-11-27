
public class PlacementField : BaseField {

    public PlacementResult placementResult;
    public int securePoints;

    public PlacementField() : base()
    {
        securePoints = 0;
        placementResult = PlacementResult.AVAILABLE;
    }

    public void SetPlacementResult(PlacementResult placementResult)
    {
        this.placementResult = placementResult;
    }

    public PlacementResult GetPlacementResult()
    {
        return this.placementResult;
    }

    public int GetSecureFieldCounter()
    {
        return securePoints;
    }

    public void DecreaseSecureFieldCounter()
    {
        securePoints--;
    }

    public override void SetWarship(Warship warship)
    {
        base.SetWarship(warship);
        placementResult = PlacementResult.INACCESSIBLE;
    }

    public override void RemoveWarship()
    {
        base.RemoveWarship();
        placementResult = PlacementResult.AVAILABLE;
    }
}
