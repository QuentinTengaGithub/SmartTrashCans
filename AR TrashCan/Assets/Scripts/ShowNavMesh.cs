using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNavMesh : MonoBehaviour
{
	void Start()
	{
		ShowMesh();
		InvokeRepeating("ShowMesh", 0f, 0.2f);
	}

	// Generates the NavMesh shape and assigns it to the MeshFilter component.
	void ShowMesh()
	{
		// NavMesh.CalculateTriangulation returns a NavMeshTriangulation object.
		UnityEngine.AI.NavMeshTriangulation meshData = UnityEngine.AI.NavMesh.CalculateTriangulation();

		// Create a new mesh and chuck in the NavMesh's vertex and triangle data to form the mesh.
		Mesh mesh = new Mesh();
		mesh.vertices = meshData.vertices;
		mesh.triangles = meshData.indices;

		// Assigns the newly-created mesh to the MeshFilter on the same GameObject.
		GetComponent<MeshFilter>().mesh = mesh;
	}
}
