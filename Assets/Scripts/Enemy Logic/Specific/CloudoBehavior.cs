using UnityEngine;

public class CloudoBehavior : MonoBehaviour, IKnockbackable, IEnemyStats {
    [SerializeField] private EnemyData baseEnemyData;
    public EnemyData uniqueEnemyData;
    public EnemyData GetStats() => uniqueEnemyData;

    [SerializeField] private EnemyMovement moveScript;
    [SerializeField] private EnemyHurt hurtScript;

    private float knockbackTimer = 0f;
    private float knockbackDuration = 0f;

    void Awake() {
        uniqueEnemyData = Instantiate(baseEnemyData);

        hurtScript.Initialize(uniqueEnemyData);
    }

    void FixedUpdate() {
        if (knockbackTimer > 0) {
            knockbackTimer -= Time.fixedDeltaTime;
        } 
        else {
            moveScript.MoveTowardsFromAnywhere(uniqueEnemyData.movementSpeed);
        }
    }

    public void StartKnockback() {
        knockbackTimer = knockbackDuration;
    }
}
