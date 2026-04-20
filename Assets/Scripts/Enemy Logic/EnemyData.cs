using UnityEngine;

// Creates an asset menu for enemies, streamlining the stat making process for them.
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject {
    [Header("Identity")]
    public string enemyName = "Sample Enemy Name";

    [Header("Stats")]
    public float HP = 0;
    public int maxHP = 0;
    public int contactDamage = 0;
    public int projectileDamage = 0;
    public float projectileRange = 0;
    public float projectileSpeed = 0;
    public float movementSpeed = 0;
}