using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SeismicDataVisualiser : MonoBehaviour
{
    [SerializeField] private Transform moon;
    [SerializeField] private GameObject seismicObjectPrefab;
    [SerializeField] private float magnitudeScalar;
    
    [Header("CSV")]
    [SerializeField] private string csvFilePath = "Assets/Data/nakamura_1979_sm_locations.csv"; 

    private float _moonRadius = 10f;

    void Start()
    {
        // Load and parse the CSV data
        string[] csvLines = File.ReadAllLines(csvFilePath);

        // Skip the header row if needed
        for (int i = 1; i < csvLines.Length; i++)
        {
            string[] fields = csvLines[i].Split(',');

            // Ensure the array has the expected number of fields
            if (fields.Length >= 8)
            {
                int year = int.Parse(fields[0]);
                int day = int.Parse(fields[1]);
                int h = int.Parse(fields[2]);
                int m = int.Parse(fields[3]);
                int s = int.Parse(fields[4]);
                float lat = float.Parse(fields[5]);
                float lon = float.Parse(fields[6]);
                float magnitude = float.Parse(fields[7]);
                string comments = fields.Length >= 9 ? fields[8] : null;

                // Calculate the position on the Moon's surface
                Vector3 position = MoonUtils.LatLongToPosition(lat, lon, _moonRadius);

                // Instantiate seismic objects on the Moon's surface
                GameObject seismicObject = Instantiate(seismicObjectPrefab, position, Quaternion.identity, moon);
                float newMagnitude = _moonRadius / magnitude * 0.01f;
                seismicObject.transform.localScale = new Vector3(newMagnitude, newMagnitude, newMagnitude);

                // You can also add additional details or components here, e.g., comments
                if (!string.IsNullOrEmpty(comments))
                {
                    //seismicObject.GetComponentInChildren<TextMesh>().text = comments;
                }
            }
            else
            {
                Debug.LogWarning("Skipping invalid CSV line: " + csvLines[i]);
            }
        }
    }
}