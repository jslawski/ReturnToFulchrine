using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { Weapon, Armor, None };
public enum Rarity { Common, Uncommon, Rare, Legendary };

public abstract class Equipment : ScriptableObject
{
	public Rarity rarity;

	public CreatureType equippableCreatureType;
	public EquipmentType equipmentType;

	public List<Enchantment> enchantments;
	public List<AggregateStatusEffectMetaData> statusEffectMetaData;

	private void UpdateList(StatusEffectAsset statusEffectAsset)
	{
		AggregateStatusEffectMetaData newData = this.statusEffectMetaData.Find(x => x.statusEffect.statusEffectName == statusEffectAsset.statusEffectName);

		if (newData == null)
		{
			this.statusEffectMetaData.Add(new AggregateStatusEffectMetaData(statusEffectAsset, statusEffectAsset.level));
		}
		else
		{
			newData.aggregateLevel += statusEffectAsset.level;
		}
	}

	public void RefreshAggregateStatusEffectMetaDataList()
	{
		this.statusEffectMetaData = new List<AggregateStatusEffectMetaData>();

		foreach (Enchantment currentEnchantment in this.enchantments)
		{
			switch (this.equipmentType)
			{
			case EquipmentType.Weapon:
				this.UpdateList(currentEnchantment.weaponStatusEffect);
				break;
			case EquipmentType.Armor:
				this.UpdateList(currentEnchantment.armorStatusEffect);
				break;
			default:
				Debug.LogError("Equipment.RefreshAggregateStatusEffectMetaDataList: Unknown EquipmentType: " + this.equipmentType + ". Unable to get aggregate status effect meta data.");
				continue;
			}
		}
	}
}
