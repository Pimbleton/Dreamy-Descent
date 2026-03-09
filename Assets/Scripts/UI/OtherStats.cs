using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class OtherStats : MonoBehaviour {
    public PlayerStats playerStats;
    public GameObject player;

    public TextMeshProUGUI statsText;

    void Start() {
        playerStats = player.GetComponent<PlayerStats>();
        UpdateStats();
    }

    public void UpdateStats() {
        statsText.text = "Attack: " + playerStats.projectileDamage + "\n" +
                         "Fire Rate: " + playerStats.projectileCooldown + "\n" +
                         "Range: " + playerStats.projectileRange + "\n" +
                         "P. Speed: " + playerStats.projectileSpeed + "\n" +
                         "M. Speed: " + playerStats.projectileKnockback;
    }
}
