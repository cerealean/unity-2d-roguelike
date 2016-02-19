using UnityEngine;
using System.Collections;

public class Enemy : MovingObject
{

    public int PlayerDamage;

    private Animator _animator;
    private Transform _target;
    private bool _skipMove;

	protected override void Start ()
	{
        GameManager.Instance.AddEnemyToList(this);
	    _animator = GetComponent<Animator>();
	    _target = GameObject.FindGameObjectWithTag("Player").transform;
	    base.Start();
	}

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;
        _animator.SetTrigger("EnemyAttack");
        hitPlayer.LoseFood(PlayerDamage);
    }

    protected override void AttemptMove<T>(int xDirection, int yDirection)
    {
        if (_skipMove)
        {
            _skipMove = false;
            return;
        }

        base.AttemptMove<T>(xDirection,yDirection);
        _skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDirection = 0;
        int yDirection = 0;

        if (Mathf.Abs(_target.position.x - transform.position.x) < float.Epsilon)
        {
            yDirection = _target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            xDirection = _target.position.x > transform.position.x ? 1 : -1;
        }

        AttemptMove<Player>(xDirection,yDirection);
    }
}
