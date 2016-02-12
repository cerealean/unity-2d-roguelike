using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public int Columns = 8;
    public int Rows = 8;
    public Count WallCount = new Count(5,9);
    public Count FoodCount = new Count(1, 5);
    public GameObject Exit;
    public GameObject[] FloorTiles;
    public GameObject[] FoodTiles;
    public GameObject[] EnemyTiles;
    public GameObject[] InnerWallTiles;
    public GameObject[] OuterWallTiles;

    private Transform BoardHolder;
    private List<Vector3> GridPositions = new List<Vector3>();

    public void SetupScene(int level)
    {
        BoardSetup();
        IntializeList();
        LayoutObjectAtRandom(InnerWallTiles, WallCount.Minimum, WallCount.Maximum);
        LayoutObjectAtRandom(FoodTiles, FoodCount.Minimum, FoodCount.Maximum);
        var enemyCount = (int) Mathf.Log(level, 2f);
        LayoutObjectAtRandom(EnemyTiles, enemyCount, enemyCount);
        Instantiate(Exit, new Vector3(Columns - 1, Rows - 1, 0f), Quaternion.identity);
    }

    private void IntializeList()
    {
        GridPositions.Clear();

        for (var xAxis = 1; xAxis < Columns - 1; xAxis++)
        {
            for (var yAxis = 1; yAxis < Rows - 1; yAxis++)
            {
                GridPositions.Add(new Vector3(xAxis,yAxis,0f));
            }
        }
    }

    private void BoardSetup()
    {
        BoardHolder = new GameObject("Board").transform;
        var floorTileLength = FloorTiles.Length;
        var outerWallTileLength = OuterWallTiles.Length;

        for (var xAxis = -1; xAxis < Columns + 1; xAxis++)
        {
            for (var yAxis = -1; yAxis < Rows + 1; yAxis++)
            {
                var toInstantiate = IsOuterFloorTile(xAxis, yAxis) 
                    ? OuterWallTiles[Random.Range(0, outerWallTileLength)] 
                    : FloorTiles[Random.Range(0, floorTileLength)];

                var instance = Instantiate(toInstantiate, new Vector3(xAxis, yAxis,0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(BoardHolder);
            }
        }
    }

    private Vector3 RandomPosition()
    {
        var randomIndex = Random.Range(0, GridPositions.Count);
        Vector3 randomPosition = GridPositions[randomIndex];
        GridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }

    private void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        var objectCount = Random.Range(minimum, maximum + 1);

        for (var index = 0; index < objectCount; index++)
        {
            var randomPosition = RandomPosition();
            var tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    private bool IsOuterFloorTile(int xAxis, int yAxis)
    {
        return xAxis == -1 || xAxis == Columns || yAxis == -1 || yAxis == Rows;
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Serializable]
    public class Count
    {
        public int Minimum;
        public int Maximum;

        public Count(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
    }
}
