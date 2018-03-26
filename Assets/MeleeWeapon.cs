using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
	//Melee Weapon Exclusives
	public float swingTime = 1f;
	public Vector2 attackZoneDimensions;

	public MeleeWeapon(EquipmentClass weaponClass, float damageOutput, float windUpTime, float windDownTime, float swingTime, Vector3 attackZoneDimensions)
	{
		this.equipmentClass = weaponClass;
		this.damageOutput = damageOutput;
		this.windUpTime = windUpTime;
		this.windDownTime = windDownTime;
		this.swingTime = swingTime;
		this.attackZoneDimensions = attackZoneDimensions;
	}

	public MeleeWeapon()
	{
		this.equipmentClass = EquipmentClass.None;
		this.damageOutput = 1f;
		this.windUpTime = 0.3f;
		this.windDownTime = 0.1f;
		this.attackZoneDimensions = new Vector2(1f, 1f);
	}

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
