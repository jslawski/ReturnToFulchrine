using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Status Effect Asset", menuName = "Status Effect Asset")]
public class StatusEffectAsset : ScriptableObject 
{
	public bool activateOnEquip;

	public string statusEffectName;

	public int level;
}
