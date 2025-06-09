using NUnit.Framework;
using UnityEngine;
// using UnityEngine.XR.ARFoundation;
// using UnityEngine.XR.ARSubsystems;
using System.Collections;
using System.Collections.Generic;


// [RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    public GameObject placedPrefab;

    GameObject spawnedObject;

    // ARRaycastManager raycastManager;
    // List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        // raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        // Check if there is existing touch
        if (Input.touchCount == 0) 
            return;

        // Check if the raycast hit any trackables
        /*if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first hit means the closest
            // Maybe means that if there are mulitple planes overlapping the closest will prevail
            var hitPose = hits[0].pose;

            // Check if there is already spawned object. If there is none, instantiated the prefab
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                // Change the spawned object position and rotation to the touch position
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }

            // To make the spawned object always look at the camera
            Vector3 loosPos = Camera.main.transform.position;
            loosPos.y = 0;
            spawnedObject.transform.rotation = Quaternion.LookRotation(loosPos);
        }*/
    }
}
