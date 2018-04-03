using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			victim.TakeDamage(controllingCreature.activeWeapon.damageOutput, false, controllingCreature.activeWeapon);
		}
	}
}
