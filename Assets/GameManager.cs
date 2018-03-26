using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	private void Awake()
	{
		GameManager.instance = this;
	}

	// Use this for initialization
	void Start () {
		CameraFollow.instance.SetPointOfInterest(GameObject.Find("Player"));
	}
}
