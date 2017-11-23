
public abstract class Player{


    public Board opponentBoard;
    public Board playerBoard;
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

    public bool CheckIfYouWin() {
        return opponentBoard.GetFieldsOccupiedByWarships() == 0;
    }

}
