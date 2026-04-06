using UnityEngine;

public class DoorLogic : MonoBehaviour {
    private GameObject player;
    private GameObject currentRoom;
    private GameObject northRoom;
    private GameObject southRoom;
    private GameObject eastRoom;
    private GameObject westRoom;
    private Vector3 spawnPoint;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        currentRoom = GetComponentInParent<RoomAttributes>().gameObject;
        northRoom = GetComponentInParent<RoomAttributes>().northRoom;
        southRoom = GetComponentInParent<RoomAttributes>().southRoom;
        eastRoom = GetComponentInParent<RoomAttributes>().eastRoom;
        westRoom = GetComponentInParent<RoomAttributes>().westRoom;
    }

    void OnTriggerEnter2D(Collider2D other) {
        string objectTag = other.tag;

        if (objectTag == "Player") {
            switch (gameObject.name) {
                case "W_Door":
                    westRoom.SetActive(true);
                    MoveCamera(westRoom);
                    spawnPoint = new Vector3(westRoom.transform.position.x + 8f, westRoom.transform.position.y, 0f);
                    break;
                case "E_Door":
                    eastRoom.SetActive(true);
                    MoveCamera(eastRoom);
                    spawnPoint = new Vector3(eastRoom.transform.position.x - 8f, eastRoom.transform.position.y, 0f);
                    break;
                case "N_Door":
                    northRoom.SetActive(true);
                    MoveCamera(northRoom);
                    spawnPoint = new Vector3(northRoom.transform.position.x, northRoom.transform.position.y - 3.9f, 0f);
                    break;
                case "S_Door":
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
