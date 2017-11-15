using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PreparationController : MonoBehaviour {

    public Board preparationBoard;
    public GameObject fieldPrefab;

    private WarshipCreator warshipCreator;
    // Use this for initialization
    void Start()
    {
        preparationBoard = new Board(fieldPrefab);
        warshipCreator = new WarshipCreator();
    }

    private void CreateWarships() {

    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}



