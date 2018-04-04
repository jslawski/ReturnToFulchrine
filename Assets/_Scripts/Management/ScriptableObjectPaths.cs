using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptableObjectPaths {
	
	public const string EquipmentDirectoryName = "Equipment/";
	public const string WeaponsDirectoryName = "Weapons/";
	public const string ArmorDirectoryName = "Armor/";
	public const string MagicEquipmentDirectoryName = "Magic/";
	public const string LightEquipmentDirectoryName = "Light/";
	public const string MediumEquipmentDirectoryName = "Medium/";
	public const string HeavyEquipmentDirectoryName = "Heavy/";
	public const string CommonEquipmentDirectoryName = "Common/";
	public const string UncommonEquipmentDirectoryName = "Uncommon/";
	public const string RareEquipmentDirectoryName = "Rare/";
	public const string LegendaryEquipmentDirectoryName = "Legendary/";

	public const string WeaponsPath = EquipmentDirectoryName + WeaponsDirectoryName;
	public const string MagicWeaponsPath = WeaponsPath + MagicEquipmentDirectoryName;
	public const string LightWeaponsPath = WeaponsPath + LightEquipmentDirectoryName;
	public const string MediumWeaponsPath = WeaponsPath + MediumEquipmentDirectoryName;
	public const string HeavyWeaponsPath = WeaponsPath + HeavyEquipmentDirectoryName;
	public const string ArmorPath = EquipmentDirectoryName + ArmorDirectoryName;
	public const string MagicArmorPath = ArmorPath + MagicEquipmentDirectoryName;
	public const string LightArmorPath = ArmorPath + LightEquipmentDirectoryName;
	public const string MediumArmorPath = ArmorPath + MediumEquipmentDirectoryName;
	public const string HeavyArmorPath = ArmorPath + HeavyEquipmentDirectoryName;

	public const string PlayerCharacterPath = "PlayerCharacters/";
}
