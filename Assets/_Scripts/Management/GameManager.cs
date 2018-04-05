using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public const int EnemyLayerNum = 10;

	private void Awake()
	{
		GameManager.instance = this;

		//Initalize Characters for the session
		//Warrior
		PlayerCharacter warrior = Resources.Load<PlayerCharacter>(ScriptableObjectPaths.PlayerCharacterPath +  "Warrior");
		warrior.weapon = Resources.Load<Weapon>(ScriptableObjectPaths.MediumWeaponsPath + ScriptableObjectPaths.CommonEquipmentDirectoryName + "Sword");
		warrior.armor = Resources.Load<Armor>(ScriptableObjectPaths.MediumArmorPath + ScriptableObjectPaths.RareEquipmentDirectoryName + "StuddedMail");
		warrior.isDead = false;
		warrior.activeInflictedStatusEffects = new List<InflictedStatusEffect>();
		//Mage
		PlayerCharacter mage = Resources.Load<PlayerCharacter>(ScriptableObjectPaths.PlayerCharacterPath + "Mage");
		mage.weapon = Resources.Load<Weapon>(ScriptableObjectPaths.MagicWeaponsPath + ScriptableObjectPaths.CommonEquipmentDirectoryName + "Staff");
		mage.armor = Resources.Load<Armor>(ScriptableObjectPaths.MagicArmorPath + ScriptableObjectPaths.UncommonEquipmentDirectoryName + "Cloak");
		mage.isDead = false;
		mage.activeInflictedStatusEffects = new List<InflictedStatusEffect>();
		//Archer
		PlayerCharacter archer = Resources.Load<PlayerCharacter>(ScriptableObjectPaths.PlayerCharacterPath + "Archer");
		archer.weapon = Resources.Load<Weapon>(ScriptableObjectPaths.LightWeaponsPath + ScriptableObjectPaths.CommonEquipmentDirectoryName + "Bow");
		archer.armor = Resources.Load<Armor>(ScriptableObjectPaths.LightArmorPath + ScriptableObjectPaths.UncommonEquipmentDirectoryName + "Leather");
		archer.isDead = false;
		archer.activeInflictedStatusEffects = new List<InflictedStatusEffect>();
		//Tank
		PlayerCharacter tank = Resources.Load<PlayerCharacter>(ScriptableObjectPaths.PlayerCharacterPath + "Tank");
		tank.weapon = Resources.Load<Weapon>(ScriptableObjectPaths.HeavyWeaponsPath + ScriptableObjectPaths.CommonEquipmentDirectoryName + "Axe");
		tank.armor = Resources.Load<Armor>(ScriptableObjectPaths.HeavyArmorPath + ScriptableObjectPaths.CommonEquipmentDirectoryName + "ChainMail");
		tank.isDead = false;
		tank.activeInflictedStatusEffects = new List<InflictedStatusEffect>();
	}

	// Use this for initialization
	void Start () {
		CameraFollow.instance.SetPointOfInterest(GameObject.Find("Player"));
	}

	public static Dictionary<string, int> GetEnchantmentLevelsOnEquipment(Equipment equipment)
	{
		Dictionary<string, int> levelDictionary = new Dictionary<string, int>();

		foreach (Enchantment enchantment in equipment.enchantments)
		{
			if (levelDictionary.ContainsKey(enchantment.enchantmentName) == true)
			{
				levelDictionary[enchantment.enchantmentName] += 1;
			}
			else
			{
				levelDictionary.Add(enchantment.enchantmentName, 1);
			}
		}

		return levelDictionary;
	}

	public static void GenerateGrabbableWeapon(Vector3 instantiationPosition, Weapon weaponDetails)
	{
		GameObject grabbableWeaponPrefab = Resources.Load<GameObject>("GrabbableWeapon");

		GameObject instance = GameObject.Instantiate(grabbableWeaponPrefab, instantiationPosition, new Quaternion()) as GameObject;
		GrabbableWeapon grabbableWeaponComponent = instance.GetComponent<GrabbableWeapon>();
		grabbableWeaponComponent.weaponDetails = weaponDetails;

		grabbableWeaponComponent.UpdateEquipmentColor(weaponDetails);

		grabbableWeaponComponent.StartCoroutine("LaunchGrabbableEquipment");
	}

	public static void GenerateGrabbableArmor(Vector3 instantiationPosition, Armor armorDetails)
	{
		GameObject grabbableArmorPrefab = Resources.Load<GameObject>("GrabbableArmor");

		GameObject instance = GameObject.Instantiate(grabbableArmorPrefab, instantiationPosition, new Quaternion()) as GameObject;
		GrabbableArmor grabbableArmorComponent = instance.GetComponent<GrabbableArmor>();
		grabbableArmorComponent.armorDetails = armorDetails;

		grabbableArmorComponent.UpdateEquipmentColor(armorDetails);

		grabbableArmorComponent.StartCoroutine("LaunchGrabbableEquipment");
	}
}
