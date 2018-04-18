using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature 
{
	public void Start()
	{
		this.SetActiveArmor(Resources.Load<Armor>(ScriptableObjectPaths.LightArmorPath + ScriptableObjectPaths.UncommonEquipmentDirectoryName + "Leather"));
		this.SetActiveWeapon(Resources.Load<Weapon>(ScriptableObjectPaths.WeaponsPath + "Unarmed"));
		this.hitPoints = this.totalHitPoints;
	}

	public override void DisableMovement(){}
	public override void EnableMovement(){}
	public override void LockToRotation(){}
	public override void Attack(){}
}
