using UnityEngine;

public class PlayerHurt : MonoBehaviour {
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private HealthBar healthBar;

    private float invincibilityDuration;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;
    private float flashSpeed = 20f;

    public static PlayerHurt Instance;

    void Awake() { 
        invincibilityDuration = playerStats.invincibilityDuration;
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
        if (!isInvincible && damage > 0) {
            playerStats.HP -= damage;
            isInvincible = true;
            healthBar.UpdateHealthBar();

            if (playerStats.HP <= 0) {
                DeathHandler.Instance.Die(gameObject);
            }
        }
    }

    private void SetAlpha(float alpha) {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}
