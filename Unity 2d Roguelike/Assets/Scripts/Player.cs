using UnityEngine;
using System.Collections;

public class Player : MovingObject
{

    public int WallDamage = 1;
    public int PointsPerFood = 10;
    public int PointsPerSoda = 20;
    public float RestartLevelDelay = 1f;

    private Animator _animator;
    private int _food;

	// Use this for initialization
	protected override void Start ()
	{
	    _animator = GetComponent<Animator>();
	    _food = GameManager.Instance.PlayerFoodPoints;

        base.Start();
	}

    private void OnDisable()
    {
        GameManager.Instance.PlayerFoodPoints = _food;
    }

    protected override void OnCantMove<T>(T component)
    {
        throw new System.NotImplementedException();
    }

    protected override void AttemptMove<T>(int xDirection, int yDirection)
    {
        _food--;
        base.AttemptMove<T>(xDirection,yDirection);

        RaycastHit2D hit;
        CheckIfGameOver();

        GameManager.Instance.PlayersTurn = false;
    }

    // Update is called once per frame
	void Update ()
	{
	    if (!GameManager.Instance.PlayersTurn) return;

	    var horizontal = 0;
	    var vertical = 0;

	    horizontal = (int) Input;
	}

    private void CheckIfGameOver()
    {
        if (_food <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
