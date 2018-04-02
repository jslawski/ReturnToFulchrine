﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public enum CreatureType { Magic, Light, Medium, Heavy, None };

public abstract class Creature : MonoBehaviour, IDamageableObject 
{
	[HideInInspector]
	public CreatureType type;

	public float moveSpeed = 5;

	public GameObject attackZone;
	public float attackStartPointY = 0.5f;

	public Weapon activeWeapon;
	public Armor activeArmor;

	[SerializeField]
	protected Animator damageAnimator;

	public float totalHitPoints = 100f;

	[SerializeField]
	private InteractableTriggerZone interactableTriggerZone;
	protected List<InteractableObject> interactableObjectsInRange;
	private IOrderedEnumerable<InteractableObject> sortedInteractableObjects;

	#region IDamageableObject implementation
	protected float _hitPoints = 100f;

	public virtual float hitPoints
	{
		get { return this._hitPoints; }
		set 
		{ 
			this._hitPoints = value;
			if (this._hitPoints <= 0)
			{
				Destroy(this.gameObject);
			}
		}
	}

	public virtual void TakeDamage(float damageDealt/*, Enchantments too*/)
	{
		this.damageAnimator.enabled = true;

		this.hitPoints -= this.CalculateDamage(damageDealt);

		return;
	}

	public virtual void GainHealth(float healthRestored)
	{
		if (this.hitPoints + healthRestored > this.totalHitPoints)
		{
			this.hitPoints = this.totalHitPoints;
		}
		else
		{
			this.hitPoints += healthRestored;
		}
	}
	#endregion

	public virtual void Awake()
	{
		this.interactableObjectsInRange = new List<InteractableObject>();
		this.interactableTriggerZone.onInteractableZoneEntered += this.AddInteractableObject;
		this.interactableTriggerZone.onInteractableZoneExited += this.RemoveInteractableObject;
	}

	public void Move(Vector2 moveVector)
	{
		this.gameObject.transform.Translate(moveVector * Time.deltaTime * this.moveSpeed, Space.World);
	}

	public void Rotate(Vector2 forwardVector)
	{
		this.gameObject.transform.up = forwardVector;
	}

	public virtual void ActivateEquipStatusEffectsForEquipment(Equipment equipment)
	{
		List<AggregateStatusEffectMetaData> statusEffectMetaData = AggregateStatusEffectMetaData.GetAggregateStatusEffectMetaData(equipment);

		foreach (AggregateStatusEffectMetaData metaData in statusEffectMetaData)
		{
			if (metaData.statusEffect.activateOnEquip == true)
			{
				List<EquipStatusEffect> currentEquipStatusEffects = this.gameObject.GetComponents<EquipStatusEffect>().ToList();
				EquipStatusEffect currentStatusEffect = currentEquipStatusEffects.Find(x => x.statusEffectName == metaData.statusEffect.statusEffectName);

				if (currentStatusEffect != null)
				{
					currentStatusEffect.level += metaData.aggregateLevel;
					currentStatusEffect.UpdateStatusEffect();
				}
				else
				{
					currentStatusEffect = this.gameObject.AddComponent(Type.GetType(metaData.statusEffect.statusEffectName)) as EquipStatusEffect;
					currentStatusEffect.equipmentType = equipment.equipmentType;
					currentStatusEffect.level = metaData.aggregateLevel;
					currentStatusEffect.statusEffectName = metaData.statusEffect.statusEffectName;
					currentStatusEffect.ApplyStatusEffect(this);
				}
			}
		}
	}

	public virtual void DeactivateEquipStatusEffectsForEquipment(Equipment equipment)
	{
		List<AggregateStatusEffectMetaData> statusEffectMetaData = AggregateStatusEffectMetaData.GetAggregateStatusEffectMetaData(equipment);
		List<EquipStatusEffect> currentEquipStatusEffects = this.gameObject.GetComponents<EquipStatusEffect>().ToList();

		foreach (AggregateStatusEffectMetaData metaData in statusEffectMetaData)
		{
			if (metaData.statusEffect.activateOnEquip == true)
			{
				EquipStatusEffect currentStatusEffect = currentEquipStatusEffects.Find(x => x.statusEffectName == metaData.statusEffect.statusEffectName);
				if (currentStatusEffect == null)
				{
					Debug.LogError("Creature.DeactivateEquipStatusEffectsForEquipment: Attempting to deactivate Status Effect " + metaData.statusEffect.statusEffectName + " but it is not currently active on the creature");
					return;
				}
				currentStatusEffect.level -= metaData.aggregateLevel;
				if (currentStatusEffect.level <= 0)
				{
					currentStatusEffect.StopStatusEffect();
					Destroy(currentStatusEffect);
				}
				else
				{
					currentStatusEffect.UpdateStatusEffect();
				}
			}
		}
	}

