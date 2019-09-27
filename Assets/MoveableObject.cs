using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MoveableObject : MonoBehaviour {

	[SerializeField]
	private LayerMask ignoreColliderLayers;


	public Collider objectCollider;
	private Vector3 correctionVector;

	public Vector2 velocity = Vector3.zero;
	protected Vector2 rotateDirection = Vector3.zero;

	public float moveSpeed = 0f;
	public float moveSpeedBonus = 0f;

	protected void FixedUpdate()
	{
		if (this.velocity != Vector2.zero)
		{
			this.Move();
		}
		if (this.rotateDirection != Vector2.zero)
		{
			this.Rotate();
		}
	}

	public void Move()
	{
		this.gameObject.transform.Translate(velocity * Time.fixedDeltaTime * (this.moveSpeed + this.moveSpeedBonus), Space.World);
	}

	public void Rotate()
	{
		this.gameObject.transform.up = rotateDirection;
	}

	//TODO: Look into why this needs to be OnTriggerStay instead of OnTriggerEnter!
	//Theory: ComputePenetration has a rounding error that does not COMPLETELY move a moveableObject out of an obstacle.  As such, it moves it
	//ALMOST out, but not completely, so the moveable object doesn't re-enter the collider to trigger another resoultion.
	public void OnTriggerStay(Collider other)
	{
		int colliderLayerMask = 1 << other.gameObject.layer;

		if ((this.ignoreColliderLayers & colliderLayerMask) != 0)
		{
			return;
		}

		Vector3 resolutionDirection = Vector3.zero;
		float resolutionDistance = 0f;

		bool isStillColliding = Physics.ComputePenetration(this.objectCollider, this.transform.position, this.transform.rotation, other, other.transform.position, other.transform.rotation, out resolutionDirection, out resolutionDistance);

		if (isStillColliding == true)
		{
			this.gameObject.transform.position = this.gameObject.transform.position + ((resolutionDirection * resolutionDistance));
		}
	}
}
