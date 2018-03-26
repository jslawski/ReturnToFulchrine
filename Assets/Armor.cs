using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipment
{
	public float armorClass;

	public Armor(EquipmentClass equipmentClass, float armorClass)
	{
		this.equipmentClass = equipmentClass;
		this.armorClass = armorClass;
	}

	public Armor()
	{
		this.equipmentClass = EquipmentClass.None;
	}

}