	public virtual void DeactivateAllEquipStatusEffects()
	{
		EquipStatusEffect[] allEquippedStatusEffects = this.gameObject.GetComponents<EquipStatusEffect>();

		foreach (EquipStatusEffect currentStatusEffect in allEquippedStatusEffects)
		{
			currentStatusEffect.StopStatusEffect();
			Destroy(currentStatusEffect);
		}
	}

	public virtual void EquipWeapon(Weapon weaponToEquip)
	{
		if (weaponToEquip.equippableCreatureType == this.type)
		{
			this.DeactivateEquipStatusEffectsForEquipment(this.activeWeapon);

			this.DropEquipment(this.activeWeapon);
			this.activeWeapon = weaponToEquip;

			this.ActivateEquipStatusEffectsForEquipment(this.activeWeapon);
		}
	}

	public virtual void EquipArmor(Armor armorToEquip)
	{
		if (armorToEquip.equippableCreatureType == this.type)
		{
			this.DeactivateEquipStatusEffectsForEquipment(this.activeArmor);

			this.DropEquipment(this.activeArmor);
			this.activeArmor = armorToEquip;

			this.ActivateEquipStatusEffectsForEquipment(this.activeArmor);
		}
	}

	public virtual void DropEquipment(Equipment equipmentToDrop)
	{
		//Don't drop anything if unarmed
		if (equipmentToDrop.equippableCreatureType == CreatureType.None)
		{
			return;
		}
			
		this.DeactivateEquipStatusEffectsForEquipment(equipmentToDrop);

		switch (equipmentToDrop.equipmentType)
		{
		case EquipmentType.Weapon:
			GrabbableEquipment.GenerateGrabbableWeapon(this.transform.position, this.activeWeapon);
			this.activeWeapon = Resources.Load<Weapon>(ScriptableObjectPaths.WeaponsPath + "Unarmed");
			break;
		case EquipmentType.Armor:
			GrabbableEquipment.GenerateGrabbableArmor(this.transform.position, this.activeArmor);
			this.activeArmor = Resources.Load<Armor>(ScriptableObjectPaths.ArmorPath + "Naked");
			break;
		default:
			Debug.LogError("Creature.DropEquipment: Unknown EquipmentType: " + equipmentToDrop.equipmentType + ". Unable to drop equipment.");
			return;
		}
	}

	public virtual void ReturnToDefaultMaterial()
	{
		this.damageAnimator.enabled = false;
		//Put default renderer material here;
	}

	public abstract void DisableMovement();
	public abstract void EnableMovement();
	public abstract void LockToRotation();
	public abstract void Attack();

	protected float CalculateDamage(float damageDealt /*, Enchantments*/)
	{
		float resultant = damageDealt - this.activeArmor.armorClass;

		//Do Enchantment shenanigans here?  Bonus damage only.

		if (resultant < 0)
		{
			resultant = 0;
		}

		return resultant;
	}

	#region InteractableObject handling
	protected IEnumerator HandleInteractableObjects()
	{
		while (this.interactableObjectsInRange.Count > 0)
		{
			//Sort list of interactable objects from closest to furthest
			foreach (InteractableObject currentObject in this.interactableObjectsInRange)
			{
				currentObject.distanceFromCreature = Vector3.Distance(this.transform.position, currentObject.transform.position);
			}
			this.sortedInteractableObjects = this.interactableObjectsInRange.OrderBy(i => i.distanceFromCreature);

			yield return null;
		}
	}

	protected void AddInteractableObject(InteractableObject objectToAdd)
	{
		if (this.interactableObjectsInRange.Count == 0)
		{
			this.interactableObjectsInRange.Add(objectToAdd);
			this.StartCoroutine(HandleInteractableObjects());
		}
		else
		{
			this.interactableObjectsInRange.Add(objectToAdd);
		}
	}

	public void RemoveInteractableObject(InteractableObject objectToRemove)
	{
		this.interactableObjectsInRange.Remove(objectToRemove);
	}

	protected void InteractWithClosestObject()
	{
		if (this.sortedInteractableObjects.Count() > 0)
		{
			InteractableObject closestObject = this.sortedInteractableObjects.First();
			closestObject.Interact(this);
		}
	}
	#endregion
}
