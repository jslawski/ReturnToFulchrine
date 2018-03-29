using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeleeWeapon", menuName = "Equipment/Weapon/MeleeWeapon")]
public class MeleeWeapon : Weapon
{
	//Melee Weapon Exclusives
	public float swingTime = 1f;
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

		yield return new WaitForSeconds(this.windUpTime);

		wieldingCreature.attackZone.SetActive(true);

		yield return new WaitForSeconds(this.swingTime);

		wieldingCreature.attackZone.SetActive(false);

		yield return new WaitForSeconds(this.windDownTime);

		wieldingCreature.EnableMovement();
	}
}
