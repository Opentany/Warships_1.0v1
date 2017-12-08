
public abstract class DevicePlayer : Player {

    public ShootingBoard playerBoard;
    public WarshipsContainer shipsContainer;
    public bool isYourTurn;
    public PreparationController preparationController;
    public GameplayController gameplayController;

    public DevicePlayer() {
        isYourTurn = false;
    }

    public bool IsYourTurn() {
        return isYourTurn;
    }

    public abstract void YourTurn();

    public abstract void SetPlayerBoard();

    public abstract void SetPlayerShotResult(ShotRaport shotRaport);

    public void SetGameController(GameplayController gameplayController)
    {
        this.gameplayController = gameplayController;
    }

    public void SetPreparationController(PreparationController preparationController)
    {
        this.preparationController = preparationController;
    }

    public virtual void TakeOpponentShot(Position target)
    {
        ShotRaport shotRaport = new ShotRaport(target.x, target.y, playerBoard);
        playerBoard.ApplyShot(shotRaport);
        this.gameplayController.SendShotRaport(shotRaport);
    }

    public abstract void ArrangeBoard();

}
