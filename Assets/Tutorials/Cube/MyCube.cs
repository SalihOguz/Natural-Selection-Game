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

        int[] triangles = new int[6];
		// for (int ti = 0, vi = 0, y = 0; y < 2; y++, vi++) {
		// 	for (int x = 0; x < 2; x++, ti += 6, vi++) {
		// 		triangles[ti] = vi;
		// 		triangles[ti + 3] = triangles[ti + 2] = vi + 1;
		// 		triangles[ti + 4] = triangles[ti + 1] = vi + 2 + 1;
		// 		triangles[ti + 5] = vi + 2 + 2;
		// 	}
		// }

        triangles[0] = 0;
		triangles[1] = triangles[4] = 1;
		triangles[2] = triangles[3] = 2;
		triangles[5] = 3;

		mesh.triangles = triangles;
    }
}