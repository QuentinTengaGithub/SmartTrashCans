using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TruckController : MonoBehaviour
{
    public void SetTarget(GameObject targ)
    {
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            NavMesh.SamplePosition(transform.position, out NavMeshHit closestHit, 500, NavMesh.AllAreas);
            transform.position = closestHit.position;
            NavMesh.SamplePosition(targ.transform.position, out NavMeshHit closestHitTarg, 500, NavMesh.AllAreas);
            agent.SetDestination(closestHitTarg.position);
        }
    }
}
