using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RangedWeapon", menuName = "Equipment/Weapon/RangedWeapon")]
public class RangedWeapon : Weapon
{
	//Ranged Weapon Exclusives
	[SerializeField]
	private GameObject rangedProjectilePrefab;
	[SerializeField]
	private float projectileVelocity = 5f;
	[SerializeField]
	private float projectileLifetime = 3f;

	//AimAssist parameters
	public bool useAimAssist = true;
	[SerializeField]
	private float aimAssistSphereRadius = 1f;
	[SerializeField]
	private float aimAssistMaxDistance = 15f;

	public override IEnumerator Attack(Creature wieldingCreature)
	{
		Debug.Log("Currently Attacking? " + this.currentlyAttacking);

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

			GameObject currentProjectileObject = GameObject.Instantiate(this.rangedProjectilePrefab, wieldingCreature.transform.position, new Quaternion()) as GameObject;
			RangedProjectile currentProjectile = currentProjectileObject.GetComponent<RangedProjectile>();

			Vector3 launchDirection = wieldingCreature.transform.up;

			if (this.useAimAssist == true)
			{
				launchDirection = this.GetAimAssistDirection(wieldingCreature);
			}

			currentProjectile.Launch(launchDirection, this.projectileVelocity, this.damageOutput, this.projectileLifetime);

			yield return new WaitForSeconds(this.windDownTime);
		}

		this.currentlyAttacking = false;
		wieldingCreature.EnableMovement();
	}
		
	private bool IsCloserInDirection(Vector3 originDirection, Vector3 initialTarget, Vector3 comparatorTarget)
	{
		Vector3 initialTargetDirection = (initialTarget - originDirection).normalized;
		Vector3 comparatorTargetDirection = (comparatorTarget - originDirection).normalized;

		//Calculate angle between player direction and potential targets
		float initialAngleDifference = Mathf.Acos(Vector3.Dot(originDirection, initialTargetDirection));
		float comparatorAngleDifference = Mathf.Acos(Vector3.Dot(originDirection, comparatorTargetDirection));

		return comparatorAngleDifference < initialAngleDifference;
	}

	//Closest Target is defined as the target with the smallest angle of difference between the player's current aiming direction
	private RaycastHit GetClosestTarget(Vector3 originDirection, RaycastHit[] potentialTargets, int currentClosestTargetIndex, int nextIndex)
	{
		if (nextIndex >= potentialTargets.Length)
		{
			return potentialTargets[currentClosestTargetIndex];
		}

		if (this.IsCloserInDirection(originDirection, potentialTargets[currentClosestTargetIndex].transform.position, potentialTargets[nextIndex].transform.position) == true)
		{
			return GetClosestTarget(originDirection, potentialTargets, nextIndex, ++nextIndex);
		}
		else
		{
			return GetClosestTarget(originDirection, potentialTargets, currentClosestTargetIndex, ++nextIndex);
		}
	}

	private Vector3 GetAimAssistDirection(Creature wieldingCreature)
	{
		int layerMask = 1 << GameManager.EnemyLayerNum;

		RaycastHit[] potentialTargets = Physics.SphereCastAll(wieldingCreature.transform.position + (wieldingCreature.transform.up.normalized * this.aimAssistSphereRadius), this.aimAssistSphereRadius,
			                                wieldingCreature.transform.up, this.aimAssistMaxDistance, layerMask);

		if (potentialTargets.Length > 0)
		{
			RaycastHit estimatedTarget = this.GetClosestTarget(wieldingCreature.transform.up, potentialTargets, 0, 1);

			Vector3 AssistedAimVector = (estimatedTarget.transform.position - wieldingCreature.transform.position).normalized;

			return new Vector3(AssistedAimVector.x, AssistedAimVector.y, wieldingCreature.transform.up.z);
		}

		return wieldingCreature.transform.up;
	}
}
