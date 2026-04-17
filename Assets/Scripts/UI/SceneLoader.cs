using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public void LoadMainMenu() { SceneManager.LoadScene("Scenes/MainMenu"); }
    
    public void LoadFirstFloor() { SceneManager.LoadScene("Scenes/Game"); }
}