using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageableObject {

	float hitPoints { get; set; }

	void TakeDamage(float damageDealt, bool rawDamage = false, Weapon weapon = null);

	void ApplyStatusEffects(Weapon weapon);

	void GainHealth(float healthRestored);
}
