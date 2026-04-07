using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorGenerator : MonoBehaviour {
    public int minRooms;
    public int maxRooms;
    
    public string currentScene;
    public string boss;
    Dictionary<Vector2Int, GameObject> spawnedRooms;

    void Start() {
        currentScene = SceneManager.GetActiveScene().name;
        spawnedRooms = new Dictionary<Vector2Int, GameObject>();

        switch (currentScene) {
            case "Floor1":
                minRooms = 6;
                maxRooms = 10;
                break;
            case "Floor2":
                minRooms = 8;
                maxRooms = 12;
                break;
            case "Floor3":
                minRooms = 10;
                maxRooms = 15;
                break;
            case "Floor4":
                minRooms = 12;
                maxRooms = 18;
                break;
            case "Floor5":
                minRooms = 15;
                maxRooms = 20;
                break;
        }
        GenerateFloor();
    }

    void GenerateFloor() {
        int roomCount = Random.Range(minRooms, maxRooms + 1);
        
        // Start by placing the "Start" room at location (0,0)
        PlaceRoom(Vector2Int.zero, "Start");

        // 2. Growth Loop
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
        if (!AssignSpecialRoom("Boss") || !AssignSpecialRoom("Item")) {
            ResetFloor();
            return;
        }
        
        // Setup doors for all rooms after all have been placed.
        foreach (var room in spawnedRooms) {
            room.Value.GetComponent<RoomAttributes>().SetupDoors(spawnedRooms);

            //if (!room.Value.GetComponent<RoomAttributes>().name.Contains("Start")) {
            //    room.Value.SetActive(false);
            //}
        }
    }

    void PlaceRoom(Vector2Int pos, string type) {
        GameObject roomObj = null;

        // Sets a position based on grid coordinates, taking into account the rooms' dimensions.
        Vector3 spawnPos = new Vector3(pos.x * 17.8f, pos.y * 10f, 0f);
        
        // Loads the room prefab based on the current scene and type, then instantiates it at the calculated position.
        switch (type) {
            case "Start":
                roomObj = Instantiate(Resources.Load<GameObject>($"Prefabs/{currentScene}/Rooms/Start_Room"), spawnPos, Quaternion.identity);
                break;
            case "Basic":
                roomObj = Instantiate(Resources.Load<GameObject>($"Prefabs/{currentScene}/Rooms/Basic1"), spawnPos, Quaternion.identity);
                break;
        }
        
        
        roomObj.name = $"Room_{type}_{pos.x}_{pos.y}";
        
        RoomAttributes attr = roomObj.GetComponent<RoomAttributes>();
        attr.gridPos = pos;
        attr.roomType = type;

        spawnedRooms.Add(pos, roomObj);
    }

    Vector2Int GetRandomPlacedRoomPos() {
        // Convert dictionary keys to a list and pick one
        List<Vector2Int> keys = new List<Vector2Int>(spawnedRooms.Keys);
        return keys[Random.Range(0, keys.Count)];
    }

bool AssignSpecialRoom(string type) {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var entry in spawnedRooms) {
            Vector2Int pos = entry.Key;
            if (pos == Vector2Int.zero) continue;

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

        // 1. Get reference to the old room and its position
        GameObject oldRoom = spawnedRooms[targetPos];
        Vector3 spawnPos = oldRoom.transform.position;

        // 2. Prepare the new room variable
        GameObject newRoom = null;

        switch (type) {
            case "Boss":
                string bossName = PickBoss.pickBoss(currentScene);
                newRoom = Instantiate(Resources.Load<GameObject>($"Prefabs/{currentScene}/Rooms/{bossName}_Room"), spawnPos, Quaternion.identity);
                break;
            case "Item":
                newRoom = Instantiate(Resources.Load<GameObject>($"Prefabs/{currentScene}/Rooms/Item_Room"), spawnPos, Quaternion.identity);
                break;
        }

        if (newRoom != null) {
            newRoom.name = $"Room_{type}_{targetPos.x}_{targetPos.y}";
            
            // Update the dictionary to point to the new object
            spawnedRooms[targetPos] = newRoom;

            // Update the GridPos in the new room's RoomAttributes
            newRoom.GetComponent<RoomAttributes>().gridPos = targetPos;

            // Destroy the old "Basic" room GameObject
            Destroy(oldRoom);
        }

        return true;
    }

    Vector2Int GetRandomDirection() {
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        return dirs[Random.Range(0, 4)];
    }

    void ResetFloor () {
        foreach (var room in spawnedRooms) {
            Destroy(room.Value);
        }
        spawnedRooms.Clear();
        GenerateFloor();
    }
}