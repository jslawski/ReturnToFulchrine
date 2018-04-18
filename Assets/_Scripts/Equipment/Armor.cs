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
			if (currentEnchantment.defenseAffinity == null)
			{
				continue;
			}

			AggregateAffinityMetaData newData = this.defenseAffinityMetaData.Find(x => x.affinityAsset.type == currentEnchantment.defenseAffinity.type);

			if (newData == null)
			{
				this.defenseAffinityMetaData.Add(new AggregateAffinityMetaData(currentEnchantment.defenseAffinity, currentEnchantment.defenseAffinity.level));
			}
			else
			{
				newData.aggregateLevel += currentEnchantment.defenseAffinity.level;
			}
		}
	}

	public float GetDefenseAffinityMultiplier(AggregateAffinityMetaData attackAffinityMetaData)
	{
		Debug.LogError("Attack Affinity: " + attackAffinityMetaData.affinityAsset.name + " Level: " + attackAffinityMetaData.aggregateLevel);

		float attackFactor = 1f;

		switch (attackAffinityMetaData.aggregateLevel)
		{
		case 1:
			attackFactor = 1f;
			break;
		case 2:
			attackFactor = 1.1f;
			break;
		case 3:
			attackFactor = 1.2f;
			break;
		case 4:
			attackFactor = 1.3f;
			break;
		case 5:
			attackFactor = 1.4f;
			break;
		default:
			Debug.LogError("Armor.GetDefenseAffinityMultiplier: Unsupported affinity level: " + attackAffinityMetaData.aggregateLevel + ". Unable to apply attackFactor.");
			break;
		}

		Debug.LogError("Chosen Attack Factor: " + attackFactor);

		float defenseFactor = 0f;
		foreach (AggregateAffinityMetaData metaData in this.defenseAffinityMetaData)
		{
			if (metaData.affinityAsset.defenseDisadvantageAffinities.Contains(attackAffinityMetaData.affinityAsset.type))
			{
				defenseFactor -= 1f;
			}
			else if (metaData.affinityAsset.defenseAdvantageAffinities.Contains(attackAffinityMetaData.affinityAsset.type))
			{
				switch (metaData.aggregateLevel)
				{
				case 1:
					defenseFactor += 0.5f;
					break;
				case 2:
					defenseFactor += 0.7f;
					break;
				case 3:
					defenseFactor += 0.8f;
					break;
				case 4:
					defenseFactor += 0.9f;
					break;
				case 5:
					defenseFactor += 1f;
					break;
				default:
					Debug.LogError("Armor.GetDefenseAffinityMultiplier: Unsupported affinity level: " + metaData.aggregateLevel + ". Unable to apply defenseFactor.");
					break;
				}
			}
		}
	
		Debug.LogError("Chosen Defense Factor: " + defenseFactor);

		return Mathf.Abs(defenseFactor - attackFactor);
	}
}
