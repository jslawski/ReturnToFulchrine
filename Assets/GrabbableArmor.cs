using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableArmor : GrabbableEquipment {

	public Armor armorDetails;

	private void OnTriggerEnter(Collider other)
	{
		Creature collidingCreature = other.gameObject.GetComponent<Creature>();

		if (collidingCreature != null)
		{
			collidingCreature.EquipArmor(this.armorDetails);
			Destroy(this.gameObject);
		}
	}
}
