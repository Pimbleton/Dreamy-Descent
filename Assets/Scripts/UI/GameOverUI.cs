using UnityEngine;

public class GameOverUI : MonoBehaviour {
    public GameObject GameOverDisplay;
    public static GameOverUI Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameOverSound;

    void Awake() {
        Instance = this;
    }
    
    void Start() {
        GameOverDisplay.SetActive(false);
    }

    public void summonGameOverPopup() {
        GameOverDisplay.SetActive(true);
        audioSource.PlayOneShot(gameOverSound, .75f);
    }
}
