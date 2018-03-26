using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { Weapon, Armor };
public enum EquipmentClass { Magic, Light, Medium, Heavy, None };

public abstract class Equipment 
{
	public EquipmentClass equipmentClass;
	public EquipmentType equipmentType;

	public Creature wieldingCreature;

	//Enchantments go here?
}
