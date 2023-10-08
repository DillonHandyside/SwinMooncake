using System;
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
    
    public static Vector2 PositionToLatLong(Vector3 position)
    {
        Vector3 sphereCenter = Vector3.zero;
        
        Vector3 vectorToClickedPoint = position - sphereCenter;
        vectorToClickedPoint = Quaternion.Euler(0, -90, 0) * vectorToClickedPoint;
        
        // latitude
        float latitudeRadians = Mathf.Asin(vectorToClickedPoint.normalized.y); // Negate the y-component
        float latitudeDegrees = Mathf.Rad2Deg * latitudeRadians;

        // longitude
        Vector3 projectionXY = new Vector3(vectorToClickedPoint.x, 0, vectorToClickedPoint.z);
        float longitudeRadians = Mathf.Atan2(projectionXY.x, projectionXY.z);
        float longitudeDegrees = Mathf.Rad2Deg * longitudeRadians;

        return new Vector2(latitudeDegrees, longitudeDegrees);
    }
    
    public static float GetDisplacementValueFromLatLong(Texture2D displacementMap, Vector2 latLong)
    {
        // Assuming the center of the displacement map is at (0, 0) latitude and longitude
        // Normalize the lat/long values to the range (-1, -1) to (1, 1)
        float normalizedX = (latLong.y / 180f);
        float normalizedY = (latLong.x / 180f);

        // Offset and clamp the normalized coordinates to stay within the texture's bounds (0 to 1)
        float offset = 0.5f;
        normalizedX = Mathf.Clamp01(normalizedX + offset);
        normalizedY = Mathf.Clamp01(normalizedY + offset);

        // Convert the normalized coordinates to UV coordinates
        Vector2 uvCoords = new Vector2(normalizedX, normalizedY);
        Debug.Log(uvCoords);

        // Sample the displacement map at the UV coordinates
        Color pixelColor = displacementMap.GetPixelBilinear(uvCoords.x, uvCoords.y);

        // Convert the grayscale pixel value (0-1) to the desired scale (e.g., 0-100)
        float displacementValue = pixelColor.r;
        return pixelColor.r;
    }
}