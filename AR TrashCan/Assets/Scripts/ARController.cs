using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.AI;
public class ARController : MonoBehaviour
{
    [SerializeField]
    private GameObject model;
    private ARRaycastManager aRRaycastManager;
    private Vector2 touchPosition;
    private List<ARRaycastHit> arHits = new List<ARRaycastHit>();

    NavMeshAgent Ball;    

    private bool spawned;

    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {

            if (!spawned)
            {
                spawned = true;
                touchPosition = Input.GetTouch(0).position;

                if (aRRaycastManager.Raycast(touchPosition, arHits, TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = arHits[0].pose;
                    GameObject ball = Instantiate(model, pose.position, pose.rotation);
                    Ball = ball.GetComponent<NavMeshAgent>();
                }
            }
            else
            {
                touchPosition = Input.GetTouch(0).position;

                if (aRRaycastManager.Raycast(touchPosition, arHits, TrackableType.PlaneWithinPolygon))
                {
                    Ball.SetDestination(arHits[0].pose.position);
                }
            }
            
        }
    }
}