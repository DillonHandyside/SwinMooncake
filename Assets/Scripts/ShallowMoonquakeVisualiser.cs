using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ShallowMoonquakeVisualiser : MonoBehaviour
{
    [SerializeField] private Transform moon; 
    [SerializeField] private GameObject shallowMoonquakePrefab;
    [SerializeField] private float magnitudeScalar;
    private float _moonRadius = 10f;
    
    [Header("CSV")]
    [SerializeField] private TextAsset csvFile;

    [Serializable]
    public class ShallowMoonquake
    {
        public int Index;
        public float Latitude;
        public float Longitude;
        public float Magnitude;
        public string UTCDateTime;
        public string Type;
    }


    public List<ShallowMoonquake> ParseCSV()
    {
        List<ShallowMoonquake> earthquakeList = new List<ShallowMoonquake>();

        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');

            for (int i = 1; i < lines.Length - 1; i++) // Start from 1 to skip the header row
            {
                string[] values = lines[i].Split(',');

                ShallowMoonquake earthquakeData = new ShallowMoonquake();
                
                if (int.TryParse(values[0], out earthquakeData.Index) &&
                      float.TryParse(values[1], out earthquakeData.Latitude) &&
                      float.TryParse(values[2], out earthquakeData.Longitude) &&
                      float.TryParse(values[3], out earthquakeData.Magnitude))
                {
                    earthquakeData.UTCDateTime = values[4];
                    earthquakeData.Type = values[5];

                    earthquakeList.Add(earthquakeData);
                }
                else
                {
                    Debug.LogError($"Failed to parse data at line {i + 1}: {lines[i]}");
                }

                earthquakeList.Add(earthquakeData);
            }
        }
        else
        {
            Debug.LogError("CSV file is not assigned.");
        }

        return earthquakeList;
    }
    
    void Start()
    {
        List<ShallowMoonquake> earthquakeDataList = ParseCSV();
        
        // Now you can use the earthquakeDataList for further processing or display in your Unity project
        foreach (var data in earthquakeDataList)
        {
            // Calculate the position on the Moon's surface
            Vector3 position = MoonUtils.LatLongToPosition(data.Latitude, data.Longitude, _moonRadius);

            // Instantiate seismic objects on the Moon's surface
            GameObject seismicObject = Instantiate(shallowMoonquakePrefab, position, Quaternion.identity, moon);
            float newMagnitude = _moonRadius / data.Magnitude * 0.01f;
            seismicObject.transform.localScale = new Vector3(newMagnitude, newMagnitude, newMagnitude);
            Debug.Log($"Index: {data.Index}, Latitude: {data.Latitude}, Longitude: {data.Longitude}, Magnitude: {data.Magnitude}, UTC DateTime: {data.UTCDateTime}, Type: {data.Type}");
        }
    }
}