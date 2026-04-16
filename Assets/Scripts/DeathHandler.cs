using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathHandler : MonoBehaviour {
    [HideInInspector] public static DeathHandler Instance;

    void Awake() { Instance = this; }

    public void Die(GameObject entity) {
        // If player dies, start game over countdown
        if (entity.CompareTag("Player")) {
            Destroy(entity);
            GameOverUI.Instance.summonGameOverPopup();
            Instance.StartCoroutine(Instance.DeathTimerRoutine());
        } else {
            Destroy(entity);
        }
    }

    private IEnumerator DeathTimerRoutine() {
        // Freeze the entire game world
        Time.timeScale = 0f;

        // Wait for 5 real-time seconds
        yield return new WaitForSecondsRealtime(5f);

        // Resume time
        Time.timeScale = 1f;

        // Load main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}