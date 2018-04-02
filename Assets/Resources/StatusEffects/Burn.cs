using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : InflictedStatusEffect {

	float timeUntilNextInfliction = 1f;
	float timeBetweenInflictions = 1f;

	public override void TryApplyStatusEffect(Creature affectedCreature)
	{
		this.remainingDuration = 10f;
		this.ApplyStatusEffect(affectedCreature);
	}

	public override void ApplyStatusEffect(Creature affectedCreature)
	{
		this.StopAllCoroutines();

		this.affectedCreature = affectedCreature;
	}

	private IEnumerator ApplyBurn()
	{
		while (this.remainingDuration > 0)
		{
			if (this.timeUntilNextInfliction > 0)
			{
				this.timeUntilNextInfliction -= Time.deltaTime;
				continue;
			}

			affectedCreature.TakeDamage(this.level);
			this.timeUntilNextInfliction = timeBetweenInflictions;
			yield return null;
		}
	}
}
