using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Loot Chest class.  Make this abstract in the future when chest rarities are a thing.
/// </summary>
public class LootChest : InteractableObject {

	private string GetResourcePath(EquipmentType equipmentType, CreatureType creatureType)
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

		return equipmentResourcePoolPath;
	}

	protected virtual void GenerateLoot()
	{
		//Roll for equipment type
		EquipmentType lootType = (EquipmentType)Random.Range(0, (int)EquipmentType.None);

		//Roll for creature type
		CreatureType creatureType =  (CreatureType)Random.Range(0, (int)CreatureType.None);

		string resourcePath = this.GetResourcePath(lootType, creatureType);
		if (resourcePath == string.Empty)
		{
			return;
		}
			
		switch (lootType)
		{
		case EquipmentType.Weapon:
			Weapon[] weapons = Resources.LoadAll<Weapon>(resourcePath);
			Weapon chosenWeapon = weapons[Random.Range(0, weapons.Length)];
			GrabbableEquipment.GenerateGrabbableWeapon(this.transform.position, chosenWeapon);
			break;
		case EquipmentType.Armor:
			Armor[] armor = Resources.LoadAll<Armor>(resourcePath);
			Armor chosenArmor = armor[Random.Range(0, armor.Length)];
			GrabbableEquipment.GenerateGrabbableArmor(this.transform.position, chosenArmor);
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
