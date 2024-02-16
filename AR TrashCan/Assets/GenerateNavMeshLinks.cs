using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateNavMeshLinks : MonoBehaviour
{
    public float linkDistanceThreshold = 1f;
    public float agentHeight = 1.8f;
    public float agentRadius = 0.5f;

    private NavMeshSurface[] navMeshSurfaces;

    private void Start()
    {
        // Get all NavMeshSurface components in the scene
        navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();

        // Generate NavMesh links between overlapping surfaces
        InvokeRepeating("GenerateLinks",0.5f,1f);
    }

    private void GenerateLinks()
    {
        for (int i = 0; i < navMeshSurfaces.Length - 1; i++)
        {
            for (int j = i + 1; j < navMeshSurfaces.Length; j++)
            {
                NavMeshSurface surfaceA = navMeshSurfaces[i];
                NavMeshSurface surfaceB = navMeshSurfaces[j];

                // Check if surfaces overlap
                if (SurfaceOverlaps(surfaceA, surfaceB))
                {
                    // Find closest points on the surfaces
                    Vector3 closestPointA, closestPointB;
                    FindClosestPoints(surfaceA, surfaceB, out closestPointA, out closestPointB);

                    // Calculate the distance between closest points
                    float distance = Vector3.Distance(closestPointA, closestPointB);

                    // Create a NavMeshLink if the distance is below the threshold
                    if (distance < linkDistanceThreshold)
                    {
                        CreateNavMeshLink(closestPointA, closestPointB, surfaceA);
                    }
                }
            }
        }
    }

    private bool SurfaceOverlaps(NavMeshSurface surfaceA, NavMeshSurface surfaceB)
    {
        // Check if the bounding boxes of the surfaces overlap
        return surfaceA.GetComponent<Collider>().bounds.Intersects(surfaceB.GetComponent<Collider>().bounds);
    }

    private void FindClosestPoints(NavMeshSurface surfaceA, NavMeshSurface surfaceB, out Vector3 closestPointA, out Vector3 closestPointB)
    {
        // Find closest points on the surfaces using collider bounds
        Bounds boundsA = surfaceA.GetComponent<Collider>().bounds;
        Bounds boundsB = surfaceB.GetComponent<Collider>().bounds;
        closestPointA = boundsA.ClosestPoint(boundsB.center);
        closestPointB = boundsB.ClosestPoint(boundsA.center);
    }

    private void CreateNavMeshLink(Vector3 positionA, Vector3 positionB, NavMeshSurface surface)
    {
        // Create a NavMeshLink between the surfaces
        GameObject linkObject = new GameObject("NavMeshLink");
        linkObject.transform.position = positionA;

        NavMeshLink navMeshLink = linkObject.AddComponent<NavMeshLink>();
        navMeshLink.startPoint = Vector3.zero;
        navMeshLink.endPoint = positionB - positionA;
        navMeshLink.width = agentRadius * 2f;
        navMeshLink.bidirectional = true;

        // Bake the updated NavMesh
        surface.BuildNavMesh();
    }
}
