using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PitLogic : MonoBehaviour {
    private GameObject player;
    private AudioSource audioSource;
    public static PitLogic Instance;

    void Awake() { 
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = player.GetComponent<AudioSource>();
        Instance = this;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (FloorGenerator.Instance.currentFloor == 5) {
                audioSource.Stop();
                audioSource.clip = Resources.Load<AudioClip>($"Sounds/SoundEffects/congrats");
                audioSource.PlayOneShot(audioSource.clip, .75f);
                
                CongratsUI.Instance.summonCongratsPopup();
                Instance.StartCoroutine(Instance.CongratulatePlayer());
            } else {
                // Pit entered, now incrementing to next floor.
                FloorGenerator.Instance.currentFloor += 1;

                audioSource.Stop();
                audioSource.clip = Resources.Load<AudioClip>($"Sounds/Music/f{FloorGenerator.Instance.currentFloor}Music");
                audioSource.Play();

                // Sends player to center of "new" floor and centers camera there.
                player.transform.position = Vector3.zero;
                Camera.main.transform.position = new Vector3(0f, 0f, Camera.main.transform.position.z);

                // Generate the new floor.
                FloorGenerator.Instance.ResetFloor();
            }

        }
    }

    private IEnumerator CongratulatePlayer() {
        // Freeze the entire game world
        Time.timeScale = 0f;

        // Wait for 5 real-time seconds
        yield return new WaitForSecondsRealtime(10f);

        // Resume time
        Time.timeScale = 1f;

        // Load main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
