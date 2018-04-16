using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enchantment", menuName = "Enchantment")]
public class Enchantment : ScriptableObject {

	public string enchantmentName;

	public StatusEffectAsset weaponStatusEffect;
	public Affinity attackAffinity;
	public StatusEffectAsset armorStatusEffect;
	public Affinity defenseAffinity;
}
