using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EquipStatusEffects are immediately equipped to a Creature as a component.
/// Their effects last indefinitely as long as the Creature is using a piece of Equipment that 
/// has an Enchantment that contains the EquipStatusEffect.
/// </summary>
public abstract class EquipStatusEffect : StatusEffect 
{
	public EquipmentType equipmentType;

	public abstract void UpdateStatusEffect();
}
