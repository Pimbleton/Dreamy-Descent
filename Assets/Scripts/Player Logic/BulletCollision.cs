using UnityEngine;

public class BulletCollision : MonoBehaviour {
    private float damage;
    private float knockback;

    void Start() {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().projectileDamage;
        knockback = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().projectileKnockback;
    }

    void OnTriggerEnter2D(Collider2D other) {
        string objectTag = other.tag;

        switch (objectTag) {
            case "Enemy":
                Destroy(gameObject);
                other.GetComponent<ChaserLogic>().EnemyTakeDamage(damage, knockback);
                break;
            case "Projectile Walls":
                Destroy(gameObject);
                break;
        }
    }
}
