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
        cubePosList = new Vector3[rowCount, columnCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                cubePosList[i, j] = new Vector3(i, 0, j);
            }
        }

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                CubeData cubeData = new CubeData{
                    frontSide = true,
                    backSide = true,
                    leftSide = true,
                    rightSide = true,
                    topSide = true,
                    bottomSide = false,
                    cubeType = CubeType.dirt
                };

                _cube.PutCube(cubePosList[i, j], cubeData);
            }
        }

    }
}