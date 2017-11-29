
public class PlacementField : BaseField {

    public int securePoints;

    public PlacementField() : base()
    {
        securePoints = 0;
    }

    public bool isSecure()
    {
        return securePoints > 0;
    }

}
