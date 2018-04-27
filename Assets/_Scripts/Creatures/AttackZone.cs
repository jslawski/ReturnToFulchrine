using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolarCoordinates;

public class AttackZone : MonoBehaviour, IDamageDealer {

	[SerializeField]
	private Creature controllingCreature;

	#region IDamageDealer implementation
	private float _damageOutput = 0f;

	public float damageOutput
	{
		get { return this._damageOutput; }
		set { this._damageOutput = value; }
	}
	#endregion

	private void OnTriggerEnter(Collider other)
	{
		IDamageableObject victim = other.gameObject.GetComponent<IDamageableObject>();

		if (victim != null)
		{
			float damageOutput = this.controllingCreature.activeWeapon.GetRandomRangeValue();

			victim.TakeDamage(damageOutput, false, this.controllingCreature.activeWeapon);
			this.AttemptKnockback(victim);
		}
	}

	private Vector3 GetKnockbackDirection()
	{
		PolarCoordinate polarDirection = new PolarCoordinate(1, this.gameObject.transform.eulerAngles.z * Mathf.Deg2Rad);

		Debug.LogError("Polar Coordinate: " + polarDirection.ToString());
		Debug.LogError("KnockbackDirection: " + polarDirection.PolarToCartesian().ToString());

		return polarDirection.PolarToCartesian();
	}

	private void AttemptKnockback(IDamageableObject victim)
	{
		float knockbackRoll = Random.Range(0f, 1f);

		if (knockbackRoll < this.controllingCreature.activeWeapon.knockbackChance)
		{
			Vector3 knockbackDirection = this.controllingCreature.transform.up;

			victim.Knockback(knockbackDirection, this.controllingCreature.activeWeapon.knockbackDistance);
		}
	}
}
