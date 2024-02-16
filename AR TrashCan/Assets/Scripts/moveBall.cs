using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class moveBall : MonoBehaviour
{


    private ARRaycastManager aRRaycastManager;
    private Vector2 touchPosition;
    private List<ARRaycastHit> arHits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;

            if (aRRaycastManager.Raycast(touchPosition, arHits, TrackableType.PlaneWithinPolygon))
            {
                Pose pose = arHits[0].pose;
                GetComponent<NavMeshAgent>().SetDestination(arHits[0].pose.position);
            }
        }
    }
}
