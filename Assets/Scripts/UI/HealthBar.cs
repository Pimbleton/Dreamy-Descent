using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    private Image healthBar;
    private PlayerStats playerStats;
    private string folderPath = "HealthBars/HealthBar";

    [HideInInspector] public static HealthBar Instance;
    
    void Awake() {
        Instance = this;
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        healthBar = GetComponent<Image>();
    }

    void Start() { UpdateHealthBar(); }

    public void UpdateHealthBar() {
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