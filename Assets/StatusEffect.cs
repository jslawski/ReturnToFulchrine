using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class StatusEffect : MonoBehaviour {

	public float remainingDuration;
	public int level;
	public Creature affectedCreature;
	public string statusEffectName;

	public virtual void TryApplyStatusEffect(Creature affectedCreature){}
	public virtual void ApplyStatusEffect(Creature affectedCreature){}

	public void DepleteStatusEffect()
	{
		this.StopAllCoroutines();
		this.StartCoroutine(this.ConsumeStatusEffectTime());
	}

	private IEnumerator ConsumeStatusEffectTime()
	{
		while (this.remainingDuration > 0)
		{
			this.remainingDuration -= Time.deltaTime;
			yield return null;
		}

		this.StopStatusEffect();
	}

	public virtual void StopStatusEffect()
	{
		this.StopAllCoroutines();
	}
}
