using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PolarCoordinates;

public class GrabbableEquipment : MonoBehaviour {

	private float activationTime = 0.5f;

	private float instantiationLaunchMagnitude = 5f;

	void Awake()
	{
		Invoke("ActivateCollider", this.activationTime);
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

	public static void GenerateGrabbableWeapon(Vector3 instantiationPosition, Weapon weaponDetails)
	{
		GameObject grabbableWeaponPrefab = Resources.Load<GameObject>("GrabbableWeapon");

		GameObject instance = GameObject.Instantiate(grabbableWeaponPrefab, instantiationPosition, new Quaternion()) as GameObject;
		GrabbableWeapon grabbableWeaponComponent = instance.GetComponent<GrabbableWeapon>();
		grabbableWeaponComponent.weaponDetails = weaponDetails;

		grabbableWeaponComponent.StartCoroutine("LaunchGrabbableEquipment");
	}

	public static void GenerateGrabbableArmor(Vector3 instantiationPosition, Armor armorDetails)
	{
		GameObject grabbableArmorPrefab = Resources.Load<GameObject>("GrabbableArmor");

		GameObject instance = GameObject.Instantiate(grabbableArmorPrefab, instantiationPosition, new Quaternion()) as GameObject;
		GrabbableArmor grabbableArmorComponent = instance.GetComponent<GrabbableArmor>();
		grabbableArmorComponent.armorDetails = armorDetails;
		grabbableArmorComponent.StartCoroutine("LaunchGrabbableEquipment");
	}
}
