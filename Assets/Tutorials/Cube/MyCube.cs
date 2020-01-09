using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MyCube : MonoBehaviour {
    private Vector3[] vertices;
    private Mesh mesh;

     private void Awake () {
		Generate();
	}

    private void Generate () {
        // add mesh
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";

        vertices = new Vector3[8];
        
        for (int vi = 0, x = 0; x < 2; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                for (int z = 0; z < 2; z++)
                {
                    vertices[vi++] = new Vector3(x, y, z);
                }
            }
        }

        mesh.vertices = vertices;


        int[] triangles = new int[36];

        // left side
        triangles[0] = 0;
		triangles[1] = triangles[4] = 1;
		triangles[2] = triangles[3] = 2;
		triangles[5] = 3;

        // right side
        triangles[6] = 7;
		triangles[8] = triangles[9] = 6;
		triangles[7] = triangles[10] = 5;
		triangles[11] = 4;

        // front side
        triangles[12] = 0;
		triangles[14] = triangles[15] = 4;
		triangles[13] = triangles[16] = 2;
		triangles[17] = 6;

        // top side
        triangles[18] = 3;
		triangles[19] = triangles[22] = 7;
		triangles[20] = triangles[21] = 2;
		triangles[23] = 6;

        // bottom side
        triangles[24] = 1;
		triangles[25] = triangles[28] = 0;
		triangles[26] = triangles[27] = 5;
		triangles[29] = 4;

        // back side
        triangles[30] = 3;
		triangles[31] = triangles[34] = 1;
		triangles[32] = triangles[33] = 7;
		triangles[35] = 5;

		mesh.triangles = triangles;
    }
}