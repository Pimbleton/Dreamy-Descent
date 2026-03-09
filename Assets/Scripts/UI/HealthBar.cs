using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Image healthBar;
    public PlayerStats playerStats;
    public GameObject player;
    private string folderPath = "HealthBars/HealthBar";
    
    void Start() {
        playerStats = player.GetComponent<PlayerStats>();
        UpdateHealthBar();
    }

    public void UpdateHealthBar() {
    if (playerStats == null) {
        Debug.LogError("HealthBar: playerStats is null! Is the script on the Player?");
        return; 
    }

    // 2. Check if you assigned the UI Image in the Inspector
    if (healthBar == null) {
        Debug.LogError("HealthBar: healthBar Image is not assigned in the Inspector!");
        return;
    }

        string fullPath = folderPath + playerStats.maxHP + "_" + playerStats.HP;
        Sprite LoadedSprite = Resources.Load<Sprite>(fullPath);
        Debug.Log("Loading sprite from path: " + fullPath);
        healthBar.sprite = LoadedSprite;
    }    
}

