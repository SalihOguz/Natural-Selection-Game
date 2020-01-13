using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MyNewCube : MonoBehaviour
{
    private Vector3[] vertices;
    private Mesh mesh;

    private void Awake()
    {
        CubeData cubeData = new CubeData{
            forwardSide = true,
            backSide = true,
            leftSide = true,
            rightSide = true
        };

        PutCube(Vector3.zero, cubeData);
    }

    private void PutCube(Vector3 startPos, CubeData cubeData)
    {
        // add mesh
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        float side = cubeData.size / 2f;
        int vertexCount = GetVertexCount(cubeData);

        Vector3[] vertices = new Vector3[vertexCount];
        int currentVertexIndex = 0;

        if (cubeData.leftSide)
        {
            vertices[currentVertexIndex] = new Vector3(-side, side, side);
            
        }

        {
            
            new Vector3(-side, -side, side),
            new Vector3(-side, side, -side),
            new Vector3(-side, -side, -side),

            // new Vector3(0, 0, size),
            // new Vector3(size, 0, size),
            // new Vector3(0, size, size),
            // new Vector3(size, size, size),

            // new Vector3(0, size, 0),
            // new Vector3(size, size, 0),

            // new Vector3(0, size, 0),
            // new Vector3(0, size, size),

            // new Vector3(size, size, 0),
            // new Vector3(size, size, size),
        };

        int[] triangles = {
            2, 1, 0, // left
			2, 3, 1,
            // 4, 5, 6, // back
			// 5, 7, 6,
            // 6, 7, 8, //top
			// 7, 9 ,8,
            // 1, 3, 4, //bottom
			// 3, 5, 4,
            // 1, 11,10,// left
			// 1, 4, 11,
            // 3, 12, 5,//right
			// 5, 12, 13
        };

        // float startX = 0;
        // float startY = 1;

        // Vector2[] uvs = {
        //     new Vector2(startX, startY), // 0
		// 	new Vector2(startX + 0.5f, startY),
        //     new Vector2(startX, startY - 0.5f),
        //     new Vector2(startX + 0.5f, startY - 0.5f),

        //     new Vector2(startX, startY), // 4
		// 	new Vector2(startX, startY - 0.5f),
        //     new Vector2(startX + 0.5f, startY),
        //     new Vector2(startX + 0.5f, startY - 0.5f),

        //     new Vector2(startX, startY), // 8
		// 	new Vector2(startX, startY - 0.5f),

        //     new Vector2(startX + 0.5f, startY - 0.5f),
        //     new Vector2(startX, startY - 0.5f),

        //     new Vector2(startX + 0.5f, startY),
        //     new Vector2(startX, startY),
        // };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //mesh.uv = uvs;

        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    private int GetVertexCount(CubeData cubeData)
    {
        int vertexCount = 0;
        if (cubeData.forwardSide)
        {
            vertexCount += 4;
        }

        if (cubeData.backSide)
        {
            vertexCount += 4;
        }

        if (cubeData.leftSide)
        {
            vertexCount += 4;
        }

        if (cubeData.rightSide)
        {
            vertexCount += 4;
        }

        return vertexCount;
    }
}