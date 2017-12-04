using UnityEditor;

public class AverageBot : BotPlayer
{

    public AverageBot():base()
    {
        
    }

    public override void YourTurn()
    {
        System.Random rnd = new System.Random();
        int x = rnd.Next(BaseBoard<BaseField>.boardSize);
        int y = rnd.Next(BaseBoard<BaseField>.boardSize);
        controller.ShotOpponent(x, y);
    }

    public override void SetPlayerShotResult(ShotRaport shotRaport)
    {
        base.SetPlayerShotResult(shotRaport);
    }

}