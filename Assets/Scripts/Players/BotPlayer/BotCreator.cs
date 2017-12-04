using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCreator  {

    public static BotPlayer CreateBotPlayer(/* BotLevel level */) {
        BotLevel level = BotLevel.GREAT;
        switch (level)
        {
            case BotLevel.EASY:
                return new EasyBot();
            case BotLevel.AVERAGE:
                return new GreaterBot(BotLevel.AVERAGE);
            case BotLevel.GREAT:
                return new GreaterBot(BotLevel.GREAT);
            default:
                throw new BotLogicException("Unknown bot level");
        }
    }


}
