using UnityEngine;

public class DoorLogic : MonoBehaviour {
    [Header("Room References")]
    public GameObject currentRoom, northRoom, southRoom, eastRoom, westRoom;

    [Header("Misc")]
    private CameraScaling scaler;
    private GameObject player;
    private Vector3 spawnPoint;
    private static bool isTransitioning = false;

    void Start() {
        // Initialize appropriate references.
        currentRoom = GetComponentInParent<RoomAttributes>().gameObject;
        northRoom = GetComponentInParent<RoomAttributes>().northRoom;
        southRoom = GetComponentInParent<RoomAttributes>().southRoom;
        eastRoom = GetComponentInParent<RoomAttributes>().eastRoom;
        westRoom = GetComponentInParent<RoomAttributes>().westRoom;
        player = GameObject.FindGameObjectWithTag("Player");
        scaler = Camera.main.GetComponent<CameraScaling>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !isTransitioning) {
            GameObject targetRoom = null;

            // If player enters door trigger, teleport player into next room according to which direction door was entered.
            switch (gameObject.name) {
                case "W_Door":
                    targetRoom = westRoom;
                    spawnPoint = new Vector3(westRoom.transform.position.x + 8f, westRoom.transform.position.y, 0f);
                    break;
                case "E_Door":
                    targetRoom = eastRoom;
                    spawnPoint = new Vector3(eastRoom.transform.position.x - 8f, eastRoom.transform.position.y, 0f);
                    break;
                case "N_Door":
                    targetRoom = northRoom;
                    spawnPoint = new Vector3(northRoom.transform.position.x, northRoom.transform.position.y - 3.9f, 0f);
                    break;
                case "S_Door":
                    targetRoom = southRoom;
                    spawnPoint = new Vector3(southRoom.transform.position.x, southRoom.transform.position.y + 3.9f, 0f);
                    break;
            }

            if (targetRoom != null) {
                isTransitioning = true;
                GameObject oldRoom = currentRoom;

                targetRoom.SetActive(true);
                player.transform.position = spawnPoint;

                StartCoroutine(CameraTransition.Instance.SlideToRoom(targetRoom.transform.position, () => {
                    oldRoom.SetActive(false);
                    MoveCamera(targetRoom);
                    isTransitioning = false;
                }));
            }
        }
    }

    void MoveCamera(GameObject room) {
        if (scaler != null) {
            // Instead of transform.Find, call the script directly
            RoomAttributes attr = room.GetComponent<RoomAttributes>();
            attr.InitializeCamera(); 
        }
    }
}