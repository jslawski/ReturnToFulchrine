using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Granted StatusEffect.  Boosts creature's movement speed while equipped.
/// </summary>
public class SpeedUp : EquipStatusEffect {

	private float originalMoveSpeed;
	private float moveSpeedBuff;

	public override void TryApplyStatusEffect(Creature affectedCreature)
	{
		this.ApplyStatusEffect(affectedCreature);
	}

	public override void ApplyStatusEffect(Creature affectedCreature)
	{
		this.affectedCreature = affectedCreature;

		this.originalMoveSpeed = affectedCreature.moveSpeed;
		this.moveSpeedBuff = this.GetMoveSpeedBuff();

		this.ApplySpeedUp();
	}

	public override void UpdateStatusEffect()
	{
		this.moveSpeedBuff = this.GetMoveSpeedBuff();
		this.ApplySpeedUp();
	}

	private float GetMoveSpeedBuff()
	{
		switch (this.level)
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
			Debug.LogError("SpeedUp.GetMoveSpeedBuff: Unknown enchantmentLevel " + this.level + ". Unable to apply StatusEffect.");
			return 0f;
		}
	}

	private void ApplySpeedUp()
	{
		this.affectedCreature.moveSpeed = originalMoveSpeed + (originalMoveSpeed * moveSpeedBuff);
	}

	public override void StopStatusEffect()
	{
		this.affectedCreature.moveSpeed = originalMoveSpeed;
		Destroy(this);
	}
}
