using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseLogic : MonoBehaviour {
    public GameObject PauseMenu;
    public Button ResumeButton, TitleButton, DesktopButton;
    public bool isPaused = false;

    void Awake() { PauseMenu.SetActive(false); }
    
    void Start() {
        ResumeButton.onClick.AddListener(() => {isPaused = false; PressResumeButton();});
        TitleButton.onClick.AddListener(() => {isPaused = false;});
        DesktopButton.onClick.AddListener(() => {isPaused = false;});
    }

    void Update() {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) isPaused = !isPaused;

        if (!isPaused) {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        } else {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void PressResumeButton() {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }
}