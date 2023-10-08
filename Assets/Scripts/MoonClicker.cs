using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonClicker : MonoBehaviour
{
    [SerializeField] private Texture2D moonTexture;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Input.mousePosition;
        
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            RaycastHit hit;
        
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 clickedPoint = hit.point;
                
                
                CheckSeismicActivityAtLocation seismicActivityChecker = FindObjectOfType<CheckSeismicActivityAtLocation>();
                int count = seismicActivityChecker.CountSeismicActivityAtPoint(clickedPoint);
                Debug.Log(count);
            
                // Now you can proceed to convert 'clickedPoint' to latitude and longitude
                // or x and y coordinates.
                Vector2 latLong = MoonUtils.PositionToLatLong(clickedPoint);
                float greyscaleValue = MoonUtils.GetDisplacementValueFromLatLong(moonTexture, latLong);
                Debug.Log(greyscaleValue);
            }
        }
    }
}
