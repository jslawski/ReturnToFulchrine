using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class FileModificationWarning : MonoBehaviour
{
	[MenuItem("Equipment Organizer/Organize Equipment")]
	private static void OrganizeEquipment()
	{
		Equipment[] allEquipment = Resources.LoadAll<Equipment>(ScriptableObjectPaths.EquipmentDirectoryName);
	
		foreach (Equipment currentEquipment in allEquipment)
		{
			string oldPath = AssetDatabase.GetAssetPath(currentEquipment.GetInstanceID());
			string destinationPath = "Assets/Resources/" + ScriptableObjectPaths.EquipmentDirectoryName;

			switch (currentEquipment.equipmentType)
			{
			case EquipmentType.Weapon:
				destinationPath += ScriptableObjectPaths.WeaponsDirectoryName;
				break;
			case EquipmentType.Armor:
				destinationPath += ScriptableObjectPaths.ArmorDirectoryName;
				break;
			case EquipmentType.None:
				continue;
			}

			switch (currentEquipment.equippableCreatureType)
			{
			case CreatureType.Magic:
				destinationPath += ScriptableObjectPaths.MagicEquipmentDirectoryName;
				break;
			case CreatureType.Light:
				destinationPath += ScriptableObjectPaths.LightEquipmentDirectoryName;
				break;
			case CreatureType.Medium:
				destinationPath += ScriptableObjectPaths.MediumEquipmentDirectoryName;
				break;
			case CreatureType.Heavy:
				destinationPath += ScriptableObjectPaths.HeavyEquipmentDirectoryName;
				break;
			case CreatureType.None:
				continue;
			}
				
			//Do rarity here
			switch (currentEquipment.rarity)
			{
			case Rarity.Common:
				destinationPath += ScriptableObjectPaths.CommonEquipmentDirectoryName;
				break;
			case Rarity.Uncommon:
				destinationPath += ScriptableObjectPaths.UncommonEquipmentDirectoryName;
				break;
			case Rarity.Rare:
				destinationPath += ScriptableObjectPaths.RareEquipmentDirectoryName;
				break;
			case Rarity.Legendary:
				destinationPath += ScriptableObjectPaths.LegendaryEquipmentDirectoryName;
				break;
			default:
				continue;
			}

			destinationPath += currentEquipment.name + ".asset";

			AssetDatabase.MoveAsset(oldPath, destinationPath);
		}

		Debug.Log("All equipment is now in their proper directory based on EquipmentType, CreatureType, and Rarity!");
	}
}
