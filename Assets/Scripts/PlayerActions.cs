using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float moveSpeed = 5f;

    void FixedUpdate() {
        movement();
    }

    void movement() {
        Vector2 movementVector = new Vector2(
            Keyboard.current.dKey.IsPressed() ? 1 : 0 - (Keyboard.current.aKey.IsPressed() ? 1 : 0), 
            Keyboard.current.wKey.IsPressed() ? 1 : 0 - (Keyboard.current.sKey.IsPressed() ? 1 : 0)).normalized;

        Vector2 targetPosition = myRigidbody.position + movementVector * moveSpeed * Time.fixedDeltaTime;

        myRigidbody.MovePosition(targetPosition);
    }
}