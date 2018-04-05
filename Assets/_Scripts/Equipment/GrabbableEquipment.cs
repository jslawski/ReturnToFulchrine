using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PolarCoordinates;

public abstract class GrabbableEquipment : InteractableObject {

	private float activationTime = 0.5f;

	private float instantiationLaunchMagnitude = 5f;

	private MeshRenderer meshRenderer;

	void Awake()
	{
		Invoke("ActivateCollider", this.activationTime);
		this.meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
	}

	private void ActivateCollider()
	{
		this.gameObject.GetComponent<BoxCollider>().enabled = true;
	}

	public IEnumerator LaunchGrabbableEquipment()
	{
		PolarCoordinate instantiationDirection = new PolarCoordinate(1.0f, Random.Range(0f, 360f));
		float currentLaunchMagnitude = this.instantiationLaunchMagnitude;

		while (currentLaunchMagnitude > 0.01)
		{
			this.gameObject.transform.Translate(instantiationDirection.PolarToCartesian() * Time.deltaTime * currentLaunchMagnitude);
			currentLaunchMagnitude -= 0.1f;
			yield return null;
		}
	}

	public void UpdateEquipmentColor(Equipment equipment)
	{
		switch (equipment.rarity)
		{
		case Rarity.Common:
			this.meshRenderer.material = Resources.Load<Material>("EquipmentMaterials/Common");
			break;
		case Rarity.Uncommon:
			this.meshRenderer.material = Resources.Load<Material>("EquipmentMaterials/Uncommon");
			break;
		case Rarity.Rare:
			this.meshRenderer.material = Resources.Load<Material>("EquipmentMaterials/Rare");
			break;
		case Rarity.Legendary:
			this.meshRenderer.material = Resources.Load<Material>("EquipmentMaterials/Legendary");
			break;
		default:
			Debug.LogError("GrabbableEquipment.UpdateEquipmentColor: Unknown Rarity: " + equipment.rarity + ". Unable to change equipment color");
			return;
		}
	}
		
	public override void Interact(Creature creature)
	{
		creature.RemoveInteractableObject(this);
		Destroy(this.gameObject);
	}
}
