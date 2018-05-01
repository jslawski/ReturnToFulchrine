using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHookEnemy : Enemy {

	private enum GrappleHookEnemyState { Roam, Chase, Throw, Attack };

	private GrappleHookEnemyState _currentState;
	private GrappleHookEnemyState currentState 
	{
		get { return _currentState; }
		set 
		{ 
			this._currentState = value;
			this.UpdateState();
		}
	}

	public Creature playerInstance;

	[SerializeField]
	private GameObject grappleHookPrefab;

	// Use this for initialization
	public override void Start() {
		base.Start();
		this.moveSpeed = 3f;

		this.currentState = GrappleHookEnemyState.Chase;
	}

	private void UpdateState()
	{
		switch (this.currentState)
		{
		case GrappleHookEnemyState.Roam:
			this.StartCoroutine(this.Roam());
			break;
		case GrappleHookEnemyState.Chase:
			this.StartCoroutine(this.Chase());
			break;
		case GrappleHookEnemyState.Throw:
			this.Throw();
			break;
		case GrappleHookEnemyState.Attack:
			this.StartCoroutine(this.MeleeAttack());
			break;
		}
	}

	private IEnumerator Roam()
	{
		while (this.currentState == GrappleHookEnemyState.Roam)
		{
			//Do nothing
			yield return null;
		}

		this.currentState = GrappleHookEnemyState.Chase;
	}

	private Vector2 GetChaseDirection()
	{
		if (this.playerInstance.velocity == Vector2.zero)
		{
			return (this.playerInstance.transform.position - this.transform.position).normalized;
		}

		Vector2 point1 = new Vector2(this.playerInstance.transform.position.x, this.playerInstance.transform.position.y);
		Vector2 point2 = point1 + this.playerInstance.velocity.normalized;
		Vector2 comparePoint = new Vector2(this.transform.position.x, this.transform.position.y);

		float comparator = ((comparePoint.x - point1.x) * (point2.y - point1.y)) - ((comparePoint.y - point1.y) * (point2.x - point1.x));

		if (comparator < -0.5f)
		{
			return new Vector2(this.playerInstance.velocity.y, -this.playerInstance.velocity.x).normalized;
		}
		else if (comparator > 0.5f)
		{
			return new Vector2(-this.playerInstance.velocity.y, this.playerInstance.velocity.x).normalized;
		}
		else
		{
			return (this.playerInstance.transform.position - this.transform.position).normalized;
		}
	}

	private IEnumerator Chase()
	{
		while (this.currentState == GrappleHookEnemyState.Chase)
		{
			this.velocity = this.GetChaseDirection();
			this.rotateDirection = this.velocity;

			if ((this.playerInstance.transform.position - this.gameObject.transform.position).magnitude < 5f)
			{
				this.currentState = GrappleHookEnemyState.Throw;
				break;
			}

			yield return null;
		}

		this.velocity = Vector2.zero;
	}

	private void ReturnToChaseState()
	{
		this.currentState = GrappleHookEnemyState.Chase;
	}

	private void Throw()
	{
		GameObject grappleHookInstance = Instantiate(this.grappleHookPrefab, this.gameObject.transform.position, new Quaternion(), this.gameObject.transform);
		GrappleHook grappleHookComponent = grappleHookInstance.GetComponent<GrappleHook>();
		grappleHookComponent.LanchGrappleHook(this, this.rotateDirection);
		grappleHookComponent.OnGrappleHookFinished += this.ReturnToChaseState;
	}

	private IEnumerator MeleeAttack()
	{
		while (this.currentState == GrappleHookEnemyState.Attack)
		{
			//Do nothing	
			yield return null;
		}

		this.UpdateState();
	}
}
