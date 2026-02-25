using UnityEngine;

public class BulletCollision : MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other) {

        string objectTag = other.tag;
        Debug.Log("Bullet collided with: " + objectTag);

        switch (objectTag) {
            case "Enemy":
                Destroy(gameObject);
                break;
            case "Walls":
                Destroy(gameObject);
                break;
        }
    }
}
