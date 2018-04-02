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
