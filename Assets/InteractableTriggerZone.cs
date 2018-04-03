using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTriggerZone : MonoBehaviour {

	public delegate void InteractableZoneEnter(InteractableObject objectToAdd);
	public event InteractableZoneEnter onInteractableZoneEntered;

	public delegate void InteractableZoneExit(InteractableObject objectToRemove);
	public event InteractableZoneExit onInteractableZoneExited;

	private void OnTriggerEnter(Collider other)
	{
		InteractableObject objectToAdd = other.GetComponent<InteractableObject>();

		if (objectToAdd != null)
		{
			this.onInteractableZoneEntered(objectToAdd);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		InteractableObject objectToRemove = other.GetComponent<InteractableObject>();

		if (objectToRemove != null)
		{
			this.onInteractableZoneExited(objectToRemove);
		}
	}
}
