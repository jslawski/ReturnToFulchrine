using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : InflictedStatusEffect {

	float timeUntilNextInfliction = 1f;
	float timeBetweenInflictions = 1f;

	public override bool RollForInflictionChance()
	{
		//Do some logic based on the level here
		return true;
	}

	public override void TryApplyStatusEffect(Creature affectedCreature)
	{
		if (this.RollForInflictionChance() == true)
		{
			this.remainingDuration = 10f;
			this.ApplyStatusEffect(affectedCreature);
		}
		else
		{
			this.StopStatusEffect();
		}
	}

	public override void AttemptStatusEffectRefresh(Creature affectedCreature)
	{
		if (this.RollForInflictionChance() == true)
		{
			this.StopAllCoroutines();
			this.remainingDuration = 10f;
			this.ApplyStatusEffect(affectedCreature);
		}
	}

	public override void ApplyStatusEffect(Creature affectedCreature)
	{
		this.StopAllCoroutines();

		this.affectedCreature = affectedCreature;

		this.StartCoroutine(this.ApplyBurn());
	}

	private IEnumerator ApplyBurn()
	{
		while (this.remainingDuration > 0)
		{
			this.remainingDuration -= Time.deltaTime;

			if (this.timeUntilNextInfliction > 0)
			{
				this.timeUntilNextInfliction -= Time.deltaTime;
				yield return null;
				continue;
			}

			affectedCreature.TakeDamage(10, true);
			this.timeUntilNextInfliction = timeBetweenInflictions;
			yield return null;
		}

		this.StopStatusEffect();
	}
}
