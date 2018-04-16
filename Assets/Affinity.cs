using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AffinityType { Fire, Ice, Electric, None };

[CreateAssetMenu(fileName = "New Affinity", menuName = "Affinity")]
public class Affinity : ScriptableObject {

	public AffinityType type;

	public int level;

	public List<AffinityType> advantageAffinities;

	public List<AffinityType> disadvantageAffinities;

	public float GetAffinityDamage(int affinityLevel)
	{
		switch (affinityLevel)
		{
		case 1:
			return Mathf.Ceil(Random.Range(1f, 6f));
		case 2:
			return Mathf.Ceil(Random.Range(1f, 10f));
		case 3:
			return Mathf.Ceil(Random.Range(5f, 12f));
		case 4:
			return Mathf.Ceil(Random.Range(6f, 14f));
		case 5:
			return Mathf.Ceil(Random.Range(10f, 14f));
		default:
			Debug.LogError("Affinity.GetAffinityDamage: Unknown affinityLevel: " + affinityLevel + ". Unable to add additional affinity damage.");
			return 0;
		}
	}

	public float ApplyDefenseAffinity(Affinity attackAffinity, float affinityDamage)
	{
		return 0;	
	}
}
