﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AffinityType { Fire, Ice, Electric, None };

/// <summary>
/// Affinities define an equipment's elemental "type."  Each Affinity has strengths and weaknesses when defending against other Affinity types.
/// A weapon with an Affinity will deal additional damage in the type of that Affinity.
/// A piece of armor with an Affinity will potentially contribute to a multiplier to increase or decrease the amount of affinity damage taken.
/// 
/// Attach Affinities to Enchantments in order to give that Enchantment a type that it can impart on a piece of Equipment.
/// </summary>
[CreateAssetMenu(fileName = "New Affinity", menuName = "Affinity")]
public class Affinity : ScriptableObject {

	public AffinityType type;

	public int level;

	public List<AffinityType> defenseAdvantageAffinities;

	public List<AffinityType> defenseDisadvantageAffinities;

	public float GetAffinityDamage(int affinityLevel)
	{
		switch (affinityLevel)
		{
		case 1:
			return Mathf.Ceil(Random.Range(1f, 6f));
		case 2:
			return Mathf.Ceil(Random.Range(1f, 10f));
		case 3:
			return Mathf.Ceil(Random.Range(5f, 12f));
		case 4:
			return Mathf.Ceil(Random.Range(6f, 14f));
		case 5:
			return Mathf.Ceil(Random.Range(10f, 14f));
		default:
			Debug.LogError("Affinity.GetAffinityDamage: Unknown affinityLevel: " + affinityLevel + ". Unable to add additional affinity damage.");
			return 0;
		}
	}
}
