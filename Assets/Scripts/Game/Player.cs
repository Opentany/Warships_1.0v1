
public abstract class Player{

    protected Board opponentBoard;
    protected Board playerBoard;
    protected GameController gameController;
    protected PreparationController preparationController;
    private bool isYourTurn;

    public Player() {
    }

    public abstract void ArrangeBoard();

    public bool IsYourTurn() {
        return isYourTurn;
    }

    public virtual void YourTurn()
    {
        isYourTurn = true;
    }

    public virtual void SetPlayerBoard(Board playerBoard)
    {
        this.playerBoard = playerBoard;
    }

    public virtual void SetOpponentBoard(Board opponentBoard) {
        this.opponentBoard = opponentBoard;
    }

    public virtual void TakeOpponentShot(ShotRaport shotRaport) {
        playerBoard.ApplyShot(shotRaport);
    }

    public virtual void SetPlayerShotResult(ShotRaport shotRaport)
    {
        opponentBoard.ApplyShot(shotRaport);
    }

    public Board GetPlayerBoard()
    {
        return playerBoard;
    }

    public void SetGameController(GameController gameController)
    {
        this.gameController = gameController;
    }

    public void SetPreparationController(PreparationController preparationController)
    {
        this.preparationController = preparationController;
    }

}
