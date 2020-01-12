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

        float size = 1f;
		Vector3[] vertices = {
			new Vector3(0, size, 0),
			new Vector3(0, 0, 0),
			new Vector3(size, size, 0),
			new Vector3(size, 0, 0),

			new Vector3(0, 0, size),
			new Vector3(size, 0, size),
			new Vector3(0, size, size),
			new Vector3(size, size, size),

			new Vector3(0, size, 0),
			new Vector3(size, size, 0),

			new Vector3(0, size, 0),
			new Vector3(0, size, size),

			new Vector3(size, size, 0),
			new Vector3(size, size, size),
		};

		int[] triangles = {
			0, 2, 1, // front
			1, 2, 3,
			4, 5, 6, // back
			5, 7, 6,
			6, 7, 8, //top
			7, 9 ,8, 
			1, 3, 4, //bottom
			3, 5, 4,
			1, 11,10,// left
			1, 4, 11,
			3, 12, 5,//right
			5, 12, 13


		};

		float startX = 0;
		float startY = 1;

		Vector2[] uvs = {
			new Vector2(startX, startY), // 0
			new Vector2(startX + 0.5f, startY),
			new Vector2(startX, startY - 0.5f),
			new Vector2(startX + 0.5f, startY - 0.5f),

			new Vector2(startX, startY), // 4
			new Vector2(startX, startY - 0.5f),
			new Vector2(startX + 0.5f, startY),
			new Vector2(startX + 0.5f, startY - 0.5f),

			new Vector2(startX, startY), // 8
			new Vector2(startX, startY - 0.5f),

			new Vector2(startX + 0.5f, startY - 0.5f),
			new Vector2(startX, startY - 0.5f),

			new Vector2(startX + 0.5f, startY),
			new Vector2(startX, startY),
		};

        mesh.vertices = vertices;
		mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.Optimize ();
		mesh.RecalculateNormals ();
    }
}