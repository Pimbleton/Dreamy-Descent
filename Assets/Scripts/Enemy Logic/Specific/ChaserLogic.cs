using UnityEngine;

public class ChaserLogic : MonoBehaviour
{
    public int damage = 1;
    public float speed = 1f;
    public float health = 10f;
    public Rigidbody2D myRigidBody;

    private float knockbackTimer = 0f;
    public float knockbackDuration = 0.2f;

    private Transform player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        if (player != null) {
            Vector2 direction = (player.position - transform.position).normalized;
            myRigidBody.linearVelocity = direction * speed;
        }
        else {
            myRigidBody.linearVelocity = Vector2.zero;
        }
    }

    public void EnemyTakeDamage(float damage, float knockback) {
        health -= damage;

        if (health <= 0) {
            GetComponent<DeathHandler>().Die();
        }

        knockbackTimer = knockbackDuration;
        Vector2 knockbackDirection = (transform.position - player.position).normalized;
        myRigidBody.linearVelocity = Vector2.zero;
        myRigidBody.AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);  
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.CompareTag("Player")) { other.gameObject.GetComponent<PlayerHurt>().PlayerTakeDamage(damage); }
    }

    void OnCollisionStay2D (Collision2D other) {
        if (other.gameObject.CompareTag("Player")) { 
            other.gameObject.GetComponent<PlayerHurt>().PlayerTakeDamage(damage); 
        }
    }
}
