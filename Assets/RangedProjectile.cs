using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour, IDamageDealer 
{
	private float lifetime;

	#region IDamageDealer
	private float _damageOutput = 0f;

	public float damageOutput
	{
		get { return this._damageOutput; }
		set { this._damageOutput = value; }
	}
	#endregion

	public void Launch(Vector2 direction, float velocity, float damage, float lifetime)
	{
		this.damageOutput = damage;
		this.lifetime = lifetime;
		StartCoroutine(MoveProjectile(direction, velocity));
	}

	private IEnumerator MoveProjectile(Vector2 direction, float velocity)
	{
		float currentLifetime = 0;

		while (currentLifetime < this.lifetime)
		{
			this.transform.Translate(direction * velocity * Time.deltaTime);
			currentLifetime += Time.deltaTime;
			yield return null;
		}

		Destroy(this.gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		IDamageableObject victim = other.gameObject.GetComponent<IDamageableObject>();

		if (victim != null)
		{
			victim.TakeDamage(this.damageOutput);
			Destroy(this.gameObject);
		}
	}
}
