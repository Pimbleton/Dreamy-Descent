using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour {
    [SerializeField] private Rigidbody2D enemyRigidBody;
    private GameObject player;
    private Transform playerTransform;

    void Awake() {
        player = GameObject.FindWithTag("Player"); 
        playerTransform = player.transform; 
    }

    public void Move(float speed) {
        if (playerTransform != null) {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            enemyRigidBody.linearVelocity = direction * speed;
        }
    }

    public void Stop() {
        enemyRigidBody.linearVelocity = Vector2.zero;
    }
}
