using UnityEngine;

public class SwirloBehavior : MonoBehaviour {
    [SerializeField] private EnemyData baseEnemyData;
    private EnemyData uniqueEnemyData;
    private Rigidbody2D myRigidBody;

    private float knockbackTimer = 0f;
    private float knockbackDuration = 0.2f;

    private Transform playerTransform;

    void Awake() {
        uniqueEnemyData = Instantiate(baseEnemyData);
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) {
            playerTransform = playerObj.transform;
        }
    }

    void FixedUpdate() {
        if (knockbackTimer > 0) {
            knockbackTimer -= Time.fixedDeltaTime;
        } 
        else {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer() {
        if (playerTransform != null) {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            myRigidBody.linearVelocity = direction * uniqueEnemyData.movementSpeed;
        }
        else {
            myRigidBody.linearVelocity = Vector2.zero;
        }
    }

    public void EnemyTakeDamage(float damage, float knockback) {
        uniqueEnemyData.HP -= damage;

        if (uniqueEnemyData.HP <= 0) {
            Destroy(gameObject);
            return;
        }

        knockbackTimer = knockbackDuration;
        Vector2 knockbackDirection = (transform.position - playerTransform.position).normalized;
        myRigidBody.linearVelocity = Vector2.zero;
        myRigidBody.AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);  
    }

    void OnCollisionStay2D (Collision2D other) {
        if (other.gameObject.CompareTag("Player")) { 
            other.gameObject.GetComponent<PlayerHurt>().PlayerTakeDamage(uniqueEnemyData.contactDamage); 
        }
    }
}