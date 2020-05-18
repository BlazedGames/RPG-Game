using UnityEngine;

public class ItemPickup : Interactable {

	public Item item;	// Item to put in the inventory if picked up

	
	public override void Interact()
	{
		base.Interact();

		PickUp();
      
	}

	// Pick up the item
	void PickUp ()
	{
        
		Debug.Log("Picking up " + item.name);
		Inventory.instance.Add(item);	

		Destroy(gameObject);	
	}

}
