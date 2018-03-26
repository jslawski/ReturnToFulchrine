using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

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

	public override void TakeDamage(float damageDealt/*, Enchantments too*/)
	{
		this.damageAnimator.enabled = true;

		this.hitPoints -= this.CalculateDamage(damageDealt);
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
	private void Awake() 
	{
		this.device = InputManager.Devices[0];
		this.currentControls = new PlayerControls(this.device);
		Player.playerControlsDict = new Dictionary<string, PlayerControls>();
		this.PopulatePlayerControls();
	}

	public void Start()
	{
		//base.Start();
		this.currentCharacter = CharacterSelector.GetCharacterAtIndex(0);
		this.SwapCharacter(this.currentCharacter);
		this.moveSpeed = this.currentCharacter.moveSpeed;
	}

	// Update is called once per frame
	private void Update () {
		if (this.currentControls.movementControl != null && this.currentControls.movementControl.Vector.magnitude > 0)
		{
			this.Move(this.currentControls.movementControl.Vector);
		}

		if (this.currentControls.rotateControl != null && this.currentControls.rotateControl.Vector.magnitude > 0)
		{
			this.Rotate(this.currentControls.rotateControl.Vector);
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

		if (this.device.Action2.WasPressed)
		{
			this.TakeDamage(150f);
		}

		if (this.device.Action3.WasPressed)
		{
			this.GainHealth(150f);
		}
	}

	private void SwapCharacter(PlayerCharacter newCharacter)
	{
		this.moveSpeed = newCharacter.moveSpeed;
		this.meshRenderer.material = newCharacter.characterMaterial;
		this.currentCharacter = newCharacter;

		this.activeWeapon = this.currentCharacter.weapon;
		this.activeArmor = this.currentCharacter.armor;
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

	private PlayerCharacterType GetPlayerCharacterFromEquipmentClass(EquipmentClass equipmentClass)
	{
		switch (equipmentClass)
		{
		case EquipmentClass.Magic:
			return PlayerCharacterType.Mage;
		case EquipmentClass.Light:
			return PlayerCharacterType.Archer;
		case EquipmentClass.Medium:
			return PlayerCharacterType.Warrior;
		case EquipmentClass.Heavy:
			return PlayerCharacterType.Tank;
		default:
			Debug.LogError("Player.GetPlayerCharacterFromEquipmentClass: Unknown EquipmentClass: " + equipmentClass + ". Unable to determine PlayerCharacterType");
			return PlayerCharacterType.None;
		}
	}

	#region Creature implementation
	public override void EquipWeapon(Weapon weaponToEquip)
	{
		PlayerCharacterType playerCharacterType = this.GetPlayerCharacterFromEquipmentClass(weaponToEquip.equipmentClass);
		PlayerCharacter selectedPlayerCharacter = CharacterSelector.characterDictionary[playerCharacterType];

		this.DropEquipment(selectedPlayerCharacter.weapon);
		selectedPlayerCharacter.weapon = weaponToEquip;
	}

	public override void EquipArmor(Armor armorToEquip)
	{
		PlayerCharacterType playerCharacterType = this.GetPlayerCharacterFromEquipmentClass(armorToEquip.equipmentClass);
		PlayerCharacter selectedPlayerCharacter = CharacterSelector.characterDictionary[playerCharacterType];

		this.DropEquipment(selectedPlayerCharacter.armor);
		selectedPlayerCharacter.armor = armorToEquip;
	}

	public override void DropEquipment(Equipment equipmentToDrop)
	{
		if (equipmentToDrop.equipmentClass == EquipmentClass.None)
		{
			return;
		}

		PlayerCharacterType playerCharacterType = this.GetPlayerCharacterFromEquipmentClass(equipmentToDrop.equipmentClass);
		PlayerCharacter selectedPlayerCharacter = CharacterSelector.characterDictionary[playerCharacterType];

		switch (equipmentToDrop.equipmentType)
		{
		case EquipmentType.Weapon:
			GrabbableEquipment.GenerateGrabbableWeapon(this.transform.position, selectedPlayerCharacter.weapon);
			selectedPlayerCharacter.weapon = new MeleeWeapon();
			break;
		case EquipmentType.Armor:
			GrabbableEquipment.GenerateGrabbableArmor(this.transform.position, selectedPlayerCharacter.armor);
			selectedPlayerCharacter.armor = new Armor();
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
		return new PlayerControls(null, null, null, null, null, null);
	}

	public PlayerControls PopulateLockRotationPlayerControls()
	{
		return new PlayerControls(null, this.device.LeftStick, this.device.RightStick, this.device.LeftBumper, this.device.RightBumper, this.device.Action1);
	}
	#endregion

	public override void ReturnToDefaultMaterial()
	{
		this.damageAnimator.enabled = false;
		this.meshRenderer.material = this.currentCharacter.characterMaterial;
	}
}
