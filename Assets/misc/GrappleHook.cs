using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour {

	public delegate void GrappleHookFinished();
	public event GrappleHookFinished OnGrappleHookFinished;

	private Creature owner;

	public BoxCollider hookCollider;
	[SerializeField]
	private GameObject grappleHookChainPrefab;
	private Stack<GameObject> grappleHookChainPieces;

	public float grappleSpeed = 5f;
	public float grappleDistance = 5f;

	public void LanchGrappleHook(Creature owner, Vector2 direction)
	{
		grappleHookChainPieces = new Stack<GameObject>();
		this.owner = owner;
		this.StartCoroutine(this.Launch(direction.normalized));
	}

	private IEnumerator Launch(Vector2 direction)
	{
		float currentDistance = 0f;
		Vector3 direction3d = new Vector3(direction.x, direction.y, 0f);

		GameObject chainPiece = Instantiate(grappleHookChainPrefab, this.gameObject.transform.position - (direction3d * currentDistance), new Quaternion(), this.gameObject.transform) as GameObject;
		grappleHookChainPieces.Push(chainPiece);

		while (currentDistance < this.grappleDistance)
		{
			float distanceTravelledThisFrame = this.grappleSpeed * Time.fixedDeltaTime;

			chainPiece = Instantiate(grappleHookChainPrefab, this.gameObject.transform.position - (direction3d * currentDistance), new Quaternion(), this.gameObject.transform) as GameObject;
			grappleHookChainPieces.Push(chainPiece);


			this.gameObject.transform.Translate(direction * distanceTravelledThisFrame, Space.World);
			currentDistance += distanceTravelledThisFrame;

			yield return null;
		}

		this.StartCoroutine(this.Retract(-direction, currentDistance));
	}

	private IEnumerator Retract(Vector2 direction, float distanceTravelled)
	{
		float currentDistance = distanceTravelled;
		while (currentDistance > 0)
		{
			Destroy(grappleHookChainPieces.Pop());

			float distanceTravelledThisFrame = this.grappleSpeed * Time.fixedDeltaTime;
			this.gameObject.transform.Translate(direction * distanceTravelledThisFrame, Space.World);
			currentDistance -= distanceTravelledThisFrame;

			yield return null;
		}

		if (this.OnGrappleHookFinished != null)
		{
			this.OnGrappleHookFinished();
		}

		Destroy(this.gameObject);
	}
}
