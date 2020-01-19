using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MapTile : MonoBehaviour
{
    private Mesh mesh;

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
        int vertexCount = mesh.vertices.Length + GetVertexCount(cubeData);
        Vector3[] vertices = new Vector3[vertexCount];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            vertices[i] = mesh.vertices[i];
        }

        float side = cubeData.size / 2f;
        int currentVertexIndex = mesh.vertices.Length;
        int leftStartIndex = 0, rightStartIndex = 0, frontStartIndex = 0, backStartIndex = 0, topStartIndex = 0, bottomStartIndex = 0;

        // research done
        
        if (cubeData.leftSide)
        {
            leftStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = startPos + new Vector3(-side, side, side);
            vertices[currentVertexIndex + 1] = startPos + new Vector3(-side, -side, side);
            vertices[currentVertexIndex + 2] = startPos + new Vector3(-side, side, -side);
            vertices[currentVertexIndex + 3] = startPos + new Vector3(-side, -side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.frontSide)
        {
            frontStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = startPos + new Vector3(-side, side, side);
            vertices[currentVertexIndex + 1] = startPos + new Vector3(side, side, side);
            vertices[currentVertexIndex + 2] = startPos + new Vector3(-side, -side, side);
            vertices[currentVertexIndex + 3] = startPos + new Vector3(side, -side, side);
            currentVertexIndex += 4;
        }

        if (cubeData.backSide)
        {
            backStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = startPos + new Vector3(-side, -side, -side);
            vertices[currentVertexIndex + 1] = startPos + new Vector3(side, -side, -side);
            vertices[currentVertexIndex + 2] = startPos + new Vector3(-side, side, -side);
            vertices[currentVertexIndex + 3] = startPos + new Vector3(side, side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.rightSide)
        {
            rightStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = startPos + new Vector3(side, -side, side);
            vertices[currentVertexIndex + 1] = startPos + new Vector3(side, side, side);
            vertices[currentVertexIndex + 2] = startPos + new Vector3(side, -side, -side);
            vertices[currentVertexIndex + 3] = startPos + new Vector3(side, side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.topSide)
        {
            topStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = startPos + new Vector3(side, side, side);
            vertices[currentVertexIndex + 1] = startPos + new Vector3(-side, side, side);
            vertices[currentVertexIndex + 2] = startPos + new Vector3(side, side, -side);
            vertices[currentVertexIndex + 3] = startPos + new Vector3(-side, side, -side);
            currentVertexIndex += 4;
        }

        if (cubeData.bottomSide)
        {
            bottomStartIndex = currentVertexIndex;
            vertices[currentVertexIndex] = startPos + new Vector3(-side, -side, side);
            vertices[currentVertexIndex + 1] = startPos + new Vector3(side, -side, side);
            vertices[currentVertexIndex + 2] = startPos + new Vector3(-side, -side, -side);
            vertices[currentVertexIndex + 3] = startPos + new Vector3(side, -side, -side);
            currentVertexIndex += 4;
        }
        #endregion
        
        #region Add Triangles
        int triangleCount = mesh.triangles.Length + GetTriangleCount(cubeData);
        int[] triangles = new int[triangleCount];
        for (int i = 0; i < mesh.triangles.Length; i++)
        {
            triangles[i] = mesh.triangles[i];
        }

        currentVertexIndex = mesh.triangles.Length;
    
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
        #endregion

        #region Add UV

        float startX = GetUVStartCoord(cubeData.cubeType)[0];
        float startY = GetUVStartCoord(cubeData.cubeType)[1];

        Vector2[] uvs = new Vector2[vertexCount];
        
        for (int i = 0; i < mesh.uv.Length; i++)
        {
            uvs[i] = mesh.uv[i];
        }

        currentVertexIndex = mesh.uv.Length;
        if (cubeData.leftSide)
        {
            uvs[currentVertexIndex] = new Vector2(startX, startY);
            uvs[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            uvs[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            uvs[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }

        if (cubeData.frontSide)
        {
            uvs[currentVertexIndex] = new Vector2(startX, startY);
            uvs[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            uvs[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            uvs[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }

        if (cubeData.backSide)
        {
            uvs[currentVertexIndex] = new Vector2(startX, startY);
            uvs[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            uvs[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            uvs[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }

        if (cubeData.rightSide)
        {
            uvs[currentVertexIndex] = new Vector2(startX, startY);
            uvs[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            uvs[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            uvs[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }

        if (cubeData.topSide)
        {
            uvs[currentVertexIndex] = new Vector2(startX, startY);
            uvs[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            uvs[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            uvs[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }

        if (cubeData.bottomSide)
        {
            uvs[currentVertexIndex] = new Vector2(startX, startY);
            uvs[currentVertexIndex + 1] = new Vector2(startX + 0.5f, startY);
            uvs[currentVertexIndex + 2] =  new Vector2(startX, startY - 0.5f);
            uvs[currentVertexIndex + 3] =  new Vector2(startX + 0.5f, startY - 0.5f);
            currentVertexIndex += 4;
        }
        #endregion

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

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