using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Loot Chest class.  Make this abstract in the future when chest rarities are a thing.
/// </summary>
public class LootChest : InteractableObject {

	private string GetResourcePath(EquipmentType equipmentType, CreatureType creatureType, Rarity rarity)
	{
		string equipmentResourcePoolPath = string.Empty;

		switch (equipmentType)
		{
		case EquipmentType.Weapon:
			equipmentResourcePoolPath += ScriptableObjectPaths.WeaponsPath;
			break;
		case EquipmentType.Armor:
			equipmentResourcePoolPath += ScriptableObjectPaths.ArmorPath;
			break;
		default:
			Debug.LogError("LootChest.GetResourcePathName: Unknown EquipmentType: " + equipmentType + ". Unable to generate loot.");
			return string.Empty;
		}

		switch (creatureType)
		{
		case CreatureType.Magic:
			equipmentResourcePoolPath += ScriptableObjectPaths.MagicEquipmentDirectoryName;
			break;
		case CreatureType.Light:
			equipmentResourcePoolPath += ScriptableObjectPaths.LightEquipmentDirectoryName;
			break;
		case CreatureType.Medium:
			equipmentResourcePoolPath += ScriptableObjectPaths.MediumEquipmentDirectoryName;
			break;
		case CreatureType.Heavy:
			equipmentResourcePoolPath += ScriptableObjectPaths.HeavyEquipmentDirectoryName;
			break;
		default:
			Debug.LogError("LootChest.GetResourcePathName: Unknown CreatureType: " + creatureType + ". Unable to generate loot.");
			return string.Empty;
		}

		switch (rarity)
		{
		case Rarity.Common:
			equipmentResourcePoolPath += ScriptableObjectPaths.CommonEquipmentDirectoryName;
			break;
		case Rarity.Uncommon:
			equipmentResourcePoolPath += ScriptableObjectPaths.UncommonEquipmentDirectoryName;
			break;
		case Rarity.Rare:
			equipmentResourcePoolPath += ScriptableObjectPaths.RareEquipmentDirectoryName;
			break;
		case Rarity.Legendary:
			equipmentResourcePoolPath += ScriptableObjectPaths.LegendaryEquipmentDirectoryName;
			break;
		default:
			Debug.LogError("LootChest.GenerateResourcePathName: Unknown Rarity: " + rarity + ". Unable to generate loot");
			return string.Empty;
		}

		return equipmentResourcePoolPath;
	}

	protected virtual Rarity GetEquipmentRarity()
	{
		float rarityRoll = Random.Range(0.0f, 1.0f);

		if (rarityRoll <= 0.05f)
		{
			return Rarity.Legendary;
		}
		else if (rarityRoll <= 0.20f)
		{
			return Rarity.Rare;
		}
		else if (rarityRoll <= 0.50f)
		{
			return Rarity.Uncommon;
		}

		return Rarity.Common;
	}

	protected virtual void GenerateLoot()
	{
		//Roll for equipment type
		EquipmentType lootType = (EquipmentType)Random.Range(0, (int)EquipmentType.None);

		//Roll for creature type
		CreatureType creatureType =  (CreatureType)Random.Range(0, (int)CreatureType.None);

		//Roll for rarity
		Rarity rarity = this.GetEquipmentRarity();

		string resourcePath = this.GetResourcePath(lootType, creatureType, rarity);
		if (resourcePath == string.Empty)
		{
			return;
		}
			
		switch (lootType)
		{
		case EquipmentType.Weapon:
			Weapon[] weapons = Resources.LoadAll<Weapon>(resourcePath);
			Weapon chosenWeapon = weapons[Random.Range(0, weapons.Length)];
			GameManager.GenerateGrabbableWeapon(this.transform.position, chosenWeapon);
			break;
		case EquipmentType.Armor:
			Armor[] armor = Resources.LoadAll<Armor>(resourcePath);
			Armor chosenArmor = armor[ Random.Range(0, armor.Length)];
			GameManager.GenerateGrabbableArmor(this.transform.position, chosenArmor);
			break;
		default:
			Debug.LogError("LootChest.GenerateLoot: Unknown EquipmentType: " + lootType + ". Unable to generate loot.");
			return;
		}
	}

	public override void Interact(Creature creature)
	{	
		this.GenerateLoot();
		creature.RemoveInteractableObject(this);
		Destroy(this.gameObject);
	}
}
