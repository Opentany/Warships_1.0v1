
public abstract class Player{


    protected ShootingBoard opponentBoard;
    public ShootingBoard playerBoard;
    public WarshipsContainer shipsContainer;
    public bool isYourTurn;
    public GameplayController controller;

    public Player() {
        isYourTurn = false;
        opponentBoard = new ShootingBoard();
    }

    public abstract void ArrangeBoard();

    public bool IsYourTurn() {
        return isYourTurn;
    }

    public abstract void YourTurn();

    public abstract void SetPlayerBoard(WarshipsContainer playerBoard);

    public virtual void TakeOpponentShot(ShotRaport shotRaport) {
        playerBoard.ApplyShot(shotRaport);
		controller.UpdatePlayerCounter (this,shotRaport.GetShotResult ());
    }

    public virtual void SetPlayerShotResult(ShotRaport shotRaport)
    {
        opponentBoard.ApplyShot(shotRaport);
		controller.UpdatePlayerCounter (this, shotRaport.GetShotResult ());
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

	public int GetNumberOfRemainingWarship(){
		return playerBoard.GetFieldsOccupiedByWarships ();
	}

}
