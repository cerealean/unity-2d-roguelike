using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    public BoardManager BoardScript;
    public int PlayerFoodPoints = 100;
    private int level = 3;
    [HideInInspector] public bool PlayersTurn = true;
    

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
        BoardScript = GetComponent<BoardManager>();
        InitGame();
	}

    private void InitGame()
    {
        BoardScript.SetupScene(level);
    }

    public void GameOver()
    {
        enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
