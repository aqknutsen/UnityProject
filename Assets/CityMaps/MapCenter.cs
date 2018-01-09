using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class MapCenter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    IEnumerator HandleMap()
    {
        LocationService l = new LocationService();
        l.Start();
        var map = (AbstractMap)FindObjectOfType(typeof(AbstractMap));
        Debug.Log("here2");
        // First, check if user has location service enabled
        Debug.Log(l.isEnabledByUser);
        if (!l.isEnabledByUser)
        {
            Debug.Log("here5");
            yield break;
        }
        Debug.Log("here10");
        // Start service before querying location
        Input.location.Start();
        Debug.Log("here3");
        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            Debug.Log("WAITING FOR INITIALIZATION");
        }

       
        Debug.Log("her4");
        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
         
        }
        else
        {
            Debug.Log("here5");
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
        map.SetCenterLatitudeLongitude(new Vector2d(Input.location.lastData.latitude, Input.location.lastData.longitude));

    }

    // Update is called once per frame
    void Update () {
        Debug.Log("here");
        StartCoroutine(HandleMap());
    }
}
