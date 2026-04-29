using System;
using UnityEngine;

public class AquariusBehavior : MonoBehaviour, IEnemyStats {
    [Header("Scripts")]
    [SerializeField] private EnemyData baseEnemyData;
    [SerializeField] private EnemyData uniqueEnemyData;
    public EnemyData GetStats() => uniqueEnemyData;
    [SerializeField] private EnemyHurt hurtScript;
    
    [Header("Bounds")]
    private float roomWidth = 18f;
    private float leftBound;
    private float rightBound;

    [Header("Misc")]
    private int direction = 1; // 1 = Right, -1 = Left
    [SerializeField] private Rigidbody2D aquariusRigidbody;
    private GameObject player;

    [Header("Attack-Related")]
    [SerializeField] private GameObject rainBoltPrefab;
    [SerializeField] private GameObject bubblePrefab;
    private float cooldownEndTime = 0f;
    private float nextRainBolt = 0f;
    private float nextUniqueAttackChance = 0f;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        uniqueEnemyData = Instantiate(baseEnemyData);
        hurtScript.Initialize(uniqueEnemyData);

        // Set center of room horizontally.
        float centerX = transform.parent.position.x;
        transform.position = new Vector3(centerX, transform.parent.position.y + 4.3f, transform.position.z);
        
        // Subtract a small margin (e.g., 2 units) so the boss doesn't hit the walls
        leftBound = centerX - (roomWidth / 2) + 2f;
        rightBound = centerX + (roomWidth / 2) - 2f;
    }

    void FixedUpdate() {
        if (cooldownEndTime < Time.time) {
            if (Math.Abs(gameObject.transform.position.y - player.transform.position.y) < 1.5f) {
                Vector2 movement = new Vector2(direction * (uniqueEnemyData.movementSpeed * 3), aquariusRigidbody.linearVelocity.y);
                aquariusRigidbody.linearVelocity = movement;
            } else {
                Vector2 movement = new Vector2(direction * uniqueEnemyData.movementSpeed, aquariusRigidbody.linearVelocity.y);
                aquariusRigidbody.linearVelocity = movement;
            }

            // .Check if we hit a bound and need to flip
            if (transform.position.x >= rightBound && direction == 1) {
                direction = -1;
            } 
            else if (transform.position.x <= leftBound && direction == -1) {
                direction = 1;
            }

            if (Time.time >= nextRainBolt) {
                rainBoltAttack();
                nextRainBolt = Time.time + UnityEngine.Random.Range(.25f, 1f);
            }

            if (Time.time >= nextUniqueAttackChance) {
                int attackChance = UnityEngine.Random.Range(0, 100);

                if (attackChance < 50) {
                    aquariusRigidbody.linearVelocity = new Vector2(0, 0);
                    cooldownEndTime = Time.time + 1f;
                    bubbleBurst();
                }

                nextUniqueAttackChance = Time.time + 3f;
            }
        }
    }

    void OnCollisionStay2D (Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerHurt>().PlayerTakeDamage(uniqueEnemyData.contactDamage);
        }
    }

    void rainBoltAttack() {
        GameObject bullet1 = Instantiate(rainBoltPrefab, transform.position, Quaternion.Euler(0, 0, -27.5f), transform);
        Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();
        
        GameObject bullet2 = Instantiate(rainBoltPrefab, transform.position, Quaternion.Euler(0, 0, 0), transform);
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();

        GameObject bullet3 = Instantiate(rainBoltPrefab, transform.position, Quaternion.Euler(0, 0, 27.5f), transform);
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();

        float randAngle = UnityEngine.Random.Range(.25f, .75f);
        
        Vector2 direction1 = new Vector2(-randAngle, -1);
        rb.linearVelocity = direction1 * uniqueEnemyData.projectileSpeed;

        Vector2 direction2 = new Vector2(0, -1);
        rb2.linearVelocity = direction2 * uniqueEnemyData.projectileSpeed;

        Vector2 direction3 = new Vector2(randAngle, -1);
        rb3.linearVelocity = direction3 * uniqueEnemyData.projectileSpeed;

        Destroy(bullet1, uniqueEnemyData.projectileRange);
        Destroy(bullet2, uniqueEnemyData.projectileRange);
        Destroy(bullet3, uniqueEnemyData.projectileRange);
    }

    void bubbleBurst() {
        if (bubblePrefab != null) {
            GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity, transform);
            Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

            Vector2 direction = player.transform.position - transform.position;
            rb.linearVelocity = direction.normalized * uniqueEnemyData.projectileSpeed;
        }
    }
}
