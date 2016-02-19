using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{
    public float TurnDelay = .1f;
    public static GameManager Instance = null;
    public BoardManager BoardScript;
    public int PlayerFoodPoints = 100;
    [HideInInspector] public bool PlayersTurn = true;
    private int _level = 3;
    private List<Enemy> _enemies;
    private bool EnemiesMoving;
    

	// Use this for initialization
	void Awake () {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        _enemies = new List<Enemy>();
        BoardScript = GetComponent<BoardManager>();
        InitGame();
	}

    private void InitGame()
    {
        _enemies.Clear();
        BoardScript.SetupScene(_level);
    }

    public void GameOver()
    {
        enabled = false;
    }

    private IEnumerator MoveEnemies()
    {
        EnemiesMoving = true;
        yield return new WaitForSeconds(TurnDelay);
        if (_enemies.Count == 0)
        {
            yield return new WaitForSeconds(TurnDelay);
        }

        foreach (var enemy in _enemies)
        {
            enemy.MoveEnemy();
            yield return new WaitForSeconds(enemy.MoveTime);
        }

        PlayersTurn = true;
        EnemiesMoving = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (PlayersTurn || EnemiesMoving)
	    {
	        return;
	    }

	    StartCoroutine(MoveEnemies());
	}

    public void AddEnemyToList(Enemy script)
    {
        _enemies.Add(script);
    }
}
