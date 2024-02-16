using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;

[System.Serializable]
public class ResponseData
{
    public int id;
    public int joystick;
    public int ultrasound;
    public int sensorThree;
}

[System.Serializable]
public class ResponseDataWrapper
{
    public ResponseData[] responseDataArray;
}

public class SensorDataRetriever : MonoBehaviour
{
    [SerializeField] private string url = "https://friendly-lewin.93-90-203-17.plesk.page/sensors";

    [SerializeField]
    TrashSliderManager trashCan1;

    [SerializeField]
    TrashSliderManager trashCan2;

    [SerializeField]
    TrashSliderManager trashCan3;

    private void Start()
    {
        InvokeRepeating("Call", 0.5f, 3f);
    }


    private async void Call()
    {
        var sensorData = await GetSensorData();

        // Remove the '[' and ']' characters from the JSON response
        sensorData = sensorData.Trim('[', ']');

        // Split the string into individual field-value pairs
        string[] fieldValues = sensorData.Split(',');

        // Extract the values for the desired fields
        int joystick = ExtractFieldValue<int>(fieldValues, "joystick");
        int ultrasound = ExtractFieldValue<int>(fieldValues, "ultrasound");
        int sensorThree = ExtractFieldValue<int>(fieldValues, "sensorThree");

        // Use the retrieved values as needed
        Debug.Log("joystick: " + joystick);
        Debug.Log("ultrasound: " + ultrasound);
        Debug.Log("sensorThree: " + sensorThree);

        float scaledValueJoystick = (float)(joystick - 0) / (100 - 0);
        float scaledValueUltrasound = 1f - ((float)(ultrasound - 0) / (4000 - 0));
        float scaledValueSensorThree = (float)(sensorThree - 0) / (100 - 0);

        trashCan1.CheckIfFull(scaledValueJoystick);
        trashCan2.CheckIfFull(scaledValueUltrasound);
        trashCan3.CheckIfFull(scaledValueSensorThree);

    }

    private T ExtractFieldValue<T>(string[] fieldValues, string fieldName)
    {
        foreach (string fieldValue in fieldValues)
        {
            string[] parts = fieldValue.Split(':');
            if (parts.Length == 2)
            {
                string name = parts[0].Trim('\"');
                if (name == fieldName)
                {
                    string value = parts[1].Trim();
                    if (typeof(T) == typeof(int))
                    {
                        if (int.TryParse(value, out int intValue))
                        {
                            return (T)(object)intValue;
                        }
                    }
                    else if (typeof(T) == typeof(float))
                    {
                        if (float.TryParse(value, out float floatValue))
                        {
                            return (T)(object)floatValue;
                        }
                    }

                    break;
                }
            }
        }
        return default;
    }

    private async Task<string> GetSensorData()
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}