using System.Collections.Generic;
using UnityEngine;

public class RoomAttributes : MonoBehaviour {
    [Header("Doors")]
    public GameObject doorNorth;
    public GameObject doorSouth;
    public GameObject doorEast;
    public GameObject doorWest;

    [Header("Neighbors")]
    public GameObject northRoom;
    public GameObject southRoom;
    public GameObject eastRoom;
    public GameObject westRoom;

    [Header("Colliders")]
    private Transform playerCollidersContainer;
    private Transform projectileCollidersContainer;

    [Header("Misc")]
    private SpriteRenderer cachedBounds;
    public string roomType;

    public Vector2Int gridPos;

    void Awake() {
        doorNorth = transform.Find("N_Door").gameObject;
        doorSouth = transform.Find("S_Door").gameObject;
        doorEast = transform.Find("E_Door").gameObject;
        doorWest = transform.Find("W_Door").gameObject;
        
        playerCollidersContainer = transform.Find("PlayerCollider");  
        // projectileCollidersContainer = transform.Find("ProjectileCollider");

        Transform boundsTransform = transform.Find("CameraBounds");
        if (boundsTransform != null) {
            cachedBounds = boundsTransform.GetComponent<SpriteRenderer>();
        }
    }
    
    void Start() {
        if (gameObject.activeInHierarchy) {
                InitializeCamera();
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

    void ApplyCollider(bool n, bool s, bool e, bool w) {
        // Build the name string with respective active door directions
        string availableDoors = "";
        if (n) availableDoors += "N";
        if (e) availableDoors += "E";
        if (s) availableDoors += "S";
        if (w) availableDoors += "W";

        // Call SpawnCollider that gives the respective prefab name along with the parent transform to maintain hierarchy
        SpawnCollider($"{availableDoors}_PlayerWall", playerCollidersContainer);
        //SpawnCollider($"Prefabs/ProjectileWalls/{availableDoors}_ProjectileWalls", projectileCollidersContainer);
    }

    void SpawnCollider(string prefabName, Transform parent) {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/PlayerWalls/{prefabName}");
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