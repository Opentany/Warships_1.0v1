using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{

    public override void ArrangeBoard()
    {
        throw new System.NotImplementedException();
    }

    public override void SetPlayerBoard(WarshipsContainer warshipsContainer)
    {
        shipsContainer = warshipsContainer;
        //controller.SetMyShips(warshipsContainer.GetWarships());
    }

}
