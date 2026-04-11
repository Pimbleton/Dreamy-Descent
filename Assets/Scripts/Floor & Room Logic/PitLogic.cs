using UnityEngine;

public class PitLogic : MonoBehaviour {
    GameObject player;

void Start() {
    player = GameObject.FindGameObjectWithTag("Player");
}

void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            FloorGenerator.Instance.currentFloor += 1; // Move to the next floor
            player.transform.position = Vector3.zero; // Reset player position to the center of the new floor
            Camera.main.transform.position = new Vector3(0f, 0f, Camera.main.transform.position.z); // TODO: doesn't work, fix it.
            FloorGenerator.Instance.ResetFloor();
            
            // Optionally, you can add a transition effect here before the new floor loads.

            
            
        }
    }
}
