using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] private Rigidbody2D enemyRigidBody;
    private GameObject player;
    private Transform playerTransform;

    // Random Movement Smoothing Variables
    private Vector2 currentRandomDir;
    private float wanderTimer;
    private float wanderChangeInterval = 1f;

    void Awake() {
        player = GameObject.FindWithTag("Player"); 
        playerTransform = player.transform;
        currentRandomDir = Random.insideUnitCircle.normalized;
    }

    public void MoveTowardsFromAnywhere(float speed) {
        if (playerTransform != null) {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            enemyRigidBody.linearVelocity = direction * speed;
        }
    }

    public void MoveTowardsInRange(float speed) {
        if (playerTransform != null) {
            float distanceToPlayer = (transform.position - playerTransform.position).sqrMagnitude;

            if (distanceToPlayer < 4f) {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                enemyRigidBody.linearVelocity = direction * speed;
            } else {
                MoveRandomly(speed);
            }
        }
    }

    public void MoveAwayFromAnywhere(float speed) {
        if (playerTransform != null) {
            Vector2 direction = (transform.position - playerTransform.position).normalized;
            enemyRigidBody.linearVelocity = direction * speed;
        }
    }
    
    public void MoveAwayInRange(float speed) {
        if (playerTransform != null) {
            float distanceToPlayer = (transform.position - playerTransform.position).sqrMagnitude;

            if (distanceToPlayer < 4f) {
                Vector2 direction = (transform.position - playerTransform.position).normalized;
                enemyRigidBody.linearVelocity = direction * speed;
            } else {
                MoveRandomly(speed);
            }
        }
    }

    public void MoveRandomly(float speed) {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0) {
            currentRandomDir = Random.insideUnitCircle.normalized;
            wanderTimer = wanderChangeInterval;
        }

        enemyRigidBody.linearVelocity = currentRandomDir * speed;
    }

    public void StopMovement() {
        enemyRigidBody.linearVelocity = Vector2.zero;
        wanderTimer = 0;
    }
}
