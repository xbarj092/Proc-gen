using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataLoader
{
    public T LoadData<T>(string path)
    {
        T data = default;

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new();
            FileStream fileStream = new(path, FileMode.Open);

            data = (T)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }

        return data;
    }
}
