using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	public void SendEventTest()
	{
		byte evCode = 0;    // my event 0. could be used as "group units"
		byte[] content = new byte[] { 1, 2, 5, 10 };    // e.g. selected unity 1,2,5 and 10
		bool reliable = true;
		PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
	}

	// setup our OnEvent as callback:
	void OnEnable()
	{
		PhotonNetwork.OnEventCall += this.OnEvent;
	}

	void OnDisable()
	{
		PhotonNetwork.OnEventCall -= this.OnEvent;
	}
	// handle custom events:
	void OnEvent(byte eventcode, object content, int senderid)
	{
		if (eventcode == 0)
		{
			PhotonPlayer sender = PhotonPlayer.Find(senderid);  // who sent this?
			byte[] selected = content as byte[];
			for (int i = 0; i < selected.Length; i++)
			{
				byte unitId = selected[i];
				Debug.Log(unitId + " "+ sender.NickName);
			}
		}
	}


}