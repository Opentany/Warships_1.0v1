using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public MultiplayerController MultiplayerController;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RoomselectFromDropdown()
    {
        Debug.Log("wybrano");
        Dropdown name = FindObjectOfType<Dropdown>();

        MultiplayerController.ChooseRoom(name.options[name.value].text);

    }

    public void RoomSelectFromText()
    {
        var name = FindObjectOfType<InputField>().text;
        MultiplayerController.ChooseRoom(name);
        Debug.Log(MultiplayerController.GetChosenRoomName());
    }
    public void RefreshRoomList()
    {
        MultiplayerController.RefreshRoomList();
        Dropdown dropdown = FindObjectOfType<Dropdown>();
        RoomInfo[] roomInfos = MultiplayerController.GetRoomInfos();
        GameObject text = GameObject.FindWithTag("ListOfRooms");
        text.GetComponent<Text>().text = "";
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string> { "Brak" });
        List<string> roomnames = new List<string>();
        foreach (var roomInfo in roomInfos)
        {
            roomnames.Add(roomInfo.Name);
            text.GetComponent<Text>().text += "+ " + roomInfo.Name + "\n";
        }
        dropdown.AddOptions(roomnames);
    }

    public void JoinRoom()
    {
        MultiplayerController.JoinRoom();
    }
}
