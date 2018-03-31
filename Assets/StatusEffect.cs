using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour{

	public float remainingDuration;

	public bool activateOnEquip = false;

	public StatusEffect(){}

	public abstract void TryApplyStatusEffect(int enchantmentLevel, Creature affectedCreature);
	public abstract void ApplyStatusEffect(int enchantmentLevel, Creature affectedCreature);

	public void DepleteStatusEffect()
	{
		this.StartCoroutine(this.ConsumeStatusEffectTime());
	}

	private IEnumerator ConsumeStatusEffectTime()
	{
		while (this.remainingDuration > 0)
		{
			this.remainingDuration -= Time.deltaTime;
			yield return null;
		}
	}

	public virtual void StopStatusEffect()
	{
		this.StopAllCoroutines();
	}
}
