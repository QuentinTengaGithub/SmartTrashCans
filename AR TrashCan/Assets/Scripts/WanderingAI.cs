using UnityEngine;
using UnityEngine.AI;
using System.Collections;
 
public class WanderingAI : MonoBehaviour
{

    private Transform target;

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Target").transform;
        TryGetComponent<NavMeshAgent>(out NavMeshAgent tmp);
        if (target !=null && tmp!=null)
        {
            tmp.SetDestination(target.position);
        }
    }
}