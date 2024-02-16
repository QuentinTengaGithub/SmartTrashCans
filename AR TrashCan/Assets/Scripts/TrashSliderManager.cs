using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashSliderManager : MonoBehaviour
{
    [SerializeField]
    Slider fill;

    [SerializeField]
    GameObject target;

    [SerializeField]
    [Range(0f, 1f)]
    private float alertValue= 0.75f;

    public void CheckIfFull(float i)
    {
        i *= 100;

        fill.value = i;

        if (i >= alertValue)
        {
            SendAlert();
        }
    }


    private void SendAlert()
    {
        GameObject truck = GameObject.FindGameObjectWithTag("Truck");
        if (truck != null)
        {
            truck.GetComponent<TruckController>().SetTarget(target);
        }
        
    }
}
