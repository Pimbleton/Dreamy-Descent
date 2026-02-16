using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public GameObject PauseMenu;
    public Button ResumeButton;
    public Button TitleButton;
    public Button DesktopButton;

    //Pause Menu Condition
    public bool isPaused = false;

    //Player Stats
    public int HP = 20;
    public int maxHP = 20;
    public int projectileDamage = 2;
    public float projectileCooldown = 1f; //Basically just rate of fire, but better suited to be called "cooldown" as a variable.
    public int projectileRange = 5;
    public float moveSpeed = 5f;
    

    void Start() {
        PauseMenu.SetActive(false);
        ResumeButton.onClick.AddListener(() => {isPaused = false; PressResumeButton();});
        TitleButton.onClick.AddListener(() => {isPaused = false;});
        DesktopButton.onClick.AddListener(() => {isPaused = false;});
    }
    
    void Update() {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            isPaused = !isPaused;
        }

        if (!isPaused) {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
    void FixedUpdate() {
        Movement();
    }

    void Movement() {
        Vector2 movementVector = new Vector2(
            Keyboard.current.dKey.IsPressed() ? 1 : 0 - (Keyboard.current.aKey.IsPressed() ? 1 : 0), 
            Keyboard.current.wKey.IsPressed() ? 1 : 0 - (Keyboard.current.sKey.IsPressed() ? 1 : 0)).normalized;

        Vector2 targetPosition = myRigidbody.position + movementVector * moveSpeed * Time.fixedDeltaTime;

        myRigidbody.MovePosition(targetPosition);
    }

    void PressResumeButton() {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }
}