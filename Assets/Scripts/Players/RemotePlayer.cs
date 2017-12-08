using UnityEngine;
public class RemotePlayer : MonoBehaviour, Player
{
    GameplayController gameplayController;
    PreparationController preparationController;

    public void Start()
    {
        PhotonNetwork.OnEventCall += this.OnEvent;
    }

    public void ArrangeBoard() { }

    public void SetGameController(GameplayController gameplayController)
    {
        this.gameplayController = gameplayController;
    }

    public void SetPlayerBoard()
    {
        byte evCode =  Variables.SET_PLAYER_BOARD;
        byte[] content = new byte[] {};
        bool reliable = true;
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    public void SetPlayerShotResult(ShotRaport shotRaport)
    {
        byte evCode;
        switch (shotRaport.GetShotResult())
        {
            case DmgDone.HIT:
                evCode = Variables.SHOT_RESULT_HIT;
                break;
            case DmgDone.SINKED:
                evCode = Variables.SHOT_RESULT_SINKED;
                break;
            default:
                evCode = Variables.SHOT_RESULT_MISS;
                break;
        }
        byte[] content = new byte[] { };
        bool reliable = true;
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    public void IllegalShot()
    {
        byte evCode = Variables.SHOT_RESULT_ILLEGAL;
        byte[] content = new byte[] { };
        bool reliable = true;
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    public void SetPreparationController(PreparationController preparationController)
    {
        this.preparationController = preparationController;
    }

    public void TakeOpponentShot(Position target)
    {
        byte evCode = (byte)(10 * target.x + target.y);
        byte[] content = new byte[] { };
        bool reliable = true;
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    public void YourTurn()
    {
        byte evCode = Variables.YOUR_TURN;
        byte[] content = new byte[] { };
        bool reliable = true;
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    public void SlaveStart()
    {
        byte evCode = Variables.SLAVE_START;
        byte[] content = new byte[] { };
        bool reliable = true;
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    public void MasterStart()
    {
        byte evCode = Variables.MASTER_START;
        byte[] content = new byte[] { };
        bool reliable = true;
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    public void Initialized()
    {
        byte evCode = Variables.INITIALIZED;
        byte[] content = new byte[] { };
        bool reliable = true;
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    void OnEvent(byte eventcode, object content, int senderid)
    {
        UnityEngine.Debug.Log("EVENT CODE");
        UnityEngine.Debug.Log(eventcode);
        UnityEngine.Debug.Log("END OF EVENT CODE");
        if(eventcode < 100)
        {
            int x = eventcode / 10;
            int y = eventcode % 10;
            gameplayController.ShotOpponent(x, y);
            return;
        }
        switch (eventcode)
        {
            case Variables.SET_PLAYER_BOARD:
            {
                preparationController.otherPlayerReady = true;
                preparationController.EnemyReady();
                break;
            }
            case Variables.MASTER_START:
            {
                gameplayController.InitializeSlave(false);
                break;
            }
            case Variables.SLAVE_START:
            {
                gameplayController.InitializeSlave(true);
                break;
            }
            case Variables.INITIALIZED:
            {
                gameplayController.InitializeGame();
                break;
            }
            case Variables.YOUR_TURN:
            {
                if (!gameplayController.activeDeviceHuman)
                    UnityEngine.Debug.Log("powienien już to wiedzieć");
                gameplayController.activeDeviceHuman = true;
                break;
            }
            case Variables.SHOT_RESULT_MISS:
            {
                Position pos = gameplayController.lastHumanShot;
                ShotRaport shotRaport = new ShotRaport(pos, DmgDone.MISS);
                gameplayController.SendShotRaport(shotRaport);
                break;
            }
            case Variables.SHOT_RESULT_HIT:
            {
                Position pos = gameplayController.lastHumanShot;
                ShotRaport shotRaport = new ShotRaport(pos, DmgDone.HIT);
                gameplayController.SendShotRaport(shotRaport);
                break;
            }
            case Variables.SHOT_RESULT_SINKED:
            {
                Position pos = gameplayController.lastHumanShot;
                ShotRaport shotRaport = new ShotRaport(pos, DmgDone.SINKED);
                gameplayController.SendShotRaport(shotRaport);
                break;
            }
            case Variables.SHOT_RESULT_ILLEGAL:
            {
                Position pos = gameplayController.lastHumanShot;
                gameplayController.IllegalShot(new IllegalShotException(pos.x, pos.y));
                break;
            }
            
        }
    }

}