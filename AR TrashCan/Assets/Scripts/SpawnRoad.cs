using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnRoad : MonoBehaviour
{

    [SerializeField]
    GameObject roadP;

    GameObject instRoad;
    void Start()
    {
        instRoad = Instantiate(roadP, transform.position, Quaternion.identity);
        instRoad.transform.SetParent(transform);
    }
}
