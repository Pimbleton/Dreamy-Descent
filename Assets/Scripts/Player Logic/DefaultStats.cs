using UnityEngine;

public class DefaultStats : MonoBehaviour{
    public float moveSpeed = 2f;
    public float acceleration = 100f;
    public float deceleration = 100f;

    //Player Health
    public int HP = 5;
    public int maxHP = 5;

    // Player Projectile Stats
    public float projectileCooldown = 0.5f;
    public float projectileDamage = 2f;
    public float projectileRange = 1f; //Seconds projectile is airborn before being destroyed
    public float projectileSpeed = 5f;
    public float projectileKnockback = 5f;

    // Other Player Stats
    public float invincibilityDuration = 2f;
}
