using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    [Header("Item Prefabs")]
    [SerializeField] private GameObject[] itemPrefabs;

   void Awake() {
        Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], transform.position + new Vector3(0f, .6f, 0f), Quaternion.identity, transform);
    }
}
