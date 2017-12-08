public class HumanPlayer : DevicePlayer
{
    public static PlacementBoard placementBoard;
    public static ShootingBoard enemyBoard;
    public static ViewBoard viewBoard;
    private WarshipPlacer warshipPlacer;

    public HumanPlayer()
    {
        isYourTurn = false;
        placementBoard = new PlacementBoard();
        enemyBoard = new ShootingBoard();
    }

    public void SetMultiGameController(GameplayController gameplayController)
    {
        this.gameplayController = gameplayController;
    }

    public override void SetPlayerBoard()
    {
        shipsContainer = placementBoard.GetWarshipList();
        playerBoard = new ShootingBoard();
        foreach (Warship ship in shipsContainer.GetWarships()){
            playerBoard.SetWarship(ship);
        }
    }

    public static void SetViewBoard(ViewBoard viewBoard)
    {
        HumanPlayer.viewBoard = viewBoard;
    }

    public override void SetPlayerShotResult(ShotRaport shotRaport)
    {
        enemyBoard.ApplyShot(shotRaport);
        if (shotRaport.GetShotResult().Equals(DmgDone.SINKED))
        {
            ShipRevealer revealer = new ShipRevealer(enemyBoard, shotRaport);
            Warship ship = revealer.GetWarship();
            shotRaport.SetWarship(ship);
        }
        viewBoard.ApplyMyShot(shotRaport);
    }


    public override void TakeOpponentShot(Position target)
    {
        ShotRaport shotRaport = new ShotRaport(target.x, target.y, playerBoard);
        playerBoard.ApplyShot(shotRaport);
        viewBoard.ApplyOpponentShot(shotRaport);
        this.gameplayController.SendShotRaport(shotRaport);
    }

    public void SetShipsOnBoard()
    {
        viewBoard.SetWarshipOnMiniBoard(playerBoard.GetWarshipList().GetWarships());
    }

    public override void ArrangeBoard()
    {
    }

    public override void YourTurn()
    {
    }

}
