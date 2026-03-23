using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class OtherStats : MonoBehaviour {
    public static OtherStats Instance;

    public PlayerStats playerStats;
    public GameObject player;
    public TextMeshProUGUI statsText;

    void Awake() {
        Instance = this;
    }
    
    void Start() {
        if (player != null) {
            playerStats = player.GetComponent<PlayerStats>();
            printStats();
        }
    }

    public void printStats() {
        statsText.text = "Attack : " + playerStats.projectileDamage + "\n" +
                         "Fire Rate : " + playerStats.projectileCooldown + "\n" +
                         "Range : " + playerStats.projectileRange + "\n" +
                         "Proj. Spd. : " + playerStats.projectileSpeed + "\n" +
                         "Move Spd. : " + playerStats.moveSpeed;
    }
}