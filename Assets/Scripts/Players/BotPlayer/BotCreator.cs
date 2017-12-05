using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCreator  {

    public static BotPlayer CreateBotPlayer() {
        BotLevel level = BotLevelInfo.botLevel;
        switch (level)
        {
            case BotLevel.EASY:
                return new BotPlayer(Variables.EASY_PRECISION);
            case BotLevel.AVERAGE:
                return new BotPlayer(Variables.EASY_PRECISION);
            case BotLevel.GREAT:
                return new BotPlayer(Variables.EASY_PRECISION);
            default:
                throw new BotLogicException("Unknown bot level");
        }
    }
}
