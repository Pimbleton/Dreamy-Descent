using System.Collections.Generic;
using UnityEngine;

public class FloorManagement : MonoBehaviour
{
    private GameObject[] rooms;

    void Start() {
        rooms = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject room in rooms) {
            room.SetActive(false);
        }
    }
}
