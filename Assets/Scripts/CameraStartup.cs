using UnityEngine;

public class CameraStartup : MonoBehaviour {
    private GameObject room;

    void Start() {
        room = GameObject.FindGameObjectWithTag("First Room");
        Camera.main.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, Camera.main.transform.position.z);
    } 
}
