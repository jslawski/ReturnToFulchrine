using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableWeapon : GrabbableEquipment {

	public Weapon weaponDetails;

	private void OnTriggerEnter(Collider other)
	{
		Creature collidingCreature = other.gameObject.GetComponent<Creature>();

		if (collidingCreature != null)
		{
			collidingCreature.EquipWeapon(this.weaponDetails);
			Destroy(this.gameObject);
		}
	}
}
