using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTile : MonoBehaviour {

	MeshRenderer mesh;
	Material meshMaterial;

	private void Awake()
	{
		float tileScale = this.gameObject.transform.lossyScale.x * 10f;
		this.mesh = this.gameObject.GetComponent<MeshRenderer>();
		this.mesh.material.mainTextureScale = new Vector2(tileScale, tileScale);

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
