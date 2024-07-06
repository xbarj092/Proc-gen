using System;
using UnityEngine;
using Newtonsoft.Json;

public class TitleDataLoader
{
    public T LoadTitleData<T>(string path)
    {
        TextAsset dataFile = Resources.Load<TextAsset>(path);
        if (dataFile != null)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(dataFile.text);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        else
        {
            Debug.LogError("Failed to load title data from Resources.");
        }

        return default;
    }
}
