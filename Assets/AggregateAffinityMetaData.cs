using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If a piece of Equipment has multiple Enchantments that reference the same Affinity,
/// this class will be responsible for consolidating that information into 
/// metadata that aggregates repeated Affinities to have one, combined level
/// </summary>
public class AggregateAffinityMetaData 
{
	public Affinity affinityAsset;
	public int aggregateLevel;

	public AggregateAffinityMetaData(Affinity affinityAsset, int level)
	{
		this.affinityAsset = affinityAsset;
		this.aggregateLevel = level;
	}
}
