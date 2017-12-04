public class ShootingField : BaseField{

    public bool hasBeenShot;
    public DmgDone dmgDone;

    public ShootingField(): base()
    {
        this.hasBeenShot = false;
        this.dmgDone = DmgDone.MISS;
    }

    public void SetShotResult(DmgDone dmgDone)
    {
        this.dmgDone = dmgDone;
        hasBeenShot = true;
    }

}
