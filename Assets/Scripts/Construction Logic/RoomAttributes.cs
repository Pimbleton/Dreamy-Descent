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

    [Header("Clear Conditioning")]
    public bool cleared, hasCheckedInitialClear, previousState;

    [Header("Misc")]
    private SpriteRenderer cachedBounds;
    private SpriteResolver resolverN, resolverE, resolverS, resolverW;
    public string roomType;
    public Vector2Int gridPos;
    [SerializeField] private GameObject pitObject;

    void Awake() {
        doorNorth = transform.Find("N_Door")?.gameObject;
        doorSouth = transform.Find("S_Door")?.gameObject;
        doorEast = transform.Find("E_Door")?.gameObject;
        doorWest = transform.Find("W_Door")?.gameObject;

        if (doorNorth) resolverN = doorNorth.GetComponent<SpriteResolver>();
        if (doorSouth) resolverS = doorSouth.GetComponent<SpriteResolver>();
        if (doorEast) resolverE = doorEast.GetComponent<SpriteResolver>();
        if (doorWest) resolverW = doorWest.GetComponent<SpriteResolver>();

        playerCollidersContainer = transform.Find("PlayerCollider");  
        projectileCollidersContainer = transform.Find("ProjectileCollider");

        Transform boundsTransform = transform.Find("CameraBounds");
        if (boundsTransform != null) cachedBounds = boundsTransform.GetComponent<SpriteRenderer>();
    }

    void Start() {
        // Initialize camera to fit room dimensions
        if (gameObject.activeInHierarchy) InitializeCamera();

        // Do initial room scan
        CheckIfCleared();
    }

    // Keep watch for change in clear status of room if initially uncleared
    void Update() { if (!cleared && gameObject.activeInHierarchy) CheckIfCleared(); }

    public void InitializeCamera() {
        CameraScaling scaler = Camera.main.GetComponent<CameraScaling>();
        
        if (scaler != null && cachedBounds != null) scaler.UpdateBounds(cachedBounds);
    }

    public void SetupDoors(Dictionary<Vector2Int, GameObject> floorPlan) {  
        bool hasNorth = floorPlan.TryGetValue(gridPos + Vector2Int.up, out northRoom);
        doorNorth.SetActive(hasNorth);

        bool hasSouth = floorPlan.TryGetValue(gridPos + Vector2Int.down, out southRoom);
        doorSouth.SetActive(hasSouth);

        bool hasEast = floorPlan.TryGetValue(gridPos + Vector2Int.right, out eastRoom);
        doorEast.SetActive(hasEast);

        bool hasWest = floorPlan.TryGetValue(gridPos + Vector2Int.left, out westRoom);
        doorWest.SetActive(hasWest);

        ApplyCollider(hasNorth, hasSouth, hasEast, hasWest);
    }

    public void UpdateDoorStates() {
        string label = cleared ? "Open" : "Closed";

        SetDoorState(doorNorth, resolverN, label);
        SetDoorState(doorSouth, resolverS, label);
        SetDoorState(doorEast, resolverE, label);
        SetDoorState(doorWest, resolverW, label);
    }

    private void SetDoorState(GameObject doorObj, SpriteResolver resolver, string label) {
        if (doorObj == null) return;

        // Swap to respective sprite
        if (resolver != null) resolver.SetCategoryAndLabel("Doors", label);

        // Toggles box collider
        BoxCollider2D col = doorObj.GetComponent<BoxCollider2D>();
        if (cleared) Destroy(col);
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
    private void CheckIfCleared() {
        bool enemyFound = false;

        foreach (Transform child in transform) {
            if (child.CompareTag("Enemy")) {
                enemyFound = true;
                break;
            }
        }

        // Handles initial clear state
        if (!hasCheckedInitialClear) {
            hasCheckedInitialClear = true;
            
            // If cleared by default, skip heart pickup generation.
            if (!enemyFound) {
                cleared = true;
                previousState = true; 
                UpdateDoorStates();
                return;
            }
        }

        // Normal clear check
        if (!enemyFound) {
            cleared = true;

            if (!previousState) SpawnReward();

            if (roomType == "Boss") {
                BossObjectsBehavior.Instance.ShowObjects();
            }

            previousState = true;
            UpdateDoorStates();
        } else {
            cleared = false;
            previousState = false;
            UpdateDoorStates();
        }
    }

    private void SpawnReward() {
        int randInt = Random.Range(0, 10);

        // Spawn a health pickup if randInt is greater than 7 (8 or 9).
        if (randInt > 7) Instantiate(Resources.Load<GameObject>("Prefabs/Pickups/HeartPickup"), transform.position, Quaternion.identity, transform);
    }
}