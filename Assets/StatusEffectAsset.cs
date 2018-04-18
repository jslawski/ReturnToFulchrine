using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines a StatusEffect that a piece of equipment is able to impart.  Attach
/// new StatusEffectAssets to Enchantments in order to give that Enchantment the ability to 
/// inflict or give that StatusEffect when attached to a piece of Equipment.
/// </summary>
[CreateAssetMenu(fileName = "New Status Effect Asset", menuName = "Status Effect Asset")]
public class StatusEffectAsset : ScriptableObject 
{
	public bool activateOnEquip;

	public string statusEffectName;

	public int level;
}
