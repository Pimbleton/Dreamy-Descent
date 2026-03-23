using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class ItemData : ScriptableObject {
    
    [Header("Identity")]
    public string itemName = "Sample Item Name";
    [TextArea(3, 10)]
    public string itemDescription = "Sample Item Description";

    [Header("Combat Stats")]
    public int HPStat = 0;
    public int maxHPStat = 0;
    public float projectileDamageStat = 0;
    public float projectileRangeStat = 0;
    public float projectileSpeedStat = 0;
    public float projectileKnockbackStat = 0;

    [Header("Utility Stats")]
    public float projectileCooldownStat = 0;
    public float speedStat = 0;
    public float invincibilityDurationStat = 0;
}
