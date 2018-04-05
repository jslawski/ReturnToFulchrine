using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature 
{
	public void Start()
	{
		this.activeArmor = Resources.Load<Armor>(ScriptableObjectPaths.LightArmorPath + ScriptableObjectPaths.UncommonEquipmentDirectoryName + "Leather");
		this.activeWeapon = Resources.Load<Weapon>(ScriptableObjectPaths.WeaponsPath + "Unarmed");
	}

	public override void DisableMovement(){}
	public override void EnableMovement(){}
	public override void LockToRotation(){}
	public override void Attack(){}
}
