using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TruckPath : MonoBehaviour
{

    public static Vector3[] path = new Vector3[0];
    public NavMeshAgent nav;

    public LineRenderer lr;



    void Update()
    {
        bool na = TryGetComponent<NavMeshAgent>(out NavMeshAgent navAgent);
        if (nav == null && na)
        {
            nav = navAgent;
        }
        else
        {
            lr.positionCount = navAgent.path.corners.Length;
            lr.SetPositions(navAgent.path.corners);
        }
        
    }
}
