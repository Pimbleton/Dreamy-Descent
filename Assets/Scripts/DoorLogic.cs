using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class DoorLogic : MonoBehaviour {
    public GameObject player;
    private GameObject currentRoom;
    private GameObject northRoom, southRoom, eastRoom, westRoom;
    private Vector3 spawnPoint;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        currentRoom = GetComponentInParent<RoomAdjacency>().gameObject;
        northRoom = GetComponentInParent<RoomAdjacency>().northRoom;
        southRoom = GetComponentInParent<RoomAdjacency>().southRoom;
        eastRoom = GetComponentInParent<RoomAdjacency>().eastRoom;
        westRoom = GetComponentInParent<RoomAdjacency>().westRoom;
    }

    void OnTriggerEnter2D(Collider2D other) {
        string objectTag = other.tag;

        if (objectTag == "Player") {
            switch (gameObject.name) {
                case "DoorWest":
                    westRoom.SetActive(true);
                    MoveCamera(westRoom);
                    spawnPoint = new Vector3(westRoom.transform.position.x + 8f, westRoom.transform.position.y, 0f);
                    break;
                case "DoorEast":
                    eastRoom.SetActive(true);
                    MoveCamera(eastRoom);
                    spawnPoint = new Vector3(eastRoom.transform.position.x - 8f, eastRoom.transform.position.y, 0f);
                    break;
                case "DoorNorth":
                    northRoom.SetActive(true);
                    MoveCamera(northRoom);
                    spawnPoint = new Vector3(northRoom.transform.position.x, northRoom.transform.position.y - 3.9f, 0f);
                    break;
                case "DoorSouth":
                    southRoom.SetActive(true);
                    MoveCamera(southRoom);
                    spawnPoint = new Vector3(southRoom.transform.position.x, southRoom.transform.position.y + 3.9f, 0f);
                    break;
            }

            player.transform.position = spawnPoint;
            currentRoom.SetActive(false);
        }
    }
    
    void MoveCamera(GameObject room) {
        Camera.main.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, Camera.main.transform.position.z);
    }
}
