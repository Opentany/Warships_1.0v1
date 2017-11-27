public class HumanPlayer : Player
{

    public override void ArrangeBoard()
    {
        throw new System.NotImplementedException();
    }

    public override void SetPlayerBoard(WarshipsContainer warshipsContainer)
    {
        playerBoard = new ShootingBoard();
        opponentBoard = new ShootingBoard();
        opponentBoard.SetFieldsOccupiedByWarships(20);
        this.shipsContainer = warshipsContainer;
        foreach (Warship ship in shipsContainer.GetWarships()){
            playerBoard.SetWarship(ship);
        }
    }

    public override void YourTurn()
    {
        controller.activeHuman = true;
    }
}
