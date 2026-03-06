using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private float moveSpeed;
    private float acceleration;
    private float deceleration;
    public Rigidbody2D myRigidbody;
    
    void Start() {
        moveSpeed = GetComponent<PlayerStats>().moveSpeed;
        acceleration = GetComponent<PlayerStats>().acceleration;
        deceleration = GetComponent<PlayerStats>().deceleration;
    }
    
    void FixedUpdate() {
        Movement();
    }

    void Movement() {
        Vector2 movementVector = new Vector2(
            Keyboard.current.dKey.IsPressed() ? 1 : 0 - (Keyboard.current.aKey.IsPressed() ? 1 : 0), 
            Keyboard.current.wKey.IsPressed() ? 1 : 0 - (Keyboard.current.sKey.IsPressed() ? 1 : 0)).normalized;

        Vector2 targetVelocity = movementVector * moveSpeed;

        float accelerationThreshold = movementVector.magnitude > 0 ? acceleration : deceleration;

        myRigidbody.linearVelocity = Vector2.MoveTowards(myRigidbody.linearVelocity, targetVelocity, accelerationThreshold * Time.fixedDeltaTime);
    }
}
