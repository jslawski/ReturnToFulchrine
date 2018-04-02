﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageableObject {

	float hitPoints { get; set; }

	void TakeDamage(float damageDealt, bool rawDamage = false/*, Enchantments here too*/);

	void GainHealth(float healthRestored);
}
