using System.Collections.Generic;
using UnityEngine;

public class RoomAttributes : MonoBehaviour {
    [Header("Door GameObjects")]
    public GameObject doorNorth;
    public GameObject doorSouth;
    public GameObject doorEast;
    public GameObject doorWest;

    [HideInInspector] public Vector2Int gridPos;
    
    public GameObject northRoom;
    public GameObject southRoom;
    public GameObject eastRoom;
    public GameObject westRoom;

    public string roomType;

    void Awake() {
        doorNorth = transform.Find("N_Door").gameObject;
        doorSouth = transform.Find("S_Door").gameObject;
        doorEast = transform.Find("E_Door").gameObject;
        doorWest = transform.Find("W_Door").gameObject;
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

        //ApplyCollider(hasNorth, hasSouth, hasEast, hasWest);
    }

/*
    void ApplyCollider(bool n, bool s, bool e, bool w) {
        GameObject playerColliders = transform.Find("PlayerCollider").gameObject;
        GameObject projectileColliders = transform.Find("ProjectileCollider").gameObject;

        string availableDoors = "";
        string playerWallName;
        string projectileWallName;

        if (n) {
            availableDoors += "N";  
        } 
        if (e) {
            availableDoors += "E";  
        } 
        if (s) {
            availableDoors += "S";
        }
        if (w) {
            availableDoors += "W";
        }

        playerWallName = $"{availableDoors}_PlayerWalls";
        projectileWallName = $"{availableDoors}_ProjectileWalls";

        GameObject playerWallPrefab = Resources.Load<GameObject>($"Prefabs/{playerWallName}");
        GameObject projectileWallPrefab = Resources.Load<GameObject>($"Prefabs/{projectileWallName}");

        if (playerWallPrefab != null) {
            playerColliders = Instantiate(playerWallPrefab, playerColliders.transform.position, Quaternion.identity, playerColliders.transform);
        }
        if (projectileWallPrefab != null) {
            projectileColliders = Instantiate(projectileWallPrefab, projectileColliders.transform.position, Quaternion.identity, projectileColliders.transform);
        }
    }
*/
}