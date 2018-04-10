using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Equipment
{
	public float windUpTime;
	public float windDownTime;

	[HideInInspector]
	public bool continueAttacking = false;

	[HideInInspector]
	public bool currentlyAttacking = false;

	public abstract IEnumerator Attack(Creature wieldingCreature);
}
