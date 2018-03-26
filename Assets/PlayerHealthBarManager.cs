using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerHealthBarManager {

	private static float totalAliveCharacterHitPoints;
	public static float totalMaxHitPoints;
	public static float currentTotalHitPoints;

	private static Stack<PlayerCharacterType> aliveCharacters;
	private static Stack<PlayerCharacterType> deadCharacters;

	public static void SetupPlayerHealthBarManager(List<PlayerCharacterType> orderedPlayerCharacterList)
	{
		PlayerHealthBarManager.aliveCharacters = new Stack<PlayerCharacterType>();
		PlayerHealthBarManager.deadCharacters = new Stack<PlayerCharacterType>();

		foreach (PlayerCharacterType character in orderedPlayerCharacterList)
		{
			PlayerHealthBarManager.aliveCharacters.Push(character);
			PlayerHealthBarManager.totalAliveCharacterHitPoints += CharacterSelector.characterDictionary[character].hitPoints;
		}

		PlayerHealthBarManager.currentTotalHitPoints = PlayerHealthBarManager.totalAliveCharacterHitPoints;
		PlayerHealthBarManager.totalMaxHitPoints = PlayerHealthBarManager.totalAliveCharacterHitPoints;

		Debug.LogError("Total HP: " + PlayerHealthBarManager.currentTotalHitPoints);
	}

	public static void UpdateHitPoints(float newHitPointTotal)
	{
		bool takenDamage = false;

		if (newHitPointTotal < PlayerHealthBarManager.currentTotalHitPoints)
		{
			takenDamage = true;
		}

		PlayerHealthBarManager.currentTotalHitPoints = newHitPointTotal;

		Debug.LogError("Current HP: " + PlayerHealthBarManager.currentTotalHitPoints);

		if (takenDamage == true)
		{
			PlayerHealthBarManager.AttemptKillCharacter();
		}
		else
		{
			PlayerHealthBarManager.AttemptReviveCharacter();
		}
	}

	private static void AttemptKillCharacter()
	{
		PlayerCharacter topAliveCharacter = CharacterSelector.characterDictionary[PlayerHealthBarManager.aliveCharacters.Peek()];

		//Kill player characters
		while (PlayerHealthBarManager.currentTotalHitPoints <= (PlayerHealthBarManager.totalAliveCharacterHitPoints - topAliveCharacter.hitPoints))
		{
			PlayerCharacterType dyingPlayerCharacterType = PlayerHealthBarManager.aliveCharacters.Pop();
			PlayerHealthBarManager.deadCharacters.Push(dyingPlayerCharacterType);
			topAliveCharacter.isDead = true;
			PlayerHealthBarManager.totalAliveCharacterHitPoints -= topAliveCharacter.hitPoints;

			topAliveCharacter = CharacterSelector.characterDictionary[PlayerHealthBarManager.aliveCharacters.Peek()];
		}
	}

	private static void AttemptReviveCharacter()
	{
		//Revive player character
		while (PlayerHealthBarManager.currentTotalHitPoints > PlayerHealthBarManager.totalAliveCharacterHitPoints && PlayerHealthBarManager.deadCharacters.Count > 0)
		{
			PlayerCharacterType revivingPlayerCharacterType = PlayerHealthBarManager.deadCharacters.Pop();
			PlayerHealthBarManager.aliveCharacters.Push(revivingPlayerCharacterType);
			PlayerCharacter revivingCharacter = CharacterSelector.characterDictionary[revivingPlayerCharacterType];
			revivingCharacter.isDead = false;
			PlayerHealthBarManager.totalAliveCharacterHitPoints += revivingCharacter.hitPoints;
		}
	}
}
