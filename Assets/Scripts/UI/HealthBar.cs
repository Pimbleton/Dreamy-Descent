using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public static HealthBar Instance;

    public Image healthBar;
    private PlayerStats playerStats;
    public GameObject player;
    private string folderPath = "HealthBars/HealthBar";
    
    void Awake() {
        Instance = this;
    }

    void Start() {
        if (player != null) {
            playerStats = player.GetComponent<PlayerStats>();
        }
        
        UpdateHealthBar();
    }

    public void updateHPAndMaxHP() {
        UpdateHealthBar();
    }

    public void UpdateHealthBar() {
        if (playerStats == null) {
            return;
        }

        int health = playerStats.HP;
        int maxHealth = playerStats.maxHP;

        string fullPath = folderPath + maxHealth + "_" + health;
        Sprite LoadedSprite = Resources.Load<Sprite>(fullPath);
    
        if (LoadedSprite != null){
            healthBar.sprite = LoadedSprite;
        } else {
            Debug.LogError("Health bar sprite not found at path: " + fullPath);
        }
    }    
}