using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// An Enchantment is a class that grants a piece of Equipment access to various StatusEffects and Affinities
/// when it is equipped to that Equipment's ScriptableObject instance
/// </summary>
[CreateAssetMenu(fileName = "New Enchantment", menuName = "Enchantment")]
public class Enchantment : ScriptableObject {

	public string enchantmentName;

	public StatusEffectAsset weaponStatusEffect;
	public Affinity attackAffinity;
	public StatusEffectAsset armorStatusEffect;
	public Affinity defenseAffinity;
}
