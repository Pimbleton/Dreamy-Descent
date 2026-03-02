using UnityEngine;

public class PlayerStats : MonoBehaviour {
    //Player Movement Stats
    public float moveSpeed = 10f;
    public float acceleration = 100f;
    public float deceleration = 100f;

    //Player Health
    public int HP = 20;
    public int maxHP = 20;

    // Player Projectile Stats
    public float projectileDamage = 2f;
    public float projectileSpeed = 5f;
    public float projectileRange = 1f; //Seconds projectile is airborn before being destroyed
    public float projectileCooldown = 0.5f;
}
