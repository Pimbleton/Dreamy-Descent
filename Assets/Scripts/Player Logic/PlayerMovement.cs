using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private PlayerStats playerStats;
    public Rigidbody2D myRigidbody;
    
    void Start() {
        playerStats = GetComponent<PlayerStats>();
    }
    
    void FixedUpdate() {
        Movement();
    }

    void Movement() {
        Vector2 movementVector = new Vector2(
            Keyboard.current.dKey.IsPressed() ? 1 : 0 - (Keyboard.current.aKey.IsPressed() ? 1 : 0), 
            Keyboard.current.wKey.IsPressed() ? 1 : 0 - (Keyboard.current.sKey.IsPressed() ? 1 : 0)).normalized;

        Vector2 targetVelocity = movementVector * playerStats.moveSpeed;

        float accelerationThreshold = movementVector.magnitude > 0 ? playerStats.acceleration : playerStats.deceleration;

        myRigidbody.linearVelocity = Vector2.MoveTowards(myRigidbody.linearVelocity, targetVelocity, accelerationThreshold * Time.fixedDeltaTime);
    }
}
