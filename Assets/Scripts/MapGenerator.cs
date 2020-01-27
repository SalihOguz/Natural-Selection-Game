﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform _mapTileParent;

    [SerializeField]
    private Transform _mapFeaturesPrefab;

    [SerializeField]
    private GameObject _mapTilePrefab;

    [SerializeField]
    private GameObject _treePrefab;

    [SerializeField]
    private GameObject _plantPrefab1;

    [SerializeField]
    private GameObject _plantPrefab2;

    [SerializeField]
    private GameObject _plantPrefab3;

    public int rowCount;
    public int columnCount;

    public int tileRowLength;
    public int tileColumnLength;

    public float waterPercentage = 0.2f;

    public int maxHeight = 10;
    public float noiseScale;

    [HideInInspector]
    public Vector3[,] cubePosList;
    public static CubeData[,] cubeDataList;
    private List<MapTile> _mapTileList = new List<MapTile>();
    public static MapGenerator Instance;
    
    private void Awake() {
        Instance = this;
    }

    private void Start() 
    {
        InitTileList();
    }

    public void GenerateMap()
    {
        float startTime = Time.realtimeSinceStartup;
        cubeDataList = new CubeData[rowCount, columnCount];
        GenerateHeight();

        int tileIndex = 0;
        for (int i = 0; i < rowCount / tileRowLength; i++)
        {
            for (int j = 0; j < columnCount / tileColumnLength; j++)
            {
                GenerateTile(i * tileRowLength, j * tileColumnLength, _mapTileList[tileIndex]);
                tileIndex++;
            }
        }

        print(rowCount*columnCount + " cubes created as " + tileRowLength + "X" + tileColumnLength + "tiles in " + (Time.realtimeSinceStartup - startTime));

        AnimalManager.Instance.SpawnAnimals();
        //Time.timeScale = 20f;
    }

    private void GenerateTile(int startX, int startY, MapTile tile)
    {
        int endX = Mathf.Clamp(startX + tileRowLength, 0, rowCount - 1);
        int endY = Mathf.Clamp(startY + tileColumnLength, 0, columnCount - 1);

        for (int i = startX; i < endX; i++)
        {
            for (int j = startY; j < endY; j++)
            {
                GenerateCube(tile, i, j);

                if (i == 0 || j == 0 || i == rowCount - 2 || j == columnCount - 2)
                {
                    for (int k = (int)(waterPercentage * maxHeight); k < cubePosList[i, j].y; k++)
                    {
                        GenerateFillingCube(tile, i, j, k);
                    }
                }

                if (i < rowCount - 1 && cubePosList[i, j].y > cubePosList[i + 1, j].y + 1)
                {
                    for (int k = (int)cubePosList[i + 1, j].y + 1; k < cubePosList[i, j].y; k++)
                    {
                        GenerateFillingCube(tile, i, j, k);
                    }
                }
                if (i > 0 && cubePosList[i, j].y > cubePosList[i - 1, j].y + 1)
                {
                    for (int k = (int)cubePosList[i - 1, j].y + 1; k < cubePosList[i, j].y; k++)
                    {
                        GenerateFillingCube(tile, i, j, k);
                    }
                }
                
                if (j < columnCount - 1 && cubePosList[i, j].y > cubePosList[i, j + 1].y + 1)
                {
                    for (int k = (int)cubePosList[i, j + 1].y + 1; k < cubePosList[i, j].y; k++)
                    {
                        GenerateFillingCube(tile, i, j, k);
                    }
                }
                if (j > 0 && cubePosList[i, j].y > cubePosList[i, j - 1].y + 1)
                {
                    for (int k = (int)cubePosList[i, j - 1].y + 1; k < cubePosList[i, j].y; k++)
                    {
                        GenerateFillingCube(tile, i, j, k);
                    }
                }
            }
        }
        tile.ApplyTile();
    }

    private void GenerateCube(MapTile tile, int i, int j)
    {
        CubeData cubeData = new CubeData();
        cubeData = ArrangeCubeSides(cubeData, i, j, (int)cubePosList[i, j].y);
        cubeData = ArrangeCubeType(cubeData, cubePosList[i, j].y);
        cubeData = AddCubeFeature(cubeData, i, j);
        cubeData.pos = cubePosList[i, j];
        cubeDataList[i, j] = cubeData;

        tile.PutCubeToTile(cubeData.pos, cubeData);
    }

    private void GenerateFillingCube(MapTile tile, int i, int j, int posY)
    {
        CubeData cubeData = new CubeData();
        cubeData = ArrangeCubeSides(cubeData, i, j, posY);
        cubeData = ArrangeCubeType(cubeData, posY);

        Vector3 pos = cubePosList[i, j];
        tile.PutCubeToTile(new Vector3(pos.x, posY, pos.z), cubeData);
    }

    private CubeData AddCubeFeature(CubeData cubeData, int i, int j)
    {
        if (cubeData.cubeType == CubeType.water)
        {
            return cubeData;
        }

        float rnd = Random.Range(0f, 100f);
        if (rnd < 1.5f && cubeData.cubeType == CubeType.grass)
        {
            cubeData.cubeFeature = CubeFeature.tree;

            Instantiate(_treePrefab, cubePosList[i, j], Quaternion.identity, _mapFeaturesPrefab);
        }
        else if (rnd < 2f)
        {
            cubeData.cubeFeature = CubeFeature.plant1;

            GameObject plant = Instantiate(_plantPrefab1, cubePosList[i, j], Quaternion.identity, _mapFeaturesPrefab);
            cubeData.standingPlant = plant.GetComponent<Plant>();
        }
        else if (rnd < 3f)
        {
            cubeData.cubeFeature = CubeFeature.plant2;

            GameObject plant = Instantiate(_plantPrefab2, cubePosList[i, j], Quaternion.identity, _mapFeaturesPrefab);
            cubeData.standingPlant = plant.GetComponent<Plant>();
        }
        else if (rnd < 3.5f)
        {
            cubeData.cubeFeature = CubeFeature.plant2;

            GameObject plant = Instantiate(_plantPrefab3, cubePosList[i, j], Quaternion.identity, _mapFeaturesPrefab);
            cubeData.standingPlant = plant.GetComponent<Plant>();
        }

        return cubeData;
    }

    private CubeData ArrangeCubeSides(CubeData cubeData, int i, int j, int posY)
    {
        if (i == 0)
        {
            cubeData.leftSide = true;

            if (posY > cubePosList[i + 1, j].y)
            {
                cubeData.rightSide = true;
            }
        }
        else if (i == rowCount - 2)
        {
            cubeData.rightSide = true;

            if (posY > cubePosList[i - 1, j].y)
            {
                cubeData.leftSide = true;
            }
        }
        else
        {
            if (posY > cubePosList[i + 1, j].y)
            {
                cubeData.rightSide = true;
            }
            if (posY > cubePosList[i - 1, j].y)
            {
                cubeData.leftSide = true;
            }
        }

        if (j == 0)
        {
            cubeData.backSide = true;

            if (posY > cubePosList[i, j + 1].y)
            {
                cubeData.frontSide = true;
            }
        }
        else if (j == columnCount - 2)
        {
            cubeData.frontSide = true;

            if (posY > cubePosList[i, j - 1].y)
            {
                cubeData.backSide = true;
            }
        }
        else
        {
            if (posY > cubePosList[i, j + 1].y)
            {
                cubeData.frontSide = true;
            }
            if (posY > cubePosList[i, j - 1].y)
            {
                cubeData.backSide = true;
            }
        }

        return cubeData;
    }

    private CubeData ArrangeCubeType(CubeData cubeData, float posY)
    {
        if (posY <= maxHeight * waterPercentage)
        {
            cubeData.cubeType = CubeType.water;
        }
        else if (posY < maxHeight * 0.3f)
        {
            cubeData.cubeType = CubeType.sand;
        }
        else if (posY< maxHeight * 0.7f)
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

        float offsetX = Random.Range(-90000, 90000);
        float offsetY = Random.Range(-90000, 90000);

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                float y = Mathf.Clamp(Mathf.PerlinNoise(offsetX + (float)i*noiseScale, offsetY + (float)j*noiseScale) * maxHeight, waterPercentage * maxHeight, maxHeight);
                cubePosList[i, j] = new Vector3(i, (int)y, j);
            }
        }
    }
}