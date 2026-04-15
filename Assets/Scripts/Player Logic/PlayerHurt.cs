using UnityEngine;

public class PlayerHurt : MonoBehaviour {
    private GameObject player;
    private PlayerStats playerStats;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D myRigidBody;
    public HealthBar healthBar;

    private float invincibilityDuration;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;
    public float flashSpeed = 20f; // Higher is faster blinking

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = GetComponent<PlayerStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
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
        if (!isInvincible) {
            playerStats.HP -= damage;
            isInvincible = true;
            healthBar.UpdateHealthBar();

            if (playerStats.HP <= 0) {
                DeathHandler.Instance.Die(player);
            }
        }
    }

    private void SetAlpha(float alpha) {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}
