using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarshipCreator : MonoBehaviour {

    List<Warship> warshipList;

    public WarshipCreator() {
        GenerateWarshipList();
    }

    private void GenerateWarshipList() {
        
    }


    public List<Warship> GetWarshipList() {
        return warshipList;
    }
	
}
