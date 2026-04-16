using UnityEngine;

public class EnemyHurt : MonoBehaviour {
    [SerializeField] private Rigidbody2D enemyRigidBody;
    [SerializeField] private SwirloBehavior mainBehavior;
    private EnemyData enemyStats;

    public void Initialize(EnemyData clonedStats) { enemyStats = clonedStats; }

    public void TakeDamage(float amount, float knockbackForce, Vector3 sourcePosition) {
        // Reduce HP by player's projectile damage.
        enemyStats.HP -= amount;
        
        // Destroy enemy if HP is fully depleted.
        if (enemyStats.HP <= 0) {
            Destroy(gameObject);
            return;
        }

        // Start the knockback timer if applicable.
        mainBehavior.StartKnockback();

        // Handle Knockback Physics
        Vector2 knockbackDir = (transform.position - sourcePosition).normalized;
        enemyRigidBody.linearVelocity = Vector2.zero;
        enemyRigidBody.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
    }
}
