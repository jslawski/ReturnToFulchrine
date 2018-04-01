using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatusEffectManager {

	public static StatusEffect GetStatusEffectFromName(string statusEffectName)
	{
		return new SpeedUp();
	}
}
