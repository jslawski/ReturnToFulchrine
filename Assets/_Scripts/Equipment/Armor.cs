using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Equipment/Armor")]
public class Armor : Equipment
{
	public float armorClass;

	[HideInInspector]
	public List<AggregateAffinityMetaData> defenseAffinityMetaData;

	public override void RefreshAggregateMetaData()
	{
		this.RefreshAggregateStatusEffectMetaDataList();
		this.RefreshAggregateDefenseAffinityMetaDataList();
	}

	private void RefreshAggregateDefenseAffinityMetaDataList()
	{
		this.defenseAffinityMetaData = new List<AggregateAffinityMetaData>();

		foreach (Enchantment currentEnchantment in this.enchantments)
		{
			AggregateAffinityMetaData newData = this.defenseAffinityMetaData.Find(x => x.affinityType == currentEnchantment.defenseAffinity.type);

			if (newData == null)
			{
				this.defenseAffinityMetaData.Add(new AggregateAffinityMetaData(currentEnchantment.defenseAffinity.type, currentEnchantment.defenseAffinity.level));
			}
			else
			{
				newData.aggregateLevel += currentEnchantment.defenseAffinity.level;
			}
		}
	}
}
