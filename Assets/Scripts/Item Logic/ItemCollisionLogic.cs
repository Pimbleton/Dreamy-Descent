using UnityEngine;

public class ItemCollisionLogic : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Inventory inventory = collision.gameObject.GetComponent<Inventory>();

            if (inventory.itemCount >= 100) {
                Debug.Log("Inventory is full! Cannot pick up item: " + this.gameObject.name);
                return;
            } else {
                Debug.Log("Item picked up: " + this.gameObject.name);
                inventory.AddItem(GetComponent<GenericItem>().itemData);
                Destroy(this.gameObject);
            }
            
        }
    }
}
