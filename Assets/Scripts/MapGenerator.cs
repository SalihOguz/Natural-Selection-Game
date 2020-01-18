using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform _mapTileParent;

    [SerializeField]
    private GameObject _mapTilePrefab;

    public int rowCount;
    public int columnCount;

    public int tileRowLength;
    public int tileColumnLength;

    private Vector3[,] cubePosList;
    private List<MapTile> _mapTileList = new List<MapTile>();
    

    private void Start() 
    {
        InitTileList();
    }

    public void GenerateMap()
    {
        float startTime = Time.realtimeSinceStartup;
        GenerateHeight();

        int tileIndex = 0;
        for (int i = 0; i < rowCount / tileRowLength; i++)
        {
            for (int j = 0; j < columnCount / tileColumnLength; j++)
            {
                StartCoroutine(GenerateTile(i * tileRowLength, j * tileColumnLength, _mapTileList[tileIndex]));
                tileIndex++;
            }
        }

        print(rowCount*columnCount + " cubes created as " + tileRowLength + "X" + tileColumnLength + "tiles in " + (Time.realtimeSinceStartup - startTime));
    }

    private IEnumerator GenerateTile(int startX, int startY, MapTile tile)
    {
        yield return new WaitForEndOfFrame();
        for (int i = startX; i < Mathf.Clamp(startX + tileRowLength, 0, rowCount - 1); i++)
        {
            for (int j = startY; j < Mathf.Clamp(startY + tileColumnLength, 0, columnCount - 1); j++)
            {
                CubeData cubeData = new CubeData();
                cubeData = ArrangeCubeSides(cubeData, i, j);
                cubeData = ArrangeCubeType(cubeData, i, j);

                tile.PutTile(cubePosList[i, j], cubeData);
            }
        }
    }

    private CubeData ArrangeCubeSides(CubeData cubeData, int i, int j)
    {
        if (i == 0)
        {
            cubeData.leftSide = true;

            if (cubePosList[i, j].y > cubePosList[i + 1, j].y)
            {
                cubeData.rightSide = true;
            }
        }
        else if (i == rowCount - 1)
        {
            cubeData.rightSide = true;

            if (cubePosList[i, j].y > cubePosList[i - 1, j].y)
            {
                cubeData.leftSide = true;
            }
        }
        else
        {
            if (cubePosList[i, j].y > cubePosList[i + 1, j].y)
            {
                cubeData.rightSide = true;
            }
            if (cubePosList[i, j].y > cubePosList[i - 1, j].y)
            {
                cubeData.leftSide = true;
            }
        }

        if (j == 0)
        {
            cubeData.backSide = true;

            if (cubePosList[i, j].y > cubePosList[i, j + 1].y)
            {
                cubeData.frontSide = true;
            }
        }
        else if (j == columnCount - 1)
        {
            cubeData.frontSide = true;

            if (cubePosList[i, j].y > cubePosList[i, j - 1].y)
            {
                cubeData.backSide = true;
            }
        }
        else
        {
            if (cubePosList[i, j].y > cubePosList[i, j + 1].y)
            {
                cubeData.frontSide = true;
            }
            if (cubePosList[i, j].y > cubePosList[i, j - 1].y)
            {
                cubeData.backSide = true;
            }
        }

        return cubeData;
    }

    private CubeData ArrangeCubeType(CubeData cubeData, int i, int j)
    {
        if (cubePosList[i, j].y < 2)
        {
            cubeData.cubeType = CubeType.water;
        }
        else if (cubePosList[i, j].y < 4)
        {
            cubeData.cubeType = CubeType.sand;
        }
        else if (cubePosList[i, j].y < 7)
        {
            cubeData.cubeType = CubeType.grass;
        }
        else
        {
            cubeData.cubeType = CubeType.dirt;
        }

        return cubeData;
    }

    private void InitTileList()
    {
        foreach (Transform tile in _mapTileParent)
        {
            _mapTileList.Add(tile.GetComponent<MapTile>());
        }

        int requiredTileCount = (rowCount / tileRowLength) * (columnCount / tileColumnLength);

        for (int i = 0; i < requiredTileCount; i++)
        {
            GameObject tile = Instantiate(_mapTilePrefab, Vector3.zero, Quaternion.identity, _mapTileParent);
            _mapTileList.Add(tile.GetComponent<MapTile>());
        }
    }

    private void GenerateHeight()
    {
        cubePosList = new Vector3[rowCount, columnCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                float y = Mathf.PerlinNoise((float)i/10, (float)j/10) * 10;
                cubePosList[i, j] = new Vector3(i, (int)y, j);
            }
        }
    }
}