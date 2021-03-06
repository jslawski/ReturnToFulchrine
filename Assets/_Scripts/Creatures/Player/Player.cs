﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using System;

/// <summary>
/// Handles all aspects of the player controlled avatar that is universal across all playable PlayerCharacters:
/// Movement, collision, HP, SP, etc.
/// </summary>
public class Player : Creature 
{
	private InputDevice device;

	public PlayerControls currentControls;

	private PlayerCharacter currentCharacter;

	[SerializeField]
	private MeshRenderer meshRenderer;

	private static Dictionary<string, PlayerControls> playerControlsDict;
	public const string DefaultPlayerControlsKey = "default";
	public const string DisablePlayerControlsKey = "disable";
	public const string LockRotationPlayerControlsKey = "lockRotation";

	#region IDamageableObject implementation
	public override float hitPoints
	{
		get { return PlayerHealthBarManager.currentTotalHitPoints; }
		set 
		{ 
			PlayerHealthBarManager.UpdateHitPoints(value);
			if (this.currentCharacter.isDead)
			{
				this.SwapCharacter(CharacterSelector.GetCharacterRight());
			}
		}
	}

	public override void ApplyStatusEffects(Weapon weapon)
	{
		foreach (AggregateStatusEffectMetaData data in weapon.statusEffectMetaData)
		{
			if (data.statusEffect.activateOnEquip == false)
			{
				InflictedStatusEffect currentStatusEffect = this.gameObject.GetComponent(Type.GetType(data.statusEffect.statusEffectName)) as InflictedStatusEffect;

				if (currentStatusEffect == null)
				{
					currentStatusEffect = this.gameObject.AddComponent(Type.GetType(data.statusEffect.statusEffectName)) as InflictedStatusEffect;
					currentStatusEffect.level = data.aggregateLevel;
					this.currentCharacter.activeInflictedStatusEffects.Add(currentStatusEffect);
					currentStatusEffect.onInflicedStatusEffectEnded += this.currentCharacter.RemoveInflictedStatusEffect;
				}
				else
				{
					currentStatusEffect.level = data.aggregateLevel;
					currentStatusEffect.AttemptStatusEffectRefresh(this);
				}
			}
		}
	}

	public override void GainHealth(float healthRestored)
	{
		if (this.hitPoints + healthRestored > PlayerHealthBarManager.totalMaxHitPoints)
		{
			this.hitPoints = PlayerHealthBarManager.totalMaxHitPoints;
		}
		else
		{
			this.hitPoints += healthRestored;
		}

	}
	#endregion

	// Use this for initialization
	public override void Awake() 
	{
		this.device = InputManager.Devices[0];
		this.currentControls = new PlayerControls(this.device);
		Player.playerControlsDict = new Dictionary<string, PlayerControls>();
		this.PopulatePlayerControls();
		base.Awake();
	}

	public void Start()
	{
		this.currentCharacter = CharacterSelector.GetCharacterByType(PlayerCharacterType.Warrior);
		this.SwapCharacter(this.currentCharacter);
	}

	// Update is called once per frame
	private void Update () {
		if (this.currentControls.movementControl != null)
		{
			this.velocity = this.currentControls.movementControl.Vector;
		}
		else
		{
			this.velocity = Vector2.zero;
		}

		if (this.currentControls.rotateControl != null)
		{
			this.rotateDirection = this.currentControls.rotateControl.Vector;
		}
		else
		{
			this.rotateDirection = Vector2.zero;
		}

		if (this.currentControls.characterSelectLeft != null && this.currentControls.characterSelectRight.WasPressed)
		{
			this.SwapCharacter(CharacterSelector.GetCharacterRight());
		}

		if (this.currentControls.characterSelectRight != null && this.currentControls.characterSelectLeft.WasPressed)
		{
			this.SwapCharacter(CharacterSelector.GetCharacterLeft());
		}

		if (this.currentControls.attackButton != null && this.currentControls.attackButton.WasPressed)
		{
			this.Attack();
		}

		if (this.currentControls.attackButton != null && this.currentControls.attackButton.WasReleased)
		{
			this.currentCharacter.weapon.continueAttacking = false;
		}

		if (this.currentControls.interactButton != null && this.currentControls.interactButton.WasPressed)
		{
			this.InteractWithClosestObject();
		}

		if (this.device.Action2.WasPressed)
		{
			//this.DropEquipment(this.activeWeapon);
			//this.DropEquipment(this.activeArmor);
			this.ApplyStatusEffects(this.activeWeapon);
		}
	}

	//Begin depleting inflicted status effects in the background
	private void DepleteInflictedStatusEffects()
	{
		foreach (InflictedStatusEffect effect in this.currentCharacter.activeInflictedStatusEffects)
		{
			effect.DepleteStatusEffect();
		}
	}

	//Re-apply inflicted status effects from their depleted state
	private void ActivateInflictedStatusEffects()
	{
		foreach (InflictedStatusEffect effect in this.currentCharacter.activeInflictedStatusEffects)
		{
			effect.ApplyStatusEffect(this);
		}
	}

	private void SwapCharacter(PlayerCharacter newCharacter)
	{
		this.DepleteInflictedStatusEffects();

		this.currentCharacter = newCharacter;
		this.meshRenderer.material = this.currentCharacter.characterMaterial;
		this.moveSpeed = this.currentCharacter.moveSpeed;

		this.SetActiveWeapon(this.currentCharacter.weapon);
		this.SetActiveArmor(this.currentCharacter.armor);

		this.ActivateInflictedStatusEffects();
	}

	public override void DisableMovement()
	{
		this.currentControls = Player.playerControlsDict[Player.DisablePlayerControlsKey];
	}

