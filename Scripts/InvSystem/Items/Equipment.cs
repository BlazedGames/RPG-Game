using UnityEngine;



[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public class Equipment : Item {

	public EquipmentSlot equipSlot; // What slot to equip it in
	public int armorModifier;
	public int damageModifier;
	public SkinnedMeshRenderer prefab;


	public override void Use ()
	{
		EquipmentManager.instance.Equip(this);	
		RemoveFromInventory();	
	}

}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet}
