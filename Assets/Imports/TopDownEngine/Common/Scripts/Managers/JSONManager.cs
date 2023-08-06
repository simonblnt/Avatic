using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public static class JSONManager<T>
{
    public static List<T> GetJsonValues(string filePath)
    {
        if (filePath != "" && File.Exists(filePath))
        {
            try
            {
                var json = File.ReadAllText(filePath);
                List<T> resultObject = JsonConvert.DeserializeObject<List<T>>(json);
                return resultObject;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading JSON file in GetJsonValues: {e.Message}");
            }
        }
        return default;
    }
}