using UnityEngine;

public class PitLogic : MonoBehaviour {
    private GameObject player;

    void Awake() { player = GameObject.FindGameObjectWithTag("Player"); }

    void Start() { gameObject.SetActive(false); }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            // Pit entered, now incrementing to next floor.
            FloorGenerator.Instance.currentFloor += 1;

            // Sends player to center of "new" floor and centers camera there.
            player.transform.position = Vector3.zero;
            Camera.main.transform.position = new Vector3(0f, 0f, Camera.main.transform.position.z);

            // Generate the new floor.
            FloorGenerator.Instance.ResetFloor();
        }
    }
}
