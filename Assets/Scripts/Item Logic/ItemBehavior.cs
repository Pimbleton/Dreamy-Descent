using UnityEngine;

public class ItemBehavior : MonoBehaviour {
    [Header("Item Data Goes Here")]
    public ItemData itemData;

    void OnCollisionEnter2D(Collision2D collision) {
        // If touched by player, obtain their inventory,
        if (collision.gameObject.CompareTag("Player")) {
            Inventory inventory = collision.gameObject.GetComponent<Inventory>();

            // If player has already reached max number of items, leave item as is.
            // Otherwise, add the item's data (stat inc/dec) to the player's stats, then destroy item.
            if (inventory.itemCount >= 100) {
                return;
            } else {
                inventory.AddItem(itemData);
                Destroy(gameObject);
            }
        }
    }
}