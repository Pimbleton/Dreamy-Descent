using UnityEngine;

public class DeathHandler : MonoBehaviour {
    public static DeathHandler Instance;

    void Awake() {
        Instance = this;
    }

    public void Die() {
        Destroy(gameObject);
        //Implement game over screen and restart level logic here
    }
}
