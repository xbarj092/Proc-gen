using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataSaver
{
    public void SaveData<T>(T dataToSave, string path)
    {
        BinaryFormatter binaryFormatter = new();
        FileStream fileStream = new(path, FileMode.Create);

        binaryFormatter.Serialize(fileStream, dataToSave);
        fileStream.Close();
    }
}
