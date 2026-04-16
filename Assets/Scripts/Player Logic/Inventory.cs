using UnityEngine;

public class Inventory : MonoBehaviour {
    [SerializeField] private PlayerStats playerStats;
    private ItemData[] items;
    [HideInInspector] public int itemCount = 0;

    void Awake() { items = new ItemData[100]; }

    public void AddItem(ItemData item) {
        if (itemCount < 100) {
            items[itemCount] = item;
            itemCount++;
            Debug.Log("Item added to inventory: " + item.itemName);

            updateStats(item);
        } else {
            Debug.Log("Inventory is full! Cannot add item: " + item.itemName);
        }
    }

    public void updateStats(ItemData item) {
        if (item.HPStat != 0 || item.maxHPStat != 0) {
            if (item.HPStat != 0) {
                if (playerStats.HP + item.HPStat <= playerStats.maxHP + item.maxHPStat) {
                    playerStats.HP += item.HPStat;
                } else {
                    playerStats.HP = playerStats.maxHP + item.maxHPStat;
                }
            }

            if (item.maxHPStat != 0) {
                if (playerStats.maxHP + item.maxHPStat <= 10 && playerStats.maxHP + item.maxHPStat >= 1) {
                    playerStats.maxHP += item.maxHPStat;  
                } else if (playerStats.maxHP + item.maxHPStat > 10) {
                    playerStats.maxHP = 10;
                } else if (playerStats.maxHP + item.maxHPStat < 1) {
                    DeathHandler.Instance.Die(gameObject);
                }
            }
        }

        if (item.projectileDamageStat != 0 || item.projectileKnockbackStat != 0 || item.projectileCooldownStat != 0 || item.projectileRangeStat != 0 || item.projectileSpeedStat != 0 || item.speedStat != 0 || item.invincibilityDurationStat != 0) {
            playerStats.projectileDamage += item.projectileDamageStat;
            playerStats.projectileKnockback += item.projectileKnockbackStat;
            playerStats.projectileCooldown += item.projectileCooldownStat;
            playerStats.projectileRange += item.projectileRangeStat;
            playerStats.projectileSpeed += item.projectileSpeedStat;
            playerStats.moveSpeed += item.speedStat;
            playerStats.invincibilityDuration += item.invincibilityDurationStat;
        }

        OtherStats.Instance.printStats();
    }
}