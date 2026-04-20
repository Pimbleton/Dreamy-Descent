using UnityEngine;

public class AngelaBehavior : MonoBehaviour, IKnockbackable {
    [SerializeField] private EnemyData baseEnemyData;
    private EnemyData uniqueEnemyData;

    [SerializeField] private EnemyMovement moveScript;
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
            moveScript.MoveRandomly(uniqueEnemyData.movementSpeed);
        }
    }

    public void StartKnockback() {
        knockbackTimer = knockbackDuration;
    }
}
