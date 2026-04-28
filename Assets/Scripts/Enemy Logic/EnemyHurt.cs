using UnityEngine;

public class EnemyHurt : MonoBehaviour {
    [SerializeField] private Rigidbody2D enemyRigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private AudioClip enemyHitSound;
    [SerializeField] private AudioSource audioSource;

    private IKnockbackable mainBehavior;
    private EnemyData enemyStats;
    private Color originalColor;

    public void Initialize(EnemyData clonedStats) { 
        enemyStats = clonedStats; 
        mainBehavior = GetComponent<IKnockbackable>();
        originalColor = spriteRenderer.color;
    }

    public void EnemyTakeDamage(float amount, float knockbackForce, Vector3 sourcePosition) {
        // Reduce HP by player's projectile damage.
        enemyStats.HP -= amount;

        // Generate hurt sound with a random pitch for variety.
        if (enemyHitSound != null && audioSource != null) {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(enemyHitSound, .75f);
        }
        
        // Destroy enemy if HP is fully depleted.
        if (enemyStats.HP <= 0) {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(FlashRoutine());

        // Start the knockback timer if applicable.
        if (mainBehavior != null) {
            mainBehavior.StartKnockback();
            Vector2 knockbackDir = (transform.position - sourcePosition).normalized;
            enemyRigidBody.linearVelocity = Vector2.zero;
            enemyRigidBody.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private System.Collections.IEnumerator FlashRoutine() {
        // Get the current color
        Color flashColor = originalColor;
        
        // Set alpha to 0.5 (Semi-transparent)
        flashColor.a = .5f; 
        spriteRenderer.color = flashColor;

        // Wait for a split second.
        yield return new WaitForSeconds(0.1f);

        // Reset to original color at full opacity.
        spriteRenderer.color = originalColor;
    }
}