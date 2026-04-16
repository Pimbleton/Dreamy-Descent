using UnityEngine;

public class SwirloBehavior : MonoBehaviour {
    [SerializeField] private EnemyData baseEnemyData;
    private EnemyData uniqueEnemyData;

    [SerializeField] private MoveTowardPlayer moveScript;
    [SerializeField] private EnemyHurt hurtScript;
    
    private float knockbackTimer = 0f;
    private float knockbackDuration = 0.1f;

    void Awake() {
        uniqueEnemyData = Instantiate(baseEnemyData);

        hurtScript.Initialize(uniqueEnemyData);
    }

    void FixedUpdate() {
        if (knockbackTimer > 0) {
            knockbackTimer -= Time.fixedDeltaTime;
        } 
        else {
            moveScript.Move(uniqueEnemyData.movementSpeed);
        }
    }

    public void OnHit(float damage, float knockback, Vector3 sourcePos) {
        hurtScript.TakeDamage(damage, knockback, sourcePos);
        knockbackTimer = knockbackDuration;
    }

    public void StartKnockback() {
        if (knockbackDuration == 0) return;

        knockbackTimer = knockbackDuration;
    }

    void OnCollisionStay2D (Collision2D other) {
        if (other.gameObject.CompareTag("Player")) { other.gameObject.GetComponent<PlayerHurt>().PlayerTakeDamage(uniqueEnemyData.contactDamage); }
    }
}