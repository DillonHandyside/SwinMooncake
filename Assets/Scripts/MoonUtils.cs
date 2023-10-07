using UnityEngine;

public static class MoonUtils
{
    public static Vector3 LatLongToPosition(float latitude, float longitude, float moonRadius)
    {
        float phi = (90f - latitude) * Mathf.Deg2Rad; // Convert latitude to radians
        float theta = (360f - longitude) * Mathf.Deg2Rad; // Convert longitude to radians

        float x = moonRadius * Mathf.Sin(phi) * Mathf.Cos(theta);
        float y = moonRadius * Mathf.Cos(phi);
        float z = moonRadius * Mathf.Sin(phi) * Mathf.Sin(theta);

        return new Vector3(x, y, z);
    }
}