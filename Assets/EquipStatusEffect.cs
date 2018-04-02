using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipStatusEffect : StatusEffect 
{
	public EquipmentType equipmentType;

	public abstract void UpdateStatusEffect();
}
