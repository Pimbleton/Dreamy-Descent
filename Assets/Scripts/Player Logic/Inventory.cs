using UnityEngine;

public class Inventory : MonoBehaviour {
    [SerializeField] private PlayerStats playerStats;
    private ItemData[] items;
    [HideInInspector] public int itemCount = 0;

    void Awake() {
        items = new ItemData[100];
    }

    public void AddItem(ItemData item) {
        if (itemCount < 100) {
            items[itemCount] = item;
            itemCount++;
            updateStats(item);
        }
    }

    public void updateStats(ItemData item) {
        // If the item has any HP or max HP stat changes, apply those first, since they can cause death if max HP is reduced too much.
        if (item.HPStat != 0 || item.maxHPStat != 0) {
            if (item.maxHPStat != 0) {
                if (playerStats.maxHP + item.maxHPStat <= 10 && playerStats.maxHP + item.maxHPStat >= 1) {
                    playerStats.maxHP += item.maxHPStat;
                    HealthBar.Instance.UpdateHealthBar(); 
                } else if (playerStats.maxHP + item.maxHPStat > 10) {
                    playerStats.maxHP = 10;
                } else if (playerStats.maxHP + item.maxHPStat < 1) {
                    DeathHandler.Instance.Die(gameObject);
                }
            }

            if (item.HPStat != 0) {
                // If the HP stat increase would put the player's HP over their max HP, set it to the max HP instead.
                if (playerStats.HP + item.HPStat <= playerStats.maxHP) {
                    playerStats.HP += item.HPStat;
                    HealthBar.Instance.UpdateHealthBar();
                } else {
                    playerStats.HP = playerStats.maxHP;
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