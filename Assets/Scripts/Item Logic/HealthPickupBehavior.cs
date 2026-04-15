using UnityEngine;

public class HealthPickupBehavior : MonoBehaviour {
    private PlayerStats playerStats;

    void Awake() {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // On collision, if player's HP != their maxHP, increment Hp by 1 and destroy the heart pickup.
    // Else, leave it be.
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (playerStats.HP != playerStats.maxHP) {
                playerStats.HP += 1;
                HealthBar.Instance.UpdateHealthBar();
                Destroy(gameObject);
            }
        } 
    }
}
