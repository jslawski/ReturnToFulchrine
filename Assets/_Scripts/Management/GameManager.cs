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
		warrior.weapon = Resources.Load<Weapon>(ScriptableObjectPaths.MediumWeaponsPath + "Sword");
		warrior.armor = Resources.Load<Armor>(ScriptableObjectPaths.MediumArmorPath + "StuddedMail");
		warrior.isDead = false;
		warrior.activeStatusEffects = new List<StatusEffect>();
		//Mage
		PlayerCharacter mage = Resources.Load<PlayerCharacter>(ScriptableObjectPaths.PlayerCharacterPath + "Mage");
		mage.weapon = Resources.Load<Weapon>(ScriptableObjectPaths.MagicWeaponsPath + "Staff");
		mage.armor = Resources.Load<Armor>(ScriptableObjectPaths.MagicArmorPath + "Cloak");
		mage.isDead = false;
		mage.activeStatusEffects = new List<StatusEffect>();
		//Archer
		PlayerCharacter archer = Resources.Load<PlayerCharacter>(ScriptableObjectPaths.PlayerCharacterPath + "Archer");
		archer.weapon = Resources.Load<Weapon>(ScriptableObjectPaths.LightWeaponsPath + "Bow");
		archer.armor = Resources.Load<Armor>(ScriptableObjectPaths.LightArmorPath + "Leather");
		archer.isDead = false;
		archer.activeStatusEffects = new List<StatusEffect>();
		//Tank
		PlayerCharacter tank = Resources.Load<PlayerCharacter>(ScriptableObjectPaths.PlayerCharacterPath + "Tank");
		tank.weapon = Resources.Load<Weapon>(ScriptableObjectPaths.HeavyWeaponsPath + "Axe");
		tank.armor = Resources.Load<Armor>(ScriptableObjectPaths.HeavyArmorPath + "ChainMail");
		tank.isDead = false;
		tank.activeStatusEffects = new List<StatusEffect>();
	}

	// Use this for initialization
	void Start () {
		CameraFollow.instance.SetPointOfInterest(GameObject.Find("Player"));
	}

	public static Dictionary<string, int> GetEnchantmentLevelsOnEquipment(Equipment equipment)
	{
		Dictionary<int, string> levelDictionary = new Dictionary<int, string>();

		foreach (Enchantment enchantment in equipment.enchantments)
		{
			levelDictionary[enchantment.enchantmentName] += 1;
		}
	}
}
