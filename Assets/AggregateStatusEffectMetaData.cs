using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If a piece of equipment has multiple enchantments that reference the same status effects,
/// this class will be responsible for consolidating that information into a list of status effect
/// metadata that aggregates repeated status effects to have one, combined level
/// </summary>
public class AggregateStatusEffectMetaData 
{
	public StatusEffectAsset statusEffect;
	public int aggregateLevel;

	public AggregateStatusEffectMetaData(StatusEffectAsset statusEffect, int level)
	{
		this.aggregateLevel = level;
		this.statusEffect = statusEffect;
	}

	//TODO: Move this implementation to Equipment, it makes more sense there.
	private static void UpdateList(List<AggregateStatusEffectMetaData> currentList, StatusEffectAsset statusEffectAsset)
	{
		AggregateStatusEffectMetaData newData = currentList.Find(x => x.statusEffect.statusEffectName == statusEffectAsset.statusEffectName);

		if (newData == null)
		{
			currentList.Add(new AggregateStatusEffectMetaData(statusEffectAsset, statusEffectAsset.level));
		}
		else
		{
			newData.aggregateLevel += statusEffectAsset.level;
		}
	}

	public static List<AggregateStatusEffectMetaData> GetAggregateStatusEffectMetaData(Equipment equipment)
	{
		List<AggregateStatusEffectMetaData> metaData = new List<AggregateStatusEffectMetaData>();

		foreach (Enchantment currentEnchantment in equipment.enchantments)
		{
			switch (equipment.equipmentType)
			{
			case EquipmentType.Weapon:
				AggregateStatusEffectMetaData.UpdateList(metaData, currentEnchantment.weaponStatusEffect);
				break;
			case EquipmentType.Armor:
				AggregateStatusEffectMetaData.UpdateList(metaData, currentEnchantment.armorStatusEffect);
				break;
			default:
				Debug.LogError("AggregateStatusEffectMetaData.GetAggregateStatusEffectMetaData: Unknown EquipmentType: " + equipment.equipmentType + ". Unable to get aggregate status effect meta data.");
				return metaData;
			}
		}

		return metaData;
	}
}
