public interface Player
{

    void SetPlayerShotResult(ShotRaport shotRaport);

    void TakeOpponentShot(Position target);

    void SetGameController(GameplayController gameplayController);

    void SetPreparationController(PreparationController preparationController);

    void ArrangeBoard();

    void SetPlayerBoard();

    void YourTurn();

}