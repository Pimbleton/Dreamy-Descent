using UnityEngine;

public class PlayerStats : MonoBehaviour {
    [Header("Player Movement Stats")]
    public float moveSpeed = 5f;
    public float acceleration = 100f;
    public float deceleration = 100f;

    [Header("Player HP Stats")]
    public int HP = 5;
    public int maxHP = 5;

    [Header("Player Projectile Stats")]
    public float projectileCooldown = 0.5f;
    public float projectileDamage = 2f;
    public float projectileRange = 1f;
    public float projectileSpeed = 6f;
    public float projectileKnockback = 5f;

    [Header("Misc Stats")]
    public float invincibilityDuration = 1.5f;
}
