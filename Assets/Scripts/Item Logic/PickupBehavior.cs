using UnityEngine;

public class PickupBehavior : MonoBehaviour {
    public PlayerStats playerStats;

    void Awake() {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

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
