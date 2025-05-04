using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class TrackingGPS : MonoBehaviour
{
    public float userLat;
    public float userLon;

    private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        } else
        {
            StartCoroutine(StartLocationService());
        }
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location not enabled by user");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (Input.location.status == LocationServiceStatus.Running)
        {
            userLat = Input.location.lastData.latitude;
            userLon = Input.location.lastData.longitude;
        }
        else
        {
            Debug.Log("Unable to determine location");
        }
    }

    float GetDistanceInMeters(float lat1, float lon1, float lat2, float lon2)
    {
        float R = 6371000f;
        float dLat = Mathf.Deg2Rad * (lat2 - lat1);
        float dLon = Mathf.Deg2Rad * (lon2 - lon1);

        float a = Mathf.Sin((dLat / 2)) * Mathf.Sin((dLat / 2)) +
                  Mathf.Cos((Mathf.Deg2Rad * lat1)) * Mathf.Cos((Mathf.Deg2Rad * lat2)) *
                  Mathf.Sin((float)(dLon / 2)) * Mathf.Sin((float)(dLon / 2));

        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt((1 - a)));
        return R * c;
    }

    public bool IsWithinRadius(float lat1, float lon1, float lat2, float lon2, float radiusMeters)
    {
        return GetDistanceInMeters(lat1, lon1, lat2, lon2) <= radiusMeters;
    }
}
