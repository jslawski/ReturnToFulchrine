using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableArmor : GrabbableEquipment {

	public Armor armorDetails;

	public override void Interact(Creature creature)
	{
		creature.EquipArmor(this.armorDetails);
		base.Interact(creature);
	}
}
