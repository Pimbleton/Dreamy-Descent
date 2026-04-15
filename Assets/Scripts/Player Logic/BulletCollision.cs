using UnityEngine;

public class BulletCollision : MonoBehaviour {
    private PlayerStats playerStats;

    void Start() {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        string objectTag = other.tag;

        switch (objectTag) {
            case "Enemy":
                Destroy(gameObject);
                other.GetComponent<ChaserBehavior>().EnemyTakeDamage(playerStats.projectileDamage, playerStats.projectileKnockback);
                break;
            case "Projectile Walls":
                Destroy(gameObject);
                break;
        }
    }
}
