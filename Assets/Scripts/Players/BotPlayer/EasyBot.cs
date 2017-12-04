using UnityEngine;

public class EasyBot : BotPlayer
{

    public override void YourTurn()
    {
        System.Random rnd = new System.Random();
        int x = rnd.Next(BaseBoard<BaseField>.boardSize);
        int y = rnd.Next(BaseBoard<BaseField>.boardSize);
        controller.ShotOpponent(x, y);
    }

}
