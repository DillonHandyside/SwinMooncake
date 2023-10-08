using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSeismicActivityAtLocation : MonoBehaviour
{
    
    public int CountSeismicActivityAtPoint(Vector3 point){

        int collisionCount = 0;

        List<ShallowMoonquakeVisualiser.ShallowMoonquake> listOfShallowMoonquakes = FindObjectOfType<ShallowMoonquakeVisualiser>().earthquakeDataList;
        // Calculate vector3 point within shallowmoonquake latitude, longitude, magnitude.

        foreach (var data in listOfShallowMoonquakes)
        {
            Vector3 moonquake = MoonUtils.LatLongToPosition(data.Latitude, data.Longitude, 10f);

            float collisionThreshold = 1.0f; // Adjust as needed

            // Calculate the distance between "point" and "moonquake"
            float distance = Vector3.Distance(point, moonquake);

            // If the distance is less than the threshold, consider it a collision
            if (distance < collisionThreshold)
            {
                collisionCount++;
            }
        }

        return collisionCount;

    }
}
