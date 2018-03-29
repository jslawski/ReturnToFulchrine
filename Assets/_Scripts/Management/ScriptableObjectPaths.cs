using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptableObjectPaths {
	public const string MagicEquipmentType = "Magic/";
	public const string LightEquipmentType = "Light/";
	public const string MediumEquipmentType = "Medium/";
	public const string HeavyEquipmentType = "Heavy/";

	public const string EquipmentPath = "Equipment/";
	public const string WeaponsPath = EquipmentPath + "Weapons/";
	public const string MagicWeaponsPath = WeaponsPath + MagicEquipmentType;
	public const string LightWeaponsPath = WeaponsPath + LightEquipmentType;
	public const string MediumWeaponsPath = WeaponsPath + MediumEquipmentType;
	public const string HeavyWeaponsPath = WeaponsPath + HeavyEquipmentType;
	public const string ArmorPath = EquipmentPath + "Armor/";
	public const string MagicArmorPath = ArmorPath + MagicEquipmentType;
	public const string LightArmorPath = ArmorPath + LightEquipmentType;
	public const string MediumArmorPath = ArmorPath + MediumEquipmentType;
	public const string HeavyArmorPath = ArmorPath + HeavyEquipmentType;

	public const string PlayerCharacterPath = "PlayerCharacters/";
}
