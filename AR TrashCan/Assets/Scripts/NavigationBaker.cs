using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    public List<NavMeshSurface> surfaces = new List<NavMeshSurface>();

    private void Start()
    {
        GameObject[] meshs = GameObject.FindGameObjectsWithTag("Mosh");

        foreach (var mesh in meshs)
        {
            surfaces.Add(mesh.GetComponent<NavMeshSurface>());
        }

        InvokeRepeating("BakeMeshs", 0f, 0.5f);
    }

    void BakeMeshs()
    {
        for (int i = 0; i < surfaces.Count; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}