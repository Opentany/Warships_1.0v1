
public abstract class Player{


    public BoardModel opponentBoard;
    public BoardModel playerBoard;
    public WarshipsContainer shipsContainer;
    private bool isYourTurn;
    public GameplayController controller;

    public Player() {
        isYourTurn = false;
        opponentBoard = new BoardModel();
    }

    public abstract void ArrangeBoard();

    public bool IsYourTurn() {
        return isYourTurn;
    }

    public abstract void YourTurn();

    public abstract void SetPlayerBoard(WarshipsContainer playerBoard);

    public virtual void TakeOpponentShot(ShotRaport shotRaport) {
        playerBoard.ApplyShot(shotRaport);
    }

    public virtual void SetPlayerShotResult(ShotRaport shotRaport)
    {
        opponentBoard.ApplyShot(shotRaport);
    }

    public WarshipsContainer GetPlayerShips()
    {
        return shipsContainer;
    }

    public bool CheckIfYouLose() {
        return playerBoard.GetFieldsOccupiedByWarships() == 0;
    }

    public void SetGameController(GameplayController controller)
    {
        this.controller = controller;
    }

}
