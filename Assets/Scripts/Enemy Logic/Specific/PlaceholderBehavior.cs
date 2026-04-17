using UnityEngine;

public class PlaceholderBehavior : MonoBehaviour {
    [SerializeField] private EnemyData baseEnemyData;
    private EnemyData uniqueEnemyData;

    [SerializeField] private EnemyHurt hurtScript;
    
    private float knockbackTimer = 0f;
    private float knockbackDuration = 0f;

    void Awake() {
        uniqueEnemyData = Instantiate(baseEnemyData);

        hurtScript.Initialize(uniqueEnemyData);
    }

    public void OnHit(float damage, float knockback, Vector3 sourcePos) {
        hurtScript.TakeDamage(damage, knockback, sourcePos);
        knockbackTimer = knockbackDuration;
    }

    void OnCollisionStay2D (Collision2D other) {
        if (other.gameObject.CompareTag("Player")) other.gameObject.GetComponent<PlayerHurt>().PlayerTakeDamage(uniqueEnemyData.contactDamage);
    }
}
