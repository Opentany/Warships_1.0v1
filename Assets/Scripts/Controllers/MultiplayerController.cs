using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerController : MonoBehaviour {

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
	
}
