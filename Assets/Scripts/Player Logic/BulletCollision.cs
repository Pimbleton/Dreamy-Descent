using UnityEngine;

public class BulletCollision : MonoBehaviour {
    private PlayerStats playerStats;

    void Awake() { playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>(); }

    void OnTriggerEnter2D(Collider2D other) {
        string objectTag = other.tag;

        switch (objectTag) {
            case "Enemy":
                Destroy(gameObject);
                if (other.TryGetComponent(out EnemyHurt hurtScript)) { hurtScript.TakeDamage(playerStats.projectileDamage, playerStats.projectileKnockback, transform.position); }
                break;
            case "Projectile Walls":
                Destroy(gameObject);
                break;
        }
    }
}
