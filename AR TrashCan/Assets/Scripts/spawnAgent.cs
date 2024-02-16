using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.AI;


public class spawnAgent : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    GameObject agent;

    public bool spawned;

    void Update()
    {
        if (!spawned && (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space) ))
        {
            spawned = true;
            GameObject ball = Instantiate(agent, spawnPoint.position, spawnPoint.rotation);

            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(spawnPoint.position, out closestHit, 500, NavMesh.AllAreas))
            {
                ball.transform.position = closestHit.position;
                ball.AddComponent<NavMeshAgent>();
                NavMeshAgent nav = ball.GetComponent<NavMeshAgent>();
                nav.enabled = false;
                nav.enabled = true;
                nav.agentTypeID = 1;
            }

        }
    }

}
