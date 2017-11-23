
public abstract class Player{


    public BoardModel opponentBoard;
    public BoardModel playerBoard;
    public WarshipsContainer shipsContainer;
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

    public abstract void SetPlayerBoard(WarshipsContainer playerBoard);

    public virtual void TakeOpponentShot(ShotRaport shotRaport) {
        playerBoard.ApplyShot(shotRaport);
    }

    public virtual void SetPlayerShotResult(ShotRaport shotRaport)
    {
        opponentBoard.ApplyShot(shotRaport);
    }

    public BoardModel GetPlayerBoard()
    {
        return playerBoard;
    }

    public WarshipsContainer GetPlayerShips()
    {
        return shipsContainer;
    }

    public bool CheckIfYouWin() {
        return opponentBoard.GetFieldsOccupiedByWarships() == 0;
    }

}
