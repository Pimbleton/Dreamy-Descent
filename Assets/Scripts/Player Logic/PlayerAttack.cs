using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour {
    private float projectileSpeed;
    private float projectileRange;
    private float projectileCooldown;
    private float nextProjectileAvailability = 0f;
    public GameObject PlayerProjectilePrefab;

    void Start() {
        projectileSpeed = GetComponent<PlayerStats>().projectileSpeed;
        projectileRange = GetComponent<PlayerStats>().projectileRange;
        projectileCooldown = GetComponent<PlayerStats>().projectileCooldown;
    }
    
    void Update() {
        if (Keyboard.current.upArrowKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed) {
                if (Time.time > nextProjectileAvailability) {
                    Shoot();
                    nextProjectileAvailability = Time.time + projectileCooldown;
                }
        }
    }

    void Shoot() {
        GameObject bullet = Instantiate(PlayerProjectilePrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null) {
            if (Keyboard.current.upArrowKey.isPressed) {
                rb.linearVelocity = Vector2.up * projectileSpeed;
            }
            else if (Keyboard.current.downArrowKey.isPressed) {
                rb.linearVelocity = Vector2.down * projectileSpeed;
            }
            else if (Keyboard.current.rightArrowKey.isPressed) {
                rb.linearVelocity = Vector2.right * projectileSpeed;
            }
            else if (Keyboard.current.leftArrowKey.isPressed) {
                rb.linearVelocity = Vector2.left * projectileSpeed;
            }
        }

        Destroy(bullet, projectileRange);
    }
}