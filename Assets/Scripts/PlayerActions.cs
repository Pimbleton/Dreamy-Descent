using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    //Borrows 
    public Rigidbody2D myRigidbody;
    public GameObject PauseMenu;
    public GameObject PlayerProjectilePrefab;
    public Button ResumeButton;
    public Button TitleButton;
    public Button DesktopButton;
    
    //Pause Menu Condition
    public bool isPaused = false;

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
    public float projectileRange = 1f;
    public float projectileCooldown = 0.5f;
    private float nextProjectileAvailability = 0f;


    //Movement Stuff

    

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

        if (Keyboard.current.upArrowKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed) {
                if (Time.time > nextProjectileAvailability) {
                    Shoot();
                    nextProjectileAvailability = Time.time + projectileCooldown;
                }
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

        Vector2 targetVelocity = movementVector * moveSpeed;

        float accelerationThreshold = movementVector.magnitude > 0 ? acceleration : deceleration;

        myRigidbody.linearVelocity = Vector2.MoveTowards(myRigidbody.linearVelocity, targetVelocity, accelerationThreshold * Time.fixedDeltaTime);
    }

    void PressResumeButton() {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }

    void Shoot() {
        GameObject bullet = Instantiate(PlayerProjectilePrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null) {
            if (Keyboard.current.upArrowKey.isPressed) {
                rb.linearVelocity = Vector2.up * projectileSpeed;
            }
            else if (Keyboard.current.downArrowKey.isPressed) {
                rb.linearVelocity = Vector2.down * projectileSpeed;
            }
            else if (Keyboard.current.rightArrowKey.isPressed) {
                rb.linearVelocity = Vector2.right * projectileSpeed;
            }
            else if (Keyboard.current.leftArrowKey.isPressed) {
                rb.linearVelocity = Vector2.left * projectileSpeed;
            }
        }

        Destroy(bullet, projectileRange);
    }
}