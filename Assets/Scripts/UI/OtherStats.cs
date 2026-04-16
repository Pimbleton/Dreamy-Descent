using TMPro;
using UnityEngine;

public class OtherStats : MonoBehaviour {
    private PlayerStats playerStats;
    private TextMeshProUGUI statsText;
    [HideInInspector] public static OtherStats Instance;

    void Awake() {
        Instance = this;
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        statsText = GetComponent<TextMeshProUGUI>();
    }
    
    void Start() { printStats(); }

    public void printStats() {
        statsText.text = "Attack : " + playerStats.projectileDamage + "\n" +
                         "Fire Rate : " + playerStats.projectileCooldown + "\n" +
                         "Range : " + playerStats.projectileRange + "\n" +
                         "Proj. Spd. : " + playerStats.projectileSpeed + "\n" +
                         "Move Spd. : " + playerStats.moveSpeed;
    }
}