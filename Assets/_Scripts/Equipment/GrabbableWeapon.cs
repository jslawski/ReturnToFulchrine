using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableWeapon : GrabbableEquipment {

	public Weapon weaponDetails;

	private MeshRenderer meshRenderer;

	public override void Interact(Creature creature)
	{
		creature.EquipWeapon(this.weaponDetails);
		base.Interact(creature);
	}
}
