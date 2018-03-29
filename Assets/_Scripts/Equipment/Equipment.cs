using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { Weapon, Armor, None };

public abstract class Equipment : ScriptableObject
{
	public CreatureType equippableCreatureType;
	public EquipmentType equipmentType;

	public Equipment() {}

	//Enchantments go here?
}
