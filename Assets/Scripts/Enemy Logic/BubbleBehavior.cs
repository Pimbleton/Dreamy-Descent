using UnityEngine;

public class BubbleBehavior : MonoBehaviour {
    public GameObject miniBubblePrefab;
    public float lifetime = 1f;
    public float spreadForce = 5f;
    
    private float timer;
    private bool hasBurst = false;

    void Start() {
        timer = lifetime;
    }

    void Update() {
        // Manual countdown is more reliable than Invoke
        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0 && !hasBurst) {
                Burst();
            }
        }
    }

    void Burst() {
        if (hasBurst) return;
        hasBurst = true;

        int miniBubbleCount = 6;
        float angleStep = 360f / miniBubbleCount;

        for (int i = 0; i < miniBubbleCount; i++) {
            float angle = i * angleStep;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(x, y).normalized;

            // Spawn without parent to ensure independence
            GameObject mini = Instantiate(miniBubblePrefab, transform.position, Quaternion.identity, transform.parent);
            Rigidbody2D rb = mini.GetComponent<Rigidbody2D>();

            if (rb != null) {
                rb.linearVelocity = direction * spreadForce;
            }

            // Ensure minis don't live forever
            Destroy(mini, 1.5f);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Impact also triggers the burst
        if (!hasBurst && (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile Walls"))) {
            Burst();
        }
    }
}