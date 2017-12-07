using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerController : MonoBehaviour
{
    private string chosenRoom;
    private RoomInfo[] roomInfos;
	// Use this for initialization
	void Start ()
    {
		SettingsController.SetMusicVolumeInScene ();
		MusicController.SetActualMusic (Variables.PREPARATION_MUSIC);
		Connect();
        roomInfos = new RoomInfo[5];
	}

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("Warships_1.0v1");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnReceivedRoomListUpdate()
    {
        RefreshRoomList();
        Debug.Log("dołączono");
        GameObject lobby = GameObject.FindGameObjectWithTag("Lobby");
        LobbyController lobbycon = lobby.GetComponent<LobbyController>();
        lobbycon.RefreshRoomList();
    }

    public void RefreshRoomList()
    {
        roomInfos = PhotonNetwork.GetRoomList();
        Debug.Log(roomInfos == null);
    }

    public void Exit()
    {
        PhotonNetwork.Disconnect();
    }
    public RoomInfo[] GetRoomInfos()
    {
        return roomInfos;
    }
    public void ChooseRoom(string name)
    {
        chosenRoom = name;
    }

    public string GetChosenRoomName()
    {
        return chosenRoom;
    }
    public void JoinRoom()
    {
        if (chosenRoom=="Brak")
        {
            chosenRoom = null;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;
        roomOptions.PlayerTtl = 120000;
        roomOptions.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom(chosenRoom, roomOptions, TypedLobby.Default);
    }
    void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.player.NickName = "Player1";
        }
        else
        {
            PhotonNetwork.player.NickName = "Player2";
        }
       
        var text = GameObject.FindGameObjectWithTag("Player_Name");
        text.GetComponent<Text>().text = PhotonNetwork.playerName;
        PhotonNetwork.LoadLevel("Preparation_Multi");
    }
	
}
