using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCharacterType { Warrior, Mage, Archer, Tank, Current, None };

public class CharacterSelector : MonoBehaviour 
{
	public static Dictionary<PlayerCharacterType, PlayerCharacter> characterDictionary;
	private static List<PlayerCharacterType> characterNameList;
	private static int currentCharacterIndex = 0;

	// Use this for initialization
	private void Awake () 
	{
		CharacterSelector.characterNameList = new List<PlayerCharacterType>() { PlayerCharacterType.Warrior, PlayerCharacterType.Mage, PlayerCharacterType.Archer, PlayerCharacterType.Tank };
		CharacterSelector.characterDictionary = new Dictionary<PlayerCharacterType, PlayerCharacter>();

		Weapon warriorWeapon = new MeleeWeapon(EquipmentClass.Medium, 10f, 0.1f, 0.1f, 0.2f, new Vector2(1f, 5f));
		Weapon mageWeapon = new MeleeWeapon(EquipmentClass.Magic, 3f, 0.1f, 0.05f, 0.3f, new Vector2(5f, 3f));
		Weapon archerWeapon = new RangedWeapon("Projectile", 5f, 0.2f, 0.5f, 20f, 3f);
		Weapon tankWeapon = new MeleeWeapon(EquipmentClass.Heavy, 15f, 0.1f, 0.05f, 0.5f, new Vector2(7f, 1.5f));

		PlayerCharacter warrior = new PlayerCharacter(4.5f, warriorWeapon, new Armor(), 50f, Resources.Load<Material>("CharacterMaterials/Warrior"));
		PlayerCharacter mage = new PlayerCharacter(7f, mageWeapon, new Armor(), 30f, Resources.Load<Material>("CharacterMaterials/Mage"));
		PlayerCharacter archer = new PlayerCharacter(5.5f, archerWeapon, new Armor(), 40f, Resources.Load<Material>("CharacterMaterials/Archer"));
		PlayerCharacter tank = new PlayerCharacter(2f, tankWeapon, new Armor(), 80f, Resources.Load<Material>("CharacterMaterials/Tank"));

		CharacterSelector.characterDictionary.Add(PlayerCharacterType.Warrior, warrior);
		CharacterSelector.characterDictionary.Add(PlayerCharacterType.Mage, mage);
		CharacterSelector.characterDictionary.Add(PlayerCharacterType.Archer, archer);
		CharacterSelector.characterDictionary.Add(PlayerCharacterType.Tank, tank);

		PlayerHealthBarManager.SetupPlayerHealthBarManager(CharacterSelector.characterNameList);
	}

	public static PlayerCharacter GetCharacterRight()
	{
		if (CharacterSelector.AreAllPlayersDead() == true)
		{
			Debug.LogError("GAME OVER!");
			return null;
		}

		PlayerCharacter nextPlayer = null;

		if (CharacterSelector.currentCharacterIndex >= CharacterSelector.characterNameList.Count - 1)
		{
			CharacterSelector.currentCharacterIndex = 0;
			nextPlayer = CharacterSelector.characterDictionary[CharacterSelector.characterNameList[0]];

			if (nextPlayer.isDead == true)
			{
				return CharacterSelector.GetCharacterRight();
			}

			return nextPlayer;
		}

		nextPlayer = CharacterSelector.characterDictionary[CharacterSelector.characterNameList[++CharacterSelector.currentCharacterIndex]];

		if (nextPlayer.isDead == true)
		{
			return CharacterSelector.GetCharacterRight();
		}

		return nextPlayer;
	}

	public static PlayerCharacter GetCharacterLeft()
	{
		if (CharacterSelector.AreAllPlayersDead() == true)
		{
			Debug.LogError("GAME OVER!");
			return null;
		}

		PlayerCharacter nextPlayer = null;

		if (CharacterSelector.currentCharacterIndex == 0)
		{
			CharacterSelector.currentCharacterIndex = CharacterSelector.characterNameList.Count - 1;
			nextPlayer = CharacterSelector.characterDictionary[CharacterSelector.characterNameList[CharacterSelector.characterNameList.Count - 1]];

			if (nextPlayer.isDead == true)
			{
				return CharacterSelector.GetCharacterLeft();
			}

			return nextPlayer;
		}

		nextPlayer = CharacterSelector.characterDictionary[CharacterSelector.characterNameList[--CharacterSelector.currentCharacterIndex]];

		if (nextPlayer.isDead == true)
		{
			return CharacterSelector.GetCharacterLeft();
		}

		return nextPlayer;
	}

	public static PlayerCharacter GetCharacterAtIndex(int index)
	{
		if (index < 0 || index >= CharacterSelector.characterNameList.Count)
		{
			Debug.LogError("CharacterSelector.GetCharacterAtIndex: Index out of bounds. Unable to get character at index: " + index);
			return CharacterSelector.characterDictionary[CharacterSelector.characterNameList[CharacterSelector.currentCharacterIndex]];
		}

		return CharacterSelector.characterDictionary[CharacterSelector.characterNameList[index]];
	}

	public static bool AreAllPlayersDead()
	{
		foreach (KeyValuePair<PlayerCharacterType, PlayerCharacter> character in CharacterSelector.characterDictionary)
		{
			if (character.Value.isDead == false)
			{
				return false;
			}
		}

		return true;
	}
}
