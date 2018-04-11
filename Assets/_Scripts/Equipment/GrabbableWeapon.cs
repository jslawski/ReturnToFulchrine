using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableWeapon : GrabbableEquipment {

	public Weapon weaponDetails;

	public override void Interact(Creature creature)
	{
		creature.EquipWeapon(this.weaponDetails);
		base.Interact(creature);
	}
}
