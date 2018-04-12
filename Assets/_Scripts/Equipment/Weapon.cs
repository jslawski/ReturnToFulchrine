using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Equipment
{
	public int windUpFrames;
	public int windDownFrames;

	[HideInInspector]
	public bool continueAttacking = false;

	[HideInInspector]
	public bool currentlyAttacking = false;

	public abstract IEnumerator Attack(Creature wieldingCreature);
}
