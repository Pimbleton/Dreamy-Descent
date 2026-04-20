using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    [SerializeField] private EnemyData baseEnemyData;
    private EnemyData uniqueEnemyData;
    [SerializeField] private GameObject PlayerProjectilePrefab;

    private float nextProjectileAvailability = 0f;
    private GameObject player;

    void Awake() {
        uniqueEnemyData = Instantiate(baseEnemyData);
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate() {
        if (Time.time >= nextProjectileAvailability) {
            ShootAtPlayer(uniqueEnemyData.projectileSpeed);
            nextProjectileAvailability = Time.time + 2f;
        }
    }

    void ShootAtPlayer(float projectileSpeed) {
        GameObject bullet = Instantiate(PlayerProjectilePrefab, transform.position, transform.rotation, transform);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * projectileSpeed;

        Destroy(bullet, uniqueEnemyData.projectileRange);
    }
}
