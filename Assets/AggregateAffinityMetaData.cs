using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggregateAffinityMetaData 
{
	public AffinityType affinityType;
	public int aggregateLevel;

	public AggregateAffinityMetaData(AffinityType type, int level)
	{
		this.affinityType = type;
		this.aggregateLevel = level;
	}
}
