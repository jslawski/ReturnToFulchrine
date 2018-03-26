using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature 
{
	public void Start()
	{
		this.activeArmor = new Armor(EquipmentClass.Light, 2f);
		this.activeWeapon = new MeleeWeapon();
	}

	public override void DisableMovement(){}
	public override void EnableMovement(){}
	public override void LockToRotation(){}
	public override void Attack(){}
}
