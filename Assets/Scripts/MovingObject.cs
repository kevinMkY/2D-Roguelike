using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {

	private BoxCollider2D box2D;
	private Rigidbody2D rb2D;
	public LayerMask blockingLayer;

	public float moveTime = 0.1f;
	private float reverMoveTime;

	public virtual void Start(){
		box2D = transform.GetComponent<BoxCollider2D>();
		rb2D = GetComponent<Rigidbody2D>();
		reverMoveTime = 1 / moveTime;
	}

	public virtual void AttempMove<T>(int x,int y) where T : Component {
		
		RaycastHit2D hit;
		bool canMove = CanMove (x,y,out hit);

		if (hit.transform != null) {
			T componet = hit.transform.GetComponent<T> ();
			if (componet != null && !canMove) {
				CantMove<T> (componet);
			}
		}
	}

	public bool CanMove(int x,int y,out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (x,y);
		box2D.enabled = false;
		hit = Physics2D.Linecast (start,end,blockingLayer);
		box2D.enabled = true;
		if (hit.transform == null) {
			StartCoroutine (SmoothMove (end));
			return true;	
		} else {
			return false;
		}
	}

	IEnumerator SmoothMove(Vector3 end)
	{
		float sqrMagnitude = (transform.position - end).sqrMagnitude;

		while (sqrMagnitude > float.Epsilon) {
			Vector3 position = Vector3.MoveTowards (transform.position, end,reverMoveTime * Time.deltaTime);
			rb2D.MovePosition (position);
			sqrMagnitude = (position - end).sqrMagnitude;
			yield return null;
		}
	}

	public abstract void CantMove<T> (T componet);
}
