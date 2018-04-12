using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeleeWeapon", menuName = "Equipment/Weapon/MeleeWeapon")]
public class MeleeWeapon : Weapon
{
	//Melee Weapon Exclusives
	public int swingFrames = 1;
	public Vector2 attackZoneDimensions;

	public void PositionWeapon(Creature wieldingCreature)
	{
		float yPosition = (wieldingCreature.attackStartPointY) + (this.attackZoneDimensions.y / 2.0f);

		wieldingCreature.attackZone.transform.localPosition = new Vector3(0, yPosition, 0);
		wieldingCreature.attackZone.transform.localScale = new Vector3(this.attackZoneDimensions.x, this.attackZoneDimensions.y, 1f);
		wieldingCreature.attackZone.transform.rotation = wieldingCreature.gameObject.transform.rotation;
	}
		
	public override IEnumerator Attack(Creature wieldingCreature)
	{
		this.PositionWeapon(wieldingCreature);
		wieldingCreature.DisableMovement();

		int currentNumFrames = this.windUpFrames;
		while (currentNumFrames > 0)
		{
			currentNumFrames--;
			yield return new WaitForFixedUpdate();
		}

		wieldingCreature.attackZone.SetActive(true);

		currentNumFrames = this.swingFrames;
		while (currentNumFrames > 0)
		{
			currentNumFrames--;
			yield return new WaitForFixedUpdate();
		}

		wieldingCreature.attackZone.SetActive(false);

		currentNumFrames = this.windDownFrames;
		while (currentNumFrames > 0)
		{
			currentNumFrames--;
			yield return new WaitForFixedUpdate();
		}

		wieldingCreature.EnableMovement();
	}
}