	public override void EnableMovement()
	{
		this.currentControls = Player.playerControlsDict[Player.DefaultPlayerControlsKey];
	}

	public override void LockToRotation()
	{
		this.currentControls = Player.playerControlsDict[Player.LockRotationPlayerControlsKey];
	}

	private PlayerCharacterType GetPlayerCharacterFromEquipmentClass(CreatureType creatureType)
	{
		switch (creatureType)
		{
		case CreatureType.Magic:
			return PlayerCharacterType.Mage;
		case CreatureType.Light:
			return PlayerCharacterType.Archer;
		case CreatureType.Medium:
			return PlayerCharacterType.Warrior;
		case CreatureType.Heavy:
			return PlayerCharacterType.Tank;
		default:
			Debug.LogError("Player.GetPlayerCharacterFromEquipmentClass: Unknown CreatureType: " + creatureType + ". Unable to determine PlayerCharacterType");
			return PlayerCharacterType.None;
		}
	}

	private void SetActiveWeaponIfCurrentCharacter(PlayerCharacter selectedCharacter, Weapon selectedWeapon)
	{
		if (selectedCharacter == this.currentCharacter)
		{
			this.SetActiveWeapon(selectedWeapon);
		}
	}

	private void SetActiveArmorIfCurrentCharacter(PlayerCharacter selectedCharacter, Armor selectedArmor)
	{
		if (selectedCharacter == this.currentCharacter)
		{
			this.SetActiveArmor(selectedArmor);
		}
	}

	#region Creature implementation
	public override void EquipWeapon(Weapon weaponToEquip)
	{
		PlayerCharacterType playerCharacterType = this.GetPlayerCharacterFromEquipmentClass(weaponToEquip.equippableCreatureType);
		PlayerCharacter selectedPlayerCharacter = CharacterSelector.GetCharacterByType(playerCharacterType);

		this.DropEquipment(selectedPlayerCharacter.weapon);
		selectedPlayerCharacter.weapon = weaponToEquip;
		this.SetActiveWeaponIfCurrentCharacter(selectedPlayerCharacter, selectedPlayerCharacter.weapon);
	}

	public override void EquipArmor(Armor armorToEquip)
	{
		PlayerCharacterType playerCharacterType = this.GetPlayerCharacterFromEquipmentClass(armorToEquip.equippableCreatureType);
		PlayerCharacter selectedPlayerCharacter = CharacterSelector.GetCharacterByType(playerCharacterType);

		this.DropEquipment(selectedPlayerCharacter.armor);
		selectedPlayerCharacter.armor = armorToEquip;
		this.SetActiveArmorIfCurrentCharacter(selectedPlayerCharacter, selectedPlayerCharacter.armor);
	}

	public override void DropEquipment(Equipment equipmentToDrop)
	{
		if (equipmentToDrop.equippableCreatureType == CreatureType.None)
		{
			return;
		}

		PlayerCharacterType playerCharacterType = this.GetPlayerCharacterFromEquipmentClass(equipmentToDrop.equippableCreatureType);
		PlayerCharacter selectedPlayerCharacter = CharacterSelector.GetCharacterByType(playerCharacterType);

		switch (equipmentToDrop.equipmentType)
		{
		case EquipmentType.Weapon:
			GameManager.GenerateGrabbableWeapon(this.transform.position, selectedPlayerCharacter.weapon);
			selectedPlayerCharacter.weapon = Resources.Load<Weapon>(ScriptableObjectPaths.WeaponsPath + "Unarmed");
			this.SetActiveWeaponIfCurrentCharacter(selectedPlayerCharacter, selectedPlayerCharacter.weapon);
			break;
		case EquipmentType.Armor:
			GameManager.GenerateGrabbableArmor(this.transform.position, selectedPlayerCharacter.armor);
			selectedPlayerCharacter.armor = Resources.Load<Armor>(ScriptableObjectPaths.ArmorPath + "Naked");
			this.SetActiveArmorIfCurrentCharacter(selectedPlayerCharacter, selectedPlayerCharacter.armor);
			break;
		default:
			Debug.Log("Player.DropEquipment: Unknown EquipmentType: " + equipmentToDrop.equipmentType + ". Unable to drop equipment");
			break;
		}
	}

	public override void Attack()
	{
		this.currentCharacter.weapon.continueAttacking = true;
		this.StartCoroutine(this.currentCharacter.weapon.Attack(this));	
	}
	#endregion

	#region PlayerControl Dictionary
	public void PopulatePlayerControls()
	{
		Player.playerControlsDict.Add(Player.DefaultPlayerControlsKey, this.PopulateDefaultPlayerControls());
		Player.playerControlsDict.Add(Player.DisablePlayerControlsKey, this.PopulateDisablePlayerControls());
		Player.playerControlsDict.Add(Player.LockRotationPlayerControlsKey, this.PopulateLockRotationPlayerControls());
	}

	public PlayerControls PopulateDefaultPlayerControls()
	{
		return new PlayerControls(this.device);
	}

	public PlayerControls PopulateDisablePlayerControls()
	{
		return new PlayerControls(null, null, null, null, null, null, null);
	}

	public PlayerControls PopulateLockRotationPlayerControls()
	{
		return new PlayerControls(null, this.device.LeftStick, this.device.RightStick, this.device.LeftBumper, this.device.RightBumper, this.device.Action1, null);
	}
	#endregion

	public override void ReturnToDefaultMaterial()
	{
		this.damageAnimator.enabled = false;
		this.meshRenderer.material = this.currentCharacter.characterMaterial;
	}
}
