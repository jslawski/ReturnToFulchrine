using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If a piece of Equipment has multiple Enchantments that reference the same StatusEffects,
/// or multiple pieces of Equipment contain Enchantments with the same StatusEffects,
/// this class will be responsible for consolidating that information into
///  metadata that aggregates repeated status effects to have one, combined level
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
