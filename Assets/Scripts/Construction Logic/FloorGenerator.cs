using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {
    [Header("Floor Attributes")]
    private int minRooms;
    private int maxRooms;
    [HideInInspector] public int currentFloor;
    [SerializeField] AudioSource audioSource;
    private Dictionary<Vector2Int, GameObject> spawnedRooms;
    

    [HideInInspector] public static FloorGenerator Instance;

    void Start() {
        Instance = this;
        Time.timeScale = 1f;
        currentFloor = 1;
        spawnedRooms = new Dictionary<Vector2Int, GameObject>();

        audioSource.clip = Resources.Load<AudioClip>("Sounds/Music/f1Music");
        audioSource.Play();

        // Sets floor preset to 1 and generates the floor.
        resolveFloorPreset(currentFloor);
        GenerateFloor();
    }

    void GenerateFloor() {
        int roomCount = Random.Range(minRooms, maxRooms + 1);
        
        // Start by placing the "Start" room at location (0,0)
        PlaceRoom(Vector2Int.zero, "Start");

        // Actual random walk generation of rooms.
        // First, a random room is selected from the already placed rooms.
        // Then, a random direction is chosen to attempt to place a new room.
        // If the target position is already occupied, redo the process until successful.
        int safetyNet = 0;
        while (spawnedRooms.Count < roomCount && safetyNet < 1000) {
            Vector2Int randomRoomPos = GetRandomPlacedRoomPos();
            Vector2Int neighborPos = randomRoomPos + GetRandomDirection();

            if (!spawnedRooms.ContainsKey(neighborPos)) {
                PlaceRoom(neighborPos, "Basic");
            }
            safetyNet++;
        }

        // Assign the special case rooms here.
        // If either fails to properly generate, we reset the floor and try again.
        if (!AssignSpecialRoom("Boss") || !AssignSpecialRoom("Item")) {
            ResetFloor();
            return;
        }
        
        foreach (var room in spawnedRooms) {
            RoomAttributes attr = room.Value.GetComponent<RoomAttributes>();

            // Setup Doors
            attr.SetupDoors(spawnedRooms);

            // Only spawn enemies in "Basic" rooms.
            if (attr.roomType == "Basic") EnemySpawner.Instance.PopulateRoom(room.Value);
            

            // Deactivate non-start rooms upon entry of the floor.
            if (!attr.name.Contains("Start")) room.Value.SetActive(false);
        }

        // Sets start room as default active room and initializes the camera to focus on it.
        GameObject startRoom = spawnedRooms[Vector2Int.zero]; 
        startRoom.SetActive(true); 
        startRoom.GetComponent<RoomAttributes>().InitializeCamera();
    }

    void PlaceRoom(Vector2Int pos, string type) {
        GameObject roomObj = null;

        // Sets a position based on grid coordinates, taking into account the rooms' dimensions.
        Vector3 spawnPos = new Vector3(pos.x * 17.8f, pos.y * 10f, 0f);
        
        // Loads the room prefab based on the current floor and type, then instantiates it at the calculated position.
        switch (type) {
            case "Start":
                roomObj = Instantiate(Resources.Load<GameObject>($"Prefabs/Rooms/Floor{currentFloor}/Start_Room"), spawnPos, Quaternion.identity);
                break;
            case "Basic":
                roomObj = Instantiate(Resources.Load<GameObject>($"Prefabs/Rooms/Floor{currentFloor}/Basic_Room_0"), spawnPos, Quaternion.identity);
                break;
        }
        
        roomObj.name = $"Room_{type}_{pos.x}_{pos.y}";
        
        RoomAttributes attr = roomObj.GetComponent<RoomAttributes>();
        attr.gridPos = pos;
        attr.roomType = type;

        spawnedRooms.Add(pos, roomObj);
    }

    Vector2Int GetRandomPlacedRoomPos() {
        // Convert dictionary keys to a list and pick one.
        List<Vector2Int> keys = new List<Vector2Int>(spawnedRooms.Keys);
        return keys[Random.Range(0, keys.Count)];
    }

    bool AssignSpecialRoom(string type) {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var entry in spawnedRooms) {
            Vector2Int pos = entry.Key;

            if (pos == Vector2Int.zero) {
                continue;
            }

            if (type == "Boss") {
                if (Mathf.Abs(pos.x) + Mathf.Abs(pos.y) == 1) {
                    continue; 
                }
            }

            // Count neighbors to find dead ends by checking each cardinal direction.
            int neighborCount = 0;
            if (spawnedRooms.ContainsKey(pos + Vector2Int.up)) neighborCount++;
            if (spawnedRooms.ContainsKey(pos + Vector2Int.down)) neighborCount++;
            if (spawnedRooms.ContainsKey(pos + Vector2Int.left)) neighborCount++;
            if (spawnedRooms.ContainsKey(pos + Vector2Int.right)) neighborCount++;

            // Use the name check to ensure we don't pick a room already converted to Boss/Item
            if (neighborCount == 1 && spawnedRooms[pos].name.Contains("Basic")) {
                deadEnds.Add(pos);
            }
        }

        Vector2Int targetPos;
        if (deadEnds.Count >= 2) {
            targetPos = deadEnds[Random.Range(0, deadEnds.Count)];
        } else {
            return false;
        }

        // Get reference to the old room and its position
        GameObject oldRoom = spawnedRooms[targetPos];
        Vector3 spawnPos = oldRoom.transform.position;

        // Prepare the new room variable
        GameObject newRoom = null;

        switch (type) {
            case "Boss":
                string bossName = PickBoss.pickBoss(currentFloor);
                newRoom = Instantiate(Resources.Load<GameObject>($"Prefabs/Rooms/Floor{currentFloor}/" + bossName + "_Room"), spawnPos, Quaternion.identity);
                EnemySpawner.Instance.SpawnBoss(newRoom.transform);
                break;
            case "Item":
                newRoom = Instantiate(Resources.Load<GameObject>($"Prefabs/Rooms/Floor{currentFloor}/Item_Room"), spawnPos, Quaternion.identity);
                break;
        }

        if (newRoom != null) {
            newRoom.name = $"Room_{type}_{targetPos.x}_{targetPos.y}";
            
            // Update the dictionary to point to the new object
            spawnedRooms[targetPos] = newRoom;

            // Update the GridPos in the new room's RoomAttributes
            newRoom.GetComponent<RoomAttributes>().gridPos = targetPos;
            newRoom.GetComponent<RoomAttributes>().roomType = type;

            // Destroy the old "Basic" room GameObject
            Destroy(oldRoom);
        }

        return true;
    }

    Vector2Int GetRandomDirection() {
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        return dirs[Random.Range(0, 4)];
    }

    public void ResetFloor () {
        // Destory every room for the current floor number
        foreach (var room in spawnedRooms) {
            Destroy(room.Value);
        }
        spawnedRooms.Clear();
        
        resolveFloorPreset(currentFloor);
        GenerateFloor();
    }

    void resolveFloorPreset(int floor) {
        // Base don floor number, set the min and max room count for that floor's generation.
        switch (floor) {
            case 1:
                minRooms = 6;
                maxRooms = 9;
                break;
            case 2:
                minRooms = 10;
                maxRooms = 13;
                break;
            case 3:
                minRooms = 14;
                maxRooms = 16;
                break;
            case 4:
                minRooms = 17;
                maxRooms = 20;
                break;
            case 5:
                minRooms = 21;
                maxRooms = 24;
                break;
        }
    }
}