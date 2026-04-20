using UnityEngine;

public class PlayerProjectileCollision : MonoBehaviour {
    private PlayerStats playerStats;

    void Awake() {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        string objectTag = other.tag;

        switch (objectTag) {
            case "Enemy":
                Destroy(gameObject);
                if (other.TryGetComponent(out EnemyHurt hurtScript)) {
                    hurtScript.EnemyTakeDamage(playerStats.projectileDamage, playerStats.projectileKnockback, transform.position);
                }
                break;
            case "Projectile Walls":
                Destroy(gameObject);
                break;
        }
    }
}
