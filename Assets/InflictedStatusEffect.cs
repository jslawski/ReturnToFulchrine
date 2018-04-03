using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InflictedStatusEffect : StatusEffect 
{
	public delegate void InflicedStatusEffectEnded(InflictedStatusEffect statusEffect);
	public event InflicedStatusEffectEnded onInflicedStatusEffectEnded;

	public abstract bool RollForInflictionChance();
	public abstract void AttemptStatusEffectRefresh(Creature affectedCreature);


	public void Start()
	{
		this.TryApplyStatusEffect(this.gameObject.GetComponent<Creature>());
	}

	public override void StopStatusEffect()
	{
		if (this.onInflicedStatusEffectEnded != null)
		{
			this.onInflicedStatusEffectEnded(this);
		}

		Destroy(this);
	}


}
