using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public static EnemySpawner Instance;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject[] enemyPrefabs;

    void Awake() { Instance = this; }

    public void PopulateRoom(GameObject room) {
        // Determine number of enemies, ranging from 0 to 3 enemies in a room.
        int spawnCount = Random.Range(0, 4);

        // Actually spawn the enemies.
        for (int i = 0; i < spawnCount; i++) SpawnEnemy(room.transform);
    }

    private void SpawnEnemy(Transform roomParent) {
        // Pick a random enemy from the array.
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Generate a random position inside the room relative to the center of the room.
        Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0f);
        Vector3 spawnPos = roomParent.position + randomOffset;

        // Instantiate and set the room as the parent
        Instantiate(prefab, spawnPos, Quaternion.identity, roomParent);
    }
}