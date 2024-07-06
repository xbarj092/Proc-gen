using UnityEngine;

public class TitleDataLoader
{
    public T LoadTitleData<T>(string path)
    {
        TextAsset itemRaritiesFile = Resources.Load<TextAsset>(path);
        if (itemRaritiesFile != null)
        {
            return JsonUtility.FromJson<T>(itemRaritiesFile.text);
        }
        else
        {
            Debug.LogError("Failed to load title data from Resources.");
        }

        return default;
    }
}
