using UnityEngine;

public class BossObjectsBehavior : MonoBehaviour {
    [SerializeField] private GameObject itemObject, pit;

    public static BossObjectsBehavior Instance;

    void Awake() {
        itemObject.SetActive(false);
        pit.SetActive(false);
        Instance = this;
    }

    public void ShowObjects() {
        itemObject.SetActive(true);
        pit.SetActive(true);
    }
}
