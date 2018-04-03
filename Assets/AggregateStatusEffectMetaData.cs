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
}
