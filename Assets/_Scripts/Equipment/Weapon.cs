using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Equipment
{
	public float critChance = 0.5f;
	public float knockbackChance = 0.5f;
	public float knockbackDistance = 5f;
	public int windUpFrames;
	public int windDownFrames;

	[HideInInspector]
	public bool continueAttacking = false;

	[HideInInspector]
	public bool currentlyAttacking = false;

	[HideInInspector]
	public List<AggregateAffinityMetaData> attackAffinityMetaData;

	public abstract IEnumerator Attack(Creature wieldingCreature);

	public override void RefreshAggregateMetaData()
	{
		this.RefreshAggregateStatusEffectMetaDataList();
		this.RefreshAggregateAttackAffinityMetaDataList();
	}

	private void RefreshAggregateAttackAffinityMetaDataList()
	{
		this.attackAffinityMetaData = new List<AggregateAffinityMetaData>();

		foreach (Enchantment currentEnchantment in this.enchantments)
		{
			if (currentEnchantment.attackAffinity == null)
			{
				continue;
			}
				
			AggregateAffinityMetaData newData = this.attackAffinityMetaData.Find(x => x.affinityAsset.type == currentEnchantment.attackAffinity.type);

			if (newData == null)
			{
				this.attackAffinityMetaData.Add(new AggregateAffinityMetaData(currentEnchantment.attackAffinity, currentEnchantment.attackAffinity.level));
			}
			else
			{
				newData.aggregateLevel += currentEnchantment.attackAffinity.level;
			}
		}
	}
}
