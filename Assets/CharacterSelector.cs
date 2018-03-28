using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCharacterType { Warrior, Mage, Archer, Tank, Current, None };

public class CharacterSelector : MonoBehaviour 
{
	private static List<PlayerCharacterType> characterNameList;
	private static int currentCharacterIndex = 0;

	// Use this for initialization
	private void Awake () 
	{
		CharacterSelector.characterNameList = new List<PlayerCharacterType>() { PlayerCharacterType.Warrior, PlayerCharacterType.Mage, PlayerCharacterType.Archer, PlayerCharacterType.Tank };

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
			nextPlayer = CharacterSelector.GetCharacterByType(CharacterSelector.characterNameList[0]);

			if (nextPlayer.isDead == true)
			{
				return CharacterSelector.GetCharacterRight();
			}

			return nextPlayer;
		}

		nextPlayer = CharacterSelector.GetCharacterByType(CharacterSelector.characterNameList[++CharacterSelector.currentCharacterIndex]);

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
			nextPlayer = CharacterSelector.GetCharacterByType(CharacterSelector.characterNameList[CharacterSelector.characterNameList.Count - 1]);

			if (nextPlayer.isDead == true)
			{
				return CharacterSelector.GetCharacterLeft();
			}

			return nextPlayer;
		}

		nextPlayer = CharacterSelector.GetCharacterByType(CharacterSelector.characterNameList[--CharacterSelector.currentCharacterIndex]);

		if (nextPlayer.isDead == true)
		{
			return CharacterSelector.GetCharacterLeft();
		}

		return nextPlayer;
	}

	public static PlayerCharacter GetCharacterByType(PlayerCharacterType type)
	{
		return Resources.Load<PlayerCharacter>(ScriptableObjectPaths.PlayerCharacterPath + type.ToString());
	}

	public static bool AreAllPlayersDead()
	{
		PlayerCharacter[] allPlayerCharacters = Resources.LoadAll<PlayerCharacter>(ScriptableObjectPaths.PlayerCharacterPath);

		foreach (PlayerCharacter character in allPlayerCharacters)
		{
			if (character.isDead == false)
			{
				return false;
			}
		}

		return true;
	}
}
