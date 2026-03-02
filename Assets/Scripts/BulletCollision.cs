using UnityEngine;

public class BulletCollision : MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other) {

        string objectTag = other.tag;

        switch (objectTag) {
            case "Enemy":
                Destroy(gameObject);
                break;
            case "Projectile Walls":
                Destroy(gameObject);
                break;
        }
    }
}
