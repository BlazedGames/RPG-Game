using UnityEngine;



[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumable")]
public class Consumable : Item {

	public int healthGain;	


	public override void Use()
	{
		// Heal the player : Requires Instance of player and uses a player Stats class I'll create in a while
//		PlayerStats playerStats = Player.instance.playerStats;
//		playerStats.Heal(healthGain);

		Debug.Log(name + " consumed!");

		RemoveFromInventory();	
	}

}
