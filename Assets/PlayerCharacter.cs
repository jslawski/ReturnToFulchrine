using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all aspects of unique traits between selectable player characters:
/// Movement speed, equipped weapons, enchantments, etc. 
/// Should not be instantiated, but rather referenced by the Player class. 
/// </summary>
[CreateAssetMenu(fileName = "New Player Character", menuName = "PlayerCharacter")]
public class PlayerCharacter : ScriptableObject
{
	public PlayerCharacterType type;

	public float moveSpeed = 5f;

	public Material characterMaterial;

	public Weapon weapon;

	public Armor armor;

	public float hitPoints;

	public bool isDead = false;

	public PlayerCharacter(float moveSpeed, Weapon weapon, Armor armor, float hitPoints, Material characterMaterial, bool isDead = false)
	{
		this.moveSpeed = moveSpeed;
		this.weapon = weapon;
		this.armor = armor;
		this.hitPoints = hitPoints;
		this.characterMaterial = characterMaterial;
		this.isDead = isDead;
	}
}
