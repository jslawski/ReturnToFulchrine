﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamageableObject 
{
	public float moveSpeed = 5;

	public GameObject attackZone;
	public float attackStartPointY = 0.5f;

	public Weapon activeWeapon;
	public Armor activeArmor;

	[SerializeField]
	protected Animator damageAnimator;

	public float totalHitPoints = 100f;

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

	public void Move(Vector2 moveVector)
	{
		this.gameObject.transform.Translate(moveVector * Time.deltaTime * this.moveSpeed, Space.World);
	}

	public void Rotate(Vector2 forwardVector)
	{
		this.gameObject.transform.up = forwardVector;
	}

	public virtual void EquipWeapon(Weapon weaponToEquip)
	{
		this.DropEquipment(this.activeWeapon);
		this.activeWeapon = weaponToEquip;
	}

	public virtual void EquipArmor(Armor armorToEquip)
	{
		this.DropEquipment(this.activeArmor);
		this.activeArmor = armorToEquip;
	}

	public virtual void DropEquipment(Equipment equipmentToDrop)
	{
		if (equipmentToDrop.equipmentClass == EquipmentClass.None)
		{
			return;
		}

		switch (equipmentToDrop.equipmentType)
		{
		case EquipmentType.Weapon:
			GrabbableEquipment.GenerateGrabbableWeapon(this.transform.position, this.activeWeapon);
			this.activeWeapon = new MeleeWeapon();
			break;
		case EquipmentType.Armor:
			GrabbableEquipment.GenerateGrabbableArmor(this.transform.position, this.activeArmor);
			this.activeArmor = new Armor();
			break;
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
}
