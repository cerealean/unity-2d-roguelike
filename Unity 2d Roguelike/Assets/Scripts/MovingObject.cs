using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    public float MoveTime = 0.1f;
    public LayerMask BlockingLayer;

    private BoxCollider2D BoxCollider;
    private Rigidbody2D RigidBody2d;
    private float InverseMoveTime;

	// Use this for initialization
	protected virtual void Start ()
	{
	    BoxCollider = GetComponent<BoxCollider2D>();
	    RigidBody2d = GetComponent<Rigidbody2D>();
	    InverseMoveTime = 1f/MoveTime;
	}

    protected bool Move(int xDirection, int yDirection, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDirection, yDirection);

        BoxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, BlockingLayer);
        BoxCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        var sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(RigidBody2d.position, end, InverseMoveTime * Time.deltaTime);
            RigidBody2d.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    protected virtual void AttemptMove<T>(int xDirection, int yDirection)
        where T : Component
    {
        RaycastHit2D hit;
        var canMove = Move(xDirection, yDirection, out hit);

        if (hit.transform == null)
        {
            return;
        }

        var hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}
