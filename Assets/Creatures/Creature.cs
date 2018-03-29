using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatureType { Magic, Light, Medium, Heavy, None };

public abstract class Creature : MonoBehaviour, IDamageableObject 
{
	public CreatureType type;

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
		if (weaponToEquip.equippableCreatureType == this.type)
		{
			this.DropEquipment(this.activeWeapon);
			this.activeWeapon = weaponToEquip;
		}
	}

	public virtual void EquipArmor(Armor armorToEquip)
	{
		if (armorToEquip.equippableCreatureType == this.type)
		{
			this.DropEquipment(this.activeArmor);
			this.activeArmor = armorToEquip;
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
}
