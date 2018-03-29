using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageableObject {

	float hitPoints { get; set; }

	void TakeDamage(float damageDealt /*, Enchantments here too*/);

	void GainHealth(float healthRestored);
}
