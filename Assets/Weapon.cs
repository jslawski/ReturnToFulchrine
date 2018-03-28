using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Equipment
{
	public float damageOutput;

	public float windUpTime;
	public float windDownTime;

	[HideInInspector]
	public bool continueAttacking = false;

	[HideInInspector]
	public bool currentlyAttacking = false;

	//public Enchantments of some sort

	public abstract IEnumerator Attack(Creature wieldingCreature);
}
