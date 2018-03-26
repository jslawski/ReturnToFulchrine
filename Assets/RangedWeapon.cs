using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
	//Ranged Weapon Exclusives
	[SerializeField]
	private string rangedProjectilePrefabName;
	private float projectileVelocity = 5f;
	private float projectileLifetime = 3f;
	private bool currentlyAttacking = false;

	public RangedWeapon(string prefabName, float damageOutput, float windUpTime, float windDownTime, float projectileVelocity, float projectileLifetime)
	{
		this.rangedProjectilePrefabName = prefabName;
		this.damageOutput = damageOutput;
		this.projectileVelocity = projectileVelocity;
		this.projectileLifetime = projectileLifetime;
		this.equipmentClass = EquipmentClass.Light;
		this.windUpTime = windUpTime;
		this.windDownTime = windDownTime;
	}
		
	public override IEnumerator Attack(Creature wieldingCreature)
	{
		if (this.currentlyAttacking == true)
		{
			yield break;
		}

		this.currentlyAttacking = true;
		wieldingCreature.LockToRotation();

		//Repeatedly fire until player lets go of the attack button
		while (this.continueAttacking == true)
		{
			yield return new WaitForSeconds(this.windUpTime);

			GameObject projectilePrefab = Resources.Load<GameObject>(this.rangedProjectilePrefabName);

			GameObject currentProjectileObject = GameObject.Instantiate(projectilePrefab, wieldingCreature.transform.position, new Quaternion()) as GameObject;
			RangedProjectile currentProjectile = currentProjectileObject.GetComponent<RangedProjectile>();
			currentProjectile.Launch(wieldingCreature.transform.up, this.projectileVelocity, this.damageOutput, this.projectileLifetime);

			yield return new WaitForSeconds(this.windDownTime);
		}

		this.currentlyAttacking = false;
		wieldingCreature.EnableMovement();
	}
}
