using UnityEngine;

public class CongratsUI : MonoBehaviour {
    public GameObject CongratsDisplay;
    public static CongratsUI Instance;

    void Awake() {
        Instance = this;
    }
    
    void Start() {
        CongratsDisplay.SetActive(false);
    }

    public void summonCongratsPopup() {
        CongratsDisplay.SetActive(true);
    }
}