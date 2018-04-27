using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public enum CreatureType { Magic, Light, Medium, Heavy, None };

public abstract class Creature : MoveableObject, IDamageableObject 
{
	[HideInInspector]
	public CreatureType type;

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

	public virtual void ApplyStatusEffects(Weapon weapon)
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
				}
				else
				{
					currentStatusEffect.level = data.aggregateLevel;
					currentStatusEffect.AttemptStatusEffectRefresh(this);
				}
			}
		}
	}

	public virtual void TakeDamage(float damageDealt, bool rawDamage = false, Weapon weapon = null)
	{
		this.damageAnimator.enabled = true;

		if (rawDamage == true)
		{
			this.hitPoints -= damageDealt;
		}
		else
		{
			this.hitPoints -= this.CalculateDamage(damageDealt, weapon.critChance, weapon.attackAffinityMetaData);
		}

		if (weapon != null)
		{
			this.ApplyStatusEffects(weapon);
		}

		Debug.LogError("Remaining HP: " + this.hitPoints);

		return;
	}

	public virtual void Knockback(Vector3 knockbackDirection, float knockbackDistance)
	{
		this.StartCoroutine(this.KnockbackCoroutine(knockbackDirection, knockbackDistance));
	}

	private IEnumerator KnockbackCoroutine(Vector3 knockbackDirection, float knockbackDistance)
	{
		float cumulativeDistanceTravelled = 0f;

		while (cumulativeDistanceTravelled < knockbackDistance)
		{
			this.velocity = knockbackDirection.normalized;
			cumulativeDistanceTravelled += 1f;
			yield return new WaitForEndOfFrame();
		}

		this.velocity = Vector3.zero;
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

	protected float CalculateDamage(float damageDealt, float critChance, List<AggregateAffinityMetaData> attackAffinityMetaData)
	{
		Debug.LogError("Initial Damage: " + damageDealt);

		float critRoll = UnityEngine.Random.Range(0f, 1f);
		bool criticalHit = false;
		if (critRoll <= critChance)
		{
			Debug.LogError("CRITICAL HIT!");
			criticalHit = true;
			damageDealt *= 2f;
		}

		float resultant = damageDealt - this.activeArmor.armorClass;

		if (resultant < 0)
		{
			resultant = 0;
		}

		Debug.LogError("Base Damage Dealt: " + resultant);

		//Apply all affinity bonus damage
		foreach (AggregateAffinityMetaData metaData in attackAffinityMetaData)
		{
			float affinityDamage = metaData.affinityAsset.GetAffinityDamage(metaData.aggregateLevel);
			if (criticalHit == true)
			{
				affinityDamage *= 2f;
			}

			Debug.LogError("Original Affinity Damage: " + affinityDamage);
			float modifiedAffinityDamage =  Mathf.Ceil(affinityDamage * this.activeArmor.GetDefenseAffinityMultiplier(metaData));
			Debug.LogError("Modified Affinity Damage: " + modifiedAffinityDamage);
			resultant += modifiedAffinityDamage;
		}

		Debug.LogError("Total Damage Dealt: " + resultant);

		return resultant;
	}

	public virtual void Awake()
	{
		this.interactableObjectsInRange = new List<InteractableObject>();
		this.interactableTriggerZone.onInteractableZoneEntered += this.AddInteractableObject;
		this.interactableTriggerZone.onInteractableZoneExited += this.RemoveInteractableObject;
	}



	public virtual void ActivateEquipStatusEffectsForEquipment(Equipment equipment)
	{
		foreach (AggregateStatusEffectMetaData metaData in equipment.statusEffectMetaData)
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
		List<EquipStatusEffect> currentEquipStatusEffects = this.gameObject.GetComponents<EquipStatusEffect>().ToList();

		foreach (AggregateStatusEffectMetaData metaData in equipment.statusEffectMetaData)
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

	public virtual void SetActiveWeapon(Weapon weapon)
	{
		if (this.activeWeapon.statusEffectMetaData != null)
		{
			this.DeactivateEquipStatusEffectsForEquipment(this.activeWeapon);
		}

		this.activeWeapon = weapon;
		this.activeWeapon.RefreshAggregateMetaData();
		this.ActivateEquipStatusEffectsForEquipment(this.activeWeapon);
	}

	public virtual void SetActiveArmor(Armor armor)
	{
		if (this.activeArmor.statusEffectMetaData != null)
		{
			this.DeactivateEquipStatusEffectsForEquipment(this.activeArmor);
		}

		this.activeArmor = armor;
		this.activeArmor.RefreshAggregateMetaData();
		this.ActivateEquipStatusEffectsForEquipment(this.activeArmor);
	}

	public virtual void EquipWeapon(Weapon weaponToEquip)
	{
		if (weaponToEquip.equippableCreatureType == this.type)
		{
			this.DropEquipment(this.activeWeapon);
			this.SetActiveWeapon(weaponToEquip);
		}
	}

	public virtual void EquipArmor(Armor armorToEquip)
	{
		if (armorToEquip.equippableCreatureType == this.type)
		{
			this.DropEquipment(this.activeArmor);
			this.SetActiveArmor(armorToEquip);
		}
	}

	public virtual void DropEquipment(Equipment equipmentToDrop)
	{
		//Don't drop anything if unarmed
		if (equipmentToDrop.equippableCreatureType == CreatureType.None)
		{
			return;
		}
			
		switch (equipmentToDrop.equipmentType)
		{
		case EquipmentType.Weapon:
			GameManager.GenerateGrabbableWeapon(this.transform.position, this.activeWeapon);
			this.SetActiveWeapon(Resources.Load<Weapon>(ScriptableObjectPaths.WeaponsPath + "Unarmed"));
			break;
		case EquipmentType.Armor:
			GameManager.GenerateGrabbableArmor(this.transform.position, this.activeArmor);
			this.SetActiveArmor(Resources.Load<Armor>(ScriptableObjectPaths.ArmorPath + "Naked"));
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



	#region InteractableObject handling
	protected IEnumerator HandleInteractableObjects()
	{
		while (this.interactableObjectsInRange.Count > 0)
		{
			//Sort list of interactable objects from closest to furthest
			foreach (InteractableObject currentObject in this.interactableObjectsInRange)
			{
				currentObject.distanceFromCreature = Vector3.Distance(this.transform.position, currentObject.transform.position);
				currentObject.infoPanel.SetActive(false);
			}
			this.sortedInteractableObjects = this.interactableObjectsInRange.OrderBy(i => i.distanceFromCreature);
			this.sortedInteractableObjects.First().infoPanel.SetActive(true);

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
		objectToRemove.infoPanel.SetActive(false);
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
