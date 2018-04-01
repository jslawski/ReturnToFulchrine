using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class StatusEffect : MonoBehaviour {

	public float remainingDuration;
	public int level;
	public Creature affectedCreature;

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

	/// <summary>
	/// Should only be used to remove status effects that are wrongfully put on a creature
	/// before they take any effect
	/// </summary>
	public abstract void ForceRemoveStatusEffect();
}
