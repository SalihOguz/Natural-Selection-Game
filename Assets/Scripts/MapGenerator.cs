using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private MultipleCube _cube;

    public int rowCount;
    public int columnCount;

    private Vector3[,] cubePosList;

    private void Start() 
    {
        float startTime = Time.time;
        cubePosList = new Vector3[rowCount, columnCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                float y = Mathf.PerlinNoise((float)i/8, (float)j/8) * 10;
                print(y + " " + (int)y);
                cubePosList[i, j] = new Vector3(i, (int)y, j);
            }
        }

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                CubeData cubeData = new CubeData{

                };

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
                    else if (cubePosList[i, j].y > cubePosList[i - 1, j].y)
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
                    else if (cubePosList[i, j].y > cubePosList[i, j - 1].y)
                    {
                        cubeData.backSide = true;
                    }
                }

                _cube.PutCube(cubePosList[i, j], cubeData);
            }
        }

        print(Time.time - startTime);
    }
}