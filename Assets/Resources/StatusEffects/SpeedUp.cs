using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Granted StatusEffect.  Boosts creature's movement speed while equipped.
/// </summary>
public class SpeedUp : StatusEffect {

	private float originalMoveSpeed;
	private float moveSpeedBuff;

	public override void TryApplyStatusEffect(int enchantmentLevel, Creature affectedCreature)
	{
		this.ApplyStatusEffect(enchantmentLevel, affectedCreature);
	}

	public override void ApplyStatusEffect(int enchantmentLevel, Creature affectedCreature)
	{
		this.affectedCreature = affectedCreature;

		this.originalMoveSpeed = affectedCreature.moveSpeed;
		this.moveSpeedBuff = this.GetMoveSpeedBuff(enchantmentLevel);
		Debug.LogError("Move Speed Buff: " + this.moveSpeedBuff);

		this.level = enchantmentLevel;

		this.ApplySpeedUp();
	}

	private float GetMoveSpeedBuff(int enchantmentLevel)
	{
		switch (enchantmentLevel)
		{
		case 1:
			return 0.1f;
		case 2:
			return 0.5f;
		case 3:
			return 0.75f;
		case 4:
			return 1f;
		case 5:
			return 1.25f;
		default:
			Debug.LogError("SpeedUp.GetMoveSpeedBuff: Unknown enchantmentLevel " + enchantmentLevel + ". Unable to apply StatusEffect.");
			return 0f;
		}
	}

	private void ApplySpeedUp()
	{
		this.affectedCreature.moveSpeed = originalMoveSpeed + (originalMoveSpeed * moveSpeedBuff);
	}

	public override void StopStatusEffect()
	{
		Debug.LogError("GONNA TRY TO REMOVE SPEED UP");

		this.affectedCreature.moveSpeed = originalMoveSpeed;
		Destroy(this/*this.gameObject.GetComponent<SpeedUp>()*/);
	}

	public override void ForceRemoveStatusEffect()
	{
		Destroy(this);
	}
}
