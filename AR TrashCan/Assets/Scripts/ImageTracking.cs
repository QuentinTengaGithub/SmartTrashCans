using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField]
    GameObject[] placeablePrefabs;

    Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    ARTrackedImageManager trackedImageManager;


    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newprefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newprefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newprefab);

        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name; 
        Vector3 pos = trackedImage.transform.position;

        GameObject prefab = spawnedPrefabs[name];

        prefab.transform.position = pos;
        prefab.SetActive(true);

        foreach (GameObject go in spawnedPrefabs.Values)
        {
            if (go.name != name)
            {
                go.SetActive(false);
            }
        }
    }
}
