using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MapTile : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices = new Vector3[0];
    private int[] triangles = new int[0];
    private Vector2[] uv = new Vector2[0];

    private void Awake()
    {
        // add mesh
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
    }

    public void PutCubeToTile(Vector3 startPos, CubeData cubeData)
    {
        #region Add Vertices

        // init vertex list
        int vertexCount = vertices.Length + GetVertexCount(cubeData);
        Vector3[] newVertices = new Vector3[vertexCount];
        for (int i = 0; i < vertices.Length; i++)
        {
            newVertices[i] = vertices[i];
        }

        float side = cubeData.size / 2f;
        int currentVertexIndex = vertices.Length;
        int leftStartIndex = 0, rightStartIndex = 0, frontStartIndex = 0, backStartIndex = 0, topStartIndex = 0, bottomStartIndex = 0;

        // research done
        
        if (cubeData.leftSide)
        {
            leftStartIndex = currentVertexIndex;
            newVertices[currentVertexIndex] = startPos + new Vector3(-side, side, side);
            newVertices[currentVertexIndex + 1] = startPos + new Vector3(-side, -side, side);
            newVertices[currentVertexIndex + 2] = startPos + new Vector3(-side, side, -side);
            newVertices[currentVertexIndex + 3] = startPos + new Vector3(-side, -side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.frontSide)
        {
            frontStartIndex = currentVertexIndex;
            newVertices[currentVertexIndex] = startPos + new Vector3(-side, side, side);
            newVertices[currentVertexIndex + 1] = startPos + new Vector3(side, side, side);
            newVertices[currentVertexIndex + 2] = startPos + new Vector3(-side, -side, side);
            newVertices[currentVertexIndex + 3] = startPos + new Vector3(side, -side, side);
            currentVertexIndex += 4;
        }

        if (cubeData.backSide)
        {
            backStartIndex = currentVertexIndex;
            newVertices[currentVertexIndex] = startPos + new Vector3(-side, -side, -side);
            newVertices[currentVertexIndex + 1] = startPos + new Vector3(side, -side, -side);
            newVertices[currentVertexIndex + 2] = startPos + new Vector3(-side, side, -side);
            newVertices[currentVertexIndex + 3] = startPos + new Vector3(side, side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.rightSide)
        {
            rightStartIndex = currentVertexIndex;
            newVertices[currentVertexIndex] = startPos + new Vector3(side, -side, side);
            newVertices[currentVertexIndex + 1] = startPos + new Vector3(side, side, side);
            newVertices[currentVertexIndex + 2] = startPos + new Vector3(side, -side, -side);
            newVertices[currentVertexIndex + 3] = startPos + new Vector3(side, side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.topSide)
        {
            topStartIndex = currentVertexIndex;
            newVertices[currentVertexIndex] = startPos + new Vector3(side, side, side);
            newVertices[currentVertexIndex + 1] = startPos + new Vector3(-side, side, side);
            newVertices[currentVertexIndex + 2] = startPos + new Vector3(side, side, -side);
            newVertices[currentVertexIndex + 3] = startPos + new Vector3(-side, side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.bottomSide)
        {
            bottomStartIndex = currentVertexIndex;
            newVertices[currentVertexIndex] = startPos + new Vector3(-side, -side, side);
            newVertices[currentVertexIndex + 1] = startPos + new Vector3(side, -side, side);
            newVertices[currentVertexIndex + 2] = startPos + new Vector3(-side, -side, -side);
            newVertices[currentVertexIndex + 3] = startPos + new Vector3(side, -side, -side);
            currentVertexIndex += 4;
        }
        #endregion
        
        #region Add Triangles
        int triangleCount = triangles.Length + GetTriangleCount(cubeData);
        int[] newTriangles = new int[triangleCount];
        for (int i = 0; i < triangles.Length; i++)
        {
            newTriangles[i] = triangles[i];
        }

        currentVertexIndex = triangles.Length;
    
        if (cubeData.leftSide)
        {
            newTriangles[currentVertexIndex] = leftStartIndex + 2;
            newTriangles[currentVertexIndex + 1] = leftStartIndex + 1;
            newTriangles[currentVertexIndex + 2] = leftStartIndex;

            newTriangles[currentVertexIndex + 3] = leftStartIndex + 2;
            newTriangles[currentVertexIndex + 4] = leftStartIndex + 3;
            newTriangles[currentVertexIndex + 5] = leftStartIndex + 1;
            currentVertexIndex += 6;
        }

        if (cubeData.frontSide)
        {
            newTriangles[currentVertexIndex] = frontStartIndex + 2;
            newTriangles[currentVertexIndex + 1] = frontStartIndex + 1;
            newTriangles[currentVertexIndex + 2] = frontStartIndex;

            newTriangles[currentVertexIndex + 3] = frontStartIndex + 2;
            newTriangles[currentVertexIndex + 4] = frontStartIndex + 3;
            newTriangles[currentVertexIndex + 5] = frontStartIndex + 1;
            currentVertexIndex += 6;
        }

        if (cubeData.backSide)
        {
            newTriangles[currentVertexIndex] = backStartIndex + 2;
            newTriangles[currentVertexIndex + 1] = backStartIndex + 1;
            newTriangles[currentVertexIndex + 2] = backStartIndex;

            newTriangles[currentVertexIndex + 3] = backStartIndex + 2;
            newTriangles[currentVertexIndex + 4] = backStartIndex + 3;
            newTriangles[currentVertexIndex + 5] = backStartIndex + 1;
            currentVertexIndex += 6;
        }

        if (cubeData.rightSide)
        {
            newTriangles[currentVertexIndex] = rightStartIndex + 2;
            newTriangles[currentVertexIndex + 1] = rightStartIndex + 1;
            newTriangles[currentVertexIndex + 2] = rightStartIndex;

            newTriangles[currentVertexIndex + 3] = rightStartIndex + 2;
            newTriangles[currentVertexIndex + 4] = rightStartIndex + 3;
            newTriangles[currentVertexIndex + 5] = rightStartIndex + 1;
            currentVertexIndex += 6;
        }

        if (cubeData.topSide)
        {
            newTriangles[currentVertexIndex] = topStartIndex + 2;
            newTriangles[currentVertexIndex + 1] = topStartIndex + 1;
            newTriangles[currentVertexIndex + 2] = topStartIndex;

            newTriangles[currentVertexIndex + 3] = topStartIndex + 2;
            newTriangles[currentVertexIndex + 4] = topStartIndex + 3;
            newTriangles[currentVertexIndex + 5] = topStartIndex + 1;
            currentVertexIndex += 6;
        }

        if (cubeData.bottomSide)
        {
            newTriangles[currentVertexIndex] = bottomStartIndex + 2;
            newTriangles[currentVertexIndex + 1] = bottomStartIndex + 1;
            newTriangles[currentVertexIndex + 2] = bottomStartIndex;

            newTriangles[currentVertexIndex + 3] = bottomStartIndex + 2;
            newTriangles[currentVertexIndex + 4] = bottomStartIndex + 3;
            newTriangles[currentVertexIndex + 5] = bottomStartIndex + 1;
            currentVertexIndex += 6;
        }
        #endregion

        #region Add UV

        float startX = GetUVStartCoord(cubeData.cubeType)[0];
        float startY = GetUVStartCoord(cubeData.cubeType)[1];

        Vector2[] newUv = new Vector2[vertexCount];
        
        for (int i = 0; i < uv.Length; i++)
        {
            newUv[i] = uv[i];
        }

        currentVertexIndex = uv.Length;
        if (cubeData.leftSide)
        {
            newUv[currentVertexIndex] = new Vector2(startX, startY);
            newUv[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            newUv[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            newUv[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }

        if (cubeData.frontSide)
        {
            newUv[currentVertexIndex] = new Vector2(startX, startY);
            newUv[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            newUv[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            newUv[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }

        if (cubeData.backSide)
        {
            newUv[currentVertexIndex] = new Vector2(startX, startY);
            newUv[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            newUv[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            newUv[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }

        if (cubeData.rightSide)
        {
            newUv[currentVertexIndex] = new Vector2(startX, startY);
            newUv[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            newUv[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            newUv[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }

        if (cubeData.topSide)
        {
            newUv[currentVertexIndex] = new Vector2(startX, startY);
            newUv[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            newUv[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            newUv[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }

        if (cubeData.bottomSide)
        {
            newUv[currentVertexIndex] = new Vector2(startX, startY);
            newUv[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            newUv[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            newUv[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }
        #endregion

        vertices = newVertices;
        triangles = newTriangles;
        uv = newUv;
    }

    public void ApplyTile()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

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

    private float[] GetUVStartCoord(CubeType cubeType)
    {
        if (cubeType == CubeType.dirt)
        {
            return new float[2]{0, 1};
        }
        else if (cubeType == CubeType.sand)
        {
            return new float[2]{0, 0.5f};
        }
        else if (cubeType == CubeType.grass)
        {
            return new float[2]{0.5f, 1};
        }
        else if (cubeType == CubeType.water)
        {
            return new float[2]{0.5f, 0.5f};
        }

        return new float[2]{0, 1};
    }
}