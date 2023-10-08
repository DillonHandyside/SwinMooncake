using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class DeepMoonquakeVisualiser : MonoBehaviour
{
    [SerializeField] private Transform moon; 
    [SerializeField] private GameObject deepMoonquakePrefab;
    [SerializeField] private float magnitudeScalar;
    private float _moonRadius = 10f;
    
    [Header("CSV")]
    [SerializeField] private TextAsset csvFile;

    [Serializable]
    public class DeepMoonquake
    {
        public int Index;
        public int Cluster;
        public string Side;
        public float Latitude;
        public float LatitudeError;
        public float Longitude;
        public float LongitudeError;
        public float Depth;
        public float DepthError;
        public char Assumed;
    }

    public List<DeepMoonquake> ParseCSV()
    {
        List<DeepMoonquake> earthquakeList = new List<DeepMoonquake>();

        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');

            for (int i = 1; i < lines.Length - 1; i++) // Start from 1 to skip the header row
            {
                string[] values = lines[i].Split(',');

                DeepMoonquake earthquakeData = new DeepMoonquake();
                
                if (int.TryParse(values[0], out earthquakeData.Index) &&
                    int.TryParse(values[1], out earthquakeData.Cluster) &&
                    float.TryParse(values[3], out earthquakeData.Latitude) &&
                    float.TryParse(values[4], out earthquakeData.LatitudeError) &&
                    float.TryParse(values[5], out earthquakeData.Longitude) &&
                    float.TryParse(values[6], out earthquakeData.LongitudeError) &&
                    float.TryParse(values[7], out earthquakeData.Depth) &&
                    float.TryParse(values[8], out earthquakeData.DepthError) &&
                    char.TryParse(values[9], out earthquakeData.Assumed))
                {
                    earthquakeData.Side = values[2];
                    
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
        List<DeepMoonquake> earthquakeDataList = ParseCSV();
        
        // Now you can use the earthquakeDataList for further processing or display in your Unity project
        foreach (var data in earthquakeDataList)
        {
            // Calculate the position on the Moon's surface
            Vector3 position = MoonUtils.LatLongToPosition(data.Latitude, data.Longitude, _moonRadius);

            // Instantiate seismic objects on the Moon's surface
            GameObject seismicObject = Instantiate(deepMoonquakePrefab, position, Quaternion.identity, moon);
            //float newMagnitude = _moonRadius / data.Depth * 0.01f;
            //seismicObject.transform.localScale = new Vector3(newMagnitude, newMagnitude, newMagnitude);
        }
    }
}