using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerController : MonoBehaviour
{
    public RoomInfo[] roomInfos;
	// Use this for initialization
	void Start ()
    {
		Connect();
	}

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("Warships_1.0v1");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        roomInfos = PhotonNetwork.GetRoomList();
    }

    public void RefreshRoomList()
    {
        roomInfos = PhotonNetwork.GetRoomList();
    }
    public void CreateRoom(string roomName)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;
        roomOptions.PlayerTtl = 120000;
        PhotonNetwork.CreateRoom(roomName,roomOptions,TypedLobby.Default);
    }
    

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
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
