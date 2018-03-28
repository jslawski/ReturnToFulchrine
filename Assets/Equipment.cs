using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { Weapon, Armor };

public abstract class Equipment : ScriptableObject
{
	public CreatureType equippableCreatureType;
	public EquipmentType equipmentType;

	//Enchantments go here?
}
