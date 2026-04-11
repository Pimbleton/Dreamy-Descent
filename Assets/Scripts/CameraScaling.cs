using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    private Camera cam;
    private SpriteRenderer currentBounds;

    void Awake() {
        cam = GetComponent<Camera>();
    }

    // Ensures updating in real-time when window is rescaled.
    void Update() {
        if (currentBounds != null) {
            ScaleCamera();
        }
    }
    
    // This is called by RoomAttributes when a room is spawned
    public void UpdateBounds(SpriteRenderer newBounds) {
        currentBounds = newBounds;
        ScaleCamera();
    }



    public void ScaleCamera() {
        if (currentBounds == null || cam == null) return;

        // Get the size of the room bounds in world units from the SpriteRenderer.
        float roomWidth = currentBounds.bounds.size.x;
        float roomHeight = currentBounds.bounds.size.y;

        // Get the current screen aspect ratio.
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float roomAspect = roomWidth / roomHeight;

        if (screenAspect >= roomAspect) {
            // If window is wider than the room, fit based on height.
            cam.orthographicSize = roomHeight / 2;
        } else {
            // If window is narrower than the room, fit based on width.
            float differenceInSize = roomAspect / screenAspect;
            cam.orthographicSize = (roomHeight / 2) * differenceInSize;
        }

        // Center the camera on the room
        transform.position = new Vector3(currentBounds.bounds.center.x, currentBounds.bounds.center.y, -10);
    }
}