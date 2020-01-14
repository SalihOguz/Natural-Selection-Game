using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MyNewCube : MonoBehaviour
{
    private Vector3[] vertices;
    private Mesh mesh;

    private void Awake()
    {
        CubeData cubeData = new CubeData{
            frontSide = true,
            backSide = true,
            leftSide = true,
            rightSide = true,
            topSide = true,
            bottomSide = true
        };

        PutCube(Vector3.zero, cubeData);
    }

    private void PutCube(Vector3 startPos, CubeData cubeData)
    {
        // add mesh
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        // add vertices
        int vertexCount = GetVertexCount(cubeData);
        Vector3[] vertices = new Vector3[vertexCount];
        float side = cubeData.size / 2f;
        int currentVertexIndex = 0;
        int leftStartIndex = 0, rightStartIndex = 0, frontStartIndex = 0, backStartIndex = 0, topStartIndex = 0, bottomStartIndex = 0;
        
        if (cubeData.leftSide)
        {
            leftStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = new Vector3(-side, side, side);
            vertices[currentVertexIndex + 1] = new Vector3(-side, -side, side);
            vertices[currentVertexIndex + 2] = new Vector3(-side, side, -side);
            vertices[currentVertexIndex + 3] = new Vector3(-side, -side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.frontSide)
        {
            frontStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = new Vector3(-side, side, side);
            vertices[currentVertexIndex + 1] = new Vector3(side, side, side);
            vertices[currentVertexIndex + 2] = new Vector3(-side, -side, side);
            vertices[currentVertexIndex + 3] = new Vector3(side, -side, side);
            currentVertexIndex += 4;
        }

        if (cubeData.backSide)
        {
            backStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = new Vector3(-side, -side, -side);
            vertices[currentVertexIndex + 1] = new Vector3(side, -side, -side);
            vertices[currentVertexIndex + 2] = new Vector3(-side, side, -side);
            vertices[currentVertexIndex + 3] = new Vector3(side, side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.rightSide)
        {
            rightStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = new Vector3(side, -side, side);
            vertices[currentVertexIndex + 1] = new Vector3(side, side, side);
            vertices[currentVertexIndex + 2] = new Vector3(side, -side, -side);
            vertices[currentVertexIndex + 3] = new Vector3(side, side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.topSide)
        {
            topStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = new Vector3(side, side, side);
            vertices[currentVertexIndex + 1] = new Vector3(-side, side, side);
            vertices[currentVertexIndex + 2] = new Vector3(side, side, -side);
            vertices[currentVertexIndex + 3] = new Vector3(-side, side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.bottomSide)
        {
            bottomStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = new Vector3(-side, -side, side);
            vertices[currentVertexIndex + 1] = new Vector3(side, -side, side);
            vertices[currentVertexIndex + 2] = new Vector3(-side, -side, -side);
            vertices[currentVertexIndex + 3] = new Vector3(side, -side, -side);
            currentVertexIndex += 4;
        }
        
        // add triangles
        int triangleCount = GetTriangleCount(cubeData);
        int[] triangles = new int[triangleCount];
        currentVertexIndex = 0;
        if (cubeData.leftSide)
        {
            triangles[currentVertexIndex] = leftStartIndex + 2;
            triangles[currentVertexIndex + 1] = leftStartIndex + 1;
            triangles[currentVertexIndex + 2] = leftStartIndex;

            triangles[currentVertexIndex + 3] = leftStartIndex + 2;
            triangles[currentVertexIndex + 4] = leftStartIndex + 3;
            triangles[currentVertexIndex + 5] = leftStartIndex + 1;
            currentVertexIndex += 6;
        }

        if (cubeData.frontSide)
        {
            triangles[currentVertexIndex] = frontStartIndex + 2;
            triangles[currentVertexIndex + 1] = frontStartIndex + 1;
            triangles[currentVertexIndex + 2] = frontStartIndex;

            triangles[currentVertexIndex + 3] = frontStartIndex + 2;
            triangles[currentVertexIndex + 4] = frontStartIndex + 3;
            triangles[currentVertexIndex + 5] = frontStartIndex + 1;
            currentVertexIndex += 6;
        }

        if (cubeData.backSide)
        {
            triangles[currentVertexIndex] = backStartIndex + 2;
            triangles[currentVertexIndex + 1] = backStartIndex + 1;
            triangles[currentVertexIndex + 2] = backStartIndex;

            triangles[currentVertexIndex + 3] = backStartIndex + 2;
            triangles[currentVertexIndex + 4] = backStartIndex + 3;
            triangles[currentVertexIndex + 5] = backStartIndex + 1;
            currentVertexIndex += 6;
        }

        if (cubeData.rightSide)
        {
            triangles[currentVertexIndex] = rightStartIndex + 2;
            triangles[currentVertexIndex + 1] = rightStartIndex + 1;
            triangles[currentVertexIndex + 2] = rightStartIndex;

            triangles[currentVertexIndex + 3] = rightStartIndex + 2;
            triangles[currentVertexIndex + 4] = rightStartIndex + 3;
            triangles[currentVertexIndex + 5] = rightStartIndex + 1;
            currentVertexIndex += 6;
        }

        if (cubeData.topSide)
        {
            triangles[currentVertexIndex] = topStartIndex + 2;
            triangles[currentVertexIndex + 1] = topStartIndex + 1;
            triangles[currentVertexIndex + 2] = topStartIndex;

            triangles[currentVertexIndex + 3] = topStartIndex + 2;
            triangles[currentVertexIndex + 4] = topStartIndex + 3;
            triangles[currentVertexIndex + 5] = topStartIndex + 1;
            currentVertexIndex += 6;
        }

        if (cubeData.bottomSide)
        {
            triangles[currentVertexIndex] = bottomStartIndex + 2;
            triangles[currentVertexIndex + 1] = bottomStartIndex + 1;
            triangles[currentVertexIndex + 2] = bottomStartIndex;

            triangles[currentVertexIndex + 3] = bottomStartIndex + 2;
            triangles[currentVertexIndex + 4] = bottomStartIndex + 3;
            triangles[currentVertexIndex + 5] = bottomStartIndex + 1;
            currentVertexIndex += 6;
        }



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
        if (cubeData.frontSide)
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

        if (cubeData.topSide)
        {
            vertexCount += 4;
        }

        if (cubeData.bottomSide)
        {
            vertexCount += 4;
        }

        return vertexCount;
    }

    private int GetTriangleCount(CubeData cubeData)
    {
        int vertexCount = 0;
        if (cubeData.frontSide)
        {
            vertexCount += 6;
        }

        if (cubeData.backSide)
        {
            vertexCount += 6;
        }

        if (cubeData.leftSide)
        {
            vertexCount += 6;
        }

        if (cubeData.rightSide)
        {
            vertexCount += 6;
        }
        
        if (cubeData.topSide)
        {
            vertexCount += 6;
        }

        if (cubeData.bottomSide)
        {
            vertexCount += 6;
        }

        return vertexCount;
    }
}