using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour {

	[HideInInspector]
	public float distanceFromCreature = float.MaxValue;

	public abstract void Interact(Creature creature);
}
