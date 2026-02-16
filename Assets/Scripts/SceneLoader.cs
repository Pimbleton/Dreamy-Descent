using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scenes/TitleScreen");
    }
    
    public void LoadFirstFloor()
    {
        SceneManager.LoadScene("Scenes/FirstFloor");
    }
}


