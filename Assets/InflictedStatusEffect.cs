using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InflictedStatusEffect : StatusEffect 
{
	public void Start()
	{
		this.TryApplyStatusEffect(this.gameObject.GetComponent<Creature>());
	}
}
