using UnityEngine;

public class EnemyProjectileCollision : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        string objectTag = other.tag;

        switch (objectTag) {
            case "Player":
                Destroy(gameObject);

                if (other.TryGetComponent(out PlayerHurt hurtScript)) {
                    IEnemyStats enemyStats = transform.parent.GetComponent<IEnemyStats>();
                    hurtScript.PlayerTakeDamage(enemyStats.GetStats().projectileDamage);
                }
                break;
            case "Projectile Walls":
                Destroy(gameObject);
                break;
        }
    }
}
