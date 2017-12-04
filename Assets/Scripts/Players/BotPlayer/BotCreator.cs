using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCreator  {

    public static BotPlayer CreateBotPlayer(/* BotLevel level */) {
        return new EasyBot();
    }


}
