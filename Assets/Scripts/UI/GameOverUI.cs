using UnityEngine;

public class GameOverUI : MonoBehaviour {
    public GameObject GameOverDisplay;
    public static GameOverUI Instance;

    void Awake() {
        Instance = this;
    }
    
    void Start() {
        GameOverDisplay.SetActive(false);
    }

    public void summonGameOverPopup() {
        GameOverDisplay.SetActive(true);
    }
}
