using UnityEngine;

public class DoorLogic : MonoBehaviour {
    public GameObject player;
    private GameObject currentRoom;
    private Vector3 spawnPoint;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        currentRoom = GetComponentInParent<RoomAttributes>().gameObject;
    }

    void OnTriggerEnter2D(Collider2D other) {
        string objectTag = other.tag;

        if (objectTag == "Player") {
/*
            switch (gameObject.name) {
                case "DoorWest":
                    if (westRoom == null) {
                        boss = PickBoss.pickBoss(currentScene) + "_East";
                        westRoom = Instantiate(Resources.Load<GameObject>("Prefabs/Floor1/Rooms/" + boss));
                        westRoom.transform.position = new Vector3(currentRoom.transform.position.x - 17.8f, currentRoom.transform.position.y, 0f);
                        westRoom.GetComponent<RoomAttributes>().eastRoom = currentRoom;
                        boss = "";
                    }
                    westRoom.SetActive(true);
                    MoveCamera(westRoom);
                    spawnPoint = new Vector3(westRoom.transform.position.x + 8f, westRoom.transform.position.y, 0f);
                    break;
                case "DoorEast":
                    if (eastRoom == null){
                        boss = PickBoss.pickBoss(currentScene) + "_West";
                        eastRoom = Instantiate(Resources.Load<GameObject>("Prefabs/" + currentScene + "/Rooms/" + boss));
                        eastRoom.transform.position = new Vector3(currentRoom.transform.position.x + 17.8f, currentRoom.transform.position.y, 0f);
                        eastRoom.GetComponent<RoomAttributes>().westRoom = currentRoom;
                        boss = "";
                    }
                    eastRoom.SetActive(true);
                    MoveCamera(eastRoom);
                    spawnPoint = new Vector3(eastRoom.transform.position.x - 8f, eastRoom.transform.position.y, 0f);
                    break;
                case "DoorNorth":
                    if (northRoom == null) {
                        boss = PickBoss.pickBoss(currentScene) + "_South";
                        northRoom = Instantiate(Resources.Load<GameObject>("Prefabs/Floor1/Rooms/" + boss));
                        northRoom.transform.position = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y + 10f, 0f);
                        northRoom.GetComponent<RoomAttributes>().southRoom = currentRoom;
                        boss = "";
                    }
                    northRoom.SetActive(true);
                    MoveCamera(northRoom);
                    spawnPoint = new Vector3(northRoom.transform.position.x, northRoom.transform.position.y - 3.9f, 0f);
                    break;
                case "DoorSouth":
                    if (southRoom == null) {
                        boss = PickBoss.pickBoss(currentScene) + "_North";
                        southRoom = Instantiate(Resources.Load<GameObject>("Prefabs/Floor1/Rooms/" + boss));
                        southRoom.transform.position = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y - 10f, 0f);
                        southRoom.GetComponent<RoomAttributes>().northRoom = currentRoom;
                        boss = "";
                    }
                    southRoom.SetActive(true);
                    MoveCamera(southRoom);
                    spawnPoint = new Vector3(southRoom.transform.position.x, southRoom.transform.position.y + 3.9f, 0f);
                    break;
            }
*/
            player.transform.position = spawnPoint;
            currentRoom.SetActive(false);
        }
    }

    void MoveCamera(GameObject room) {
        Camera.main.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, Camera.main.transform.position.z);
    }
}
