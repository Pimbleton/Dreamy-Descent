using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour {
    private PlayerStats playerStats;
    private float nextProjectileAvailability = 0f;
    private GameObject PlayerProjectilePrefab;
    
    void Awake() { playerStats = GetComponent<PlayerStats>(); }
    
    void Update() {
        if (Keyboard.current.upArrowKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed) {
                if (Time.time > nextProjectileAvailability) {
                    Shoot();
                    nextProjectileAvailability = Time.time + playerStats.projectileCooldown;
                }
        }
    }

    void Shoot() {
        GameObject bullet = Instantiate(PlayerProjectilePrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null) {
            if (Keyboard.current.upArrowKey.isPressed) {
                rb.linearVelocity = Vector2.up * playerStats.projectileSpeed;
            } else if (Keyboard.current.downArrowKey.isPressed) {
                rb.linearVelocity = Vector2.down * playerStats.projectileSpeed;
            } else if (Keyboard.current.rightArrowKey.isPressed) {
                rb.linearVelocity = Vector2.right * playerStats.projectileSpeed;
            } else if (Keyboard.current.leftArrowKey.isPressed) {
                rb.linearVelocity = Vector2.left * playerStats.projectileSpeed;
            }
        }

        Destroy(bullet, playerStats.projectileRange);
    }
}