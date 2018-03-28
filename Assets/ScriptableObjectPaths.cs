using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptableObjectPaths {
	private const string MagicEquipmentType = "Magic/";
	private const string LightEquipmentType = "Light/";
	private const string MediumEquipmentType = "Medium/";
	private const string HeavyEquipmentType = "Heavy/";

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
