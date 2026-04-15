using UnityEngine;

public class GenericItem : MonoBehaviour {
    public ItemData itemData;

    void Start() {
        if (itemData == null) {
            Debug.LogError("ItemData not assigned for " + this.gameObject.name);
        }
    }

}