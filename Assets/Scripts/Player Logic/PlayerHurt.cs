using UnityEngine;

public class PlayerHurt : MonoBehaviour {
    private PlayerStats stats;
    private SpriteRenderer spriteRenderer;

    private float invincibilityDuration;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;
    public float flashSpeed = 20f; // Higher is faster blinking

    void Start() {
        stats = GetComponent<PlayerStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        invincibilityDuration = stats.invincibilityDuration;
    }

    void Update() {
        if (isInvincible) {
            invincibilityTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.2f, 1.0f, Mathf.Abs(Mathf.Sin(Time.time * flashSpeed)));
            SetAlpha(alpha);

            if (invincibilityTimer >= invincibilityDuration) {
                SetAlpha(1.0f);
                isInvincible = false;
                invincibilityTimer = 0f;
            }
        }
    }

    public void PlayerTakeDamage(int damage) {
        if (!isInvincible) {
            stats.HP -= damage;
            isInvincible = true;

            if (stats.HP <= 0) {
                Die();
            }
        }
    }

    private void SetAlpha(float alpha) {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
    
    private void Die() {
        Debug.Log("Player has died.");
        Destroy(gameObject);
    }
}
