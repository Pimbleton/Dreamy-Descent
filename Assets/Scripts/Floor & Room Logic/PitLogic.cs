using UnityEngine;

public class PitLogic : MonoBehaviour {
    [Header("Player Reference")]
    GameObject player;

    void Start() {
        gameObject.SetActive(false); // Start with the pit inactive
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                FloorGenerator.Instance.currentFloor += 1; // Move to the next floor
                player.transform.position = Vector3.zero; // Reset player position to the center of the new floor
                Camera.main.transform.position = new Vector3(0f, 0f, Camera.main.transform.position.z);
                FloorGenerator.Instance.ResetFloor();
            }
        }
    }
