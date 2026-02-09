using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public Rigidbody2D myRigidbody;

    void Update() {
        checkMovement();
    }

    void checkMovement() {
        //TODO - Integrate momentum/acceleration and implement collision detection
        if (Keyboard.current.wKey.IsPressed() || Keyboard.current.sKey.IsPressed() || Keyboard.current.aKey.IsPressed() || Keyboard.current.dKey.IsPressed()) {
            int north = Keyboard.current.wKey.IsPressed() ? 1 : 0;
            int south = Keyboard.current.sKey.IsPressed() ? 1 : 0;
            int east = Keyboard.current.dKey.IsPressed() ? 1 : 0;
            int west = Keyboard.current.aKey.IsPressed() ? 1 : 0;

            //if (!OnCollisionEnter(myRigidbody)) {
                myRigidbody.linearVelocity = Vector2.up * 10 * north + Vector2.down * 10 * south + Vector2.left * 10 * west + Vector2.right * 10 * east;
            //}
            //else {
            //    myRigidbody.linearVelocity = Vector2.zero;
           // }
        }
        else {
            myRigidbody.linearVelocity = Vector2.zero;
        }
    }
}
