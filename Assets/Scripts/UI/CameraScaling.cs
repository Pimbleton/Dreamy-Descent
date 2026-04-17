using UnityEngine;

public class CameraScaling : MonoBehaviour {
    private Camera cam;
    private SpriteRenderer currentBounds;

    void Awake() { cam = GetComponent<Camera>(); }

    // Ensures window is scaled in real-time.
    void Update() { if (currentBounds != null) ScaleCamera(); }
    
    // Called by RoomAttributes when a room is spawned.
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

        // If window is wider than the room, fit based on height.
        // Else, fit based on width.
        if (screenAspect >= roomAspect) {
            cam.orthographicSize = roomHeight / 2;
        } else {
            float differenceInSize = roomAspect / screenAspect;
            cam.orthographicSize = (roomHeight / 2) * differenceInSize;
        }

        // Center the camera on the room
        transform.position = new Vector3(currentBounds.bounds.center.x, currentBounds.bounds.center.y, -10);
    }
}