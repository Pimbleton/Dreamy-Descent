using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class RoomAttributes : MonoBehaviour {
    [Header("Doors")]
    public GameObject doorNorth, doorEast, doorSouth, doorWest;

    [Header("Neighbors")]
    public GameObject northRoom, eastRoom, southRoom, westRoom;

    [Header("Colliders")]
    private Transform playerCollidersContainer, projectileCollidersContainer;

    [Header("Misc")]
    public bool cleared;
    private SpriteRenderer cachedBounds;
    private SpriteResolver resolverN, resolverE, resolverS, resolverW;
    public string roomType;

    public Vector2Int gridPos;

    void Awake() {
        doorNorth = transform.Find("N_Door")?.gameObject;
        doorSouth = transform.Find("S_Door")?.gameObject;
        doorEast = transform.Find("E_Door")?.gameObject;
        doorWest = transform.Find("W_Door")?.gameObject;

        if (doorNorth) {
            resolverN = doorNorth.GetComponent<SpriteResolver>();
        }
        
        if (doorSouth) {
            resolverS = doorSouth.GetComponent<SpriteResolver>();
        }
        
        if (doorEast) {
            resolverE = doorEast.GetComponent<SpriteResolver>();
        }
        
        if (doorWest) {
            resolverW = doorWest.GetComponent<SpriteResolver>();
        }

        playerCollidersContainer = transform.Find("PlayerCollider");  
        projectileCollidersContainer = transform.Find("ProjectileCollider");

        Transform boundsTransform = transform.Find("CameraBounds");
        if (boundsTransform != null) {
            cachedBounds = boundsTransform.GetComponent<SpriteRenderer>();
        }
    }

    void Start() {
        if (gameObject.activeInHierarchy) {
                InitializeCamera();
        }

        CheckIfCleared();
    }

    void Update() {
        if(!cleared && gameObject.activeInHierarchy) {
            CheckIfCleared();
        }
    }

    public void SetupDoors(Dictionary<Vector2Int, GameObject> floorPlan) {  
        // North
        bool hasNorth = floorPlan.TryGetValue(gridPos + Vector2Int.up, out northRoom);
        doorNorth.SetActive(hasNorth);

        // South
        bool hasSouth = floorPlan.TryGetValue(gridPos + Vector2Int.down, out southRoom);
        doorSouth.SetActive(hasSouth);

        // East
        bool hasEast = floorPlan.TryGetValue(gridPos + Vector2Int.right, out eastRoom);
        doorEast.SetActive(hasEast);

        // West
        bool hasWest = floorPlan.TryGetValue(gridPos + Vector2Int.left, out westRoom);
        doorWest.SetActive(hasWest);

        ApplyCollider(hasNorth, hasSouth, hasEast, hasWest);
    }

    private void CheckIfCleared() {
        bool enemyFound = false;

        // Loop through the children of this room only
        foreach (Transform child in transform) {
            if (child.CompareTag("Enemy")) {
                enemyFound = true;
                break; // We found one, no need to keep looking
            }
        }

        if (!enemyFound) {
            cleared = true;
            UpdateDoorStates();
        } else {
            cleared = false;
            UpdateDoorStates();
        }
    }

    public void UpdateDoorStates() {
        string label = cleared ? "Open" : "Closed";

        SetDoorState(doorNorth, resolverN, label);
        SetDoorState(doorSouth, resolverS, label);
        SetDoorState(doorEast, resolverE, label);
        SetDoorState(doorWest, resolverW, label);
    }

    private void SetDoorState(GameObject doorObj, SpriteResolver resolver, string label) {
        if (doorObj == null) {
            return;
        }

        // Swap Sprite
        if (resolver != null) {
            resolver.SetCategoryAndLabel("Doors", label);
        }

        // Toggles box collider
        BoxCollider2D col = doorObj.GetComponent<BoxCollider2D>();
        if (cleared) {
            Destroy(col);
        }
    }

    void ApplyCollider(bool n, bool s, bool e, bool w) {
        // Build the name string with respective active door directions
        string availableDoors = "";
        if (n) availableDoors += "N";
        if (e) availableDoors += "E";
        if (s) availableDoors += "S";
        if (w) availableDoors += "W";

        // Call respective SpawnCollider methods that give the respective prefab name along with the parent transform to maintain hierarchy
        SpawnPlayerCollider($"{availableDoors}_PlayerWall", playerCollidersContainer);
        SpawnProjectileCollider($"{availableDoors}_ProjectileWall", projectileCollidersContainer);
    }

    void SpawnPlayerCollider(string prefabName, Transform parent) {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/PlayerWalls/{prefabName}");
        if (prefab != null) {
            // Instantiate without overwriting the parent reference
            Instantiate(prefab, parent.position, Quaternion.identity, parent);
        } else {
            Debug.LogWarning($"Prefab not found: {prefabName}");
        }
    }

    void SpawnProjectileCollider(string prefabName, Transform parent) {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/ProjectileWalls/{prefabName}");
        if (prefab != null) {
            // Instantiate without overwriting the parent reference
            Instantiate(prefab, parent.position, Quaternion.identity, parent);
        } else {
            Debug.LogWarning($"Prefab not found: {prefabName}");
        }
    }

    public void InitializeCamera() {
        CameraScaling scaler = Camera.main.GetComponent<CameraScaling>();
        
        if (scaler != null && cachedBounds != null) {
            scaler.UpdateBounds(cachedBounds);
        }
    }
}