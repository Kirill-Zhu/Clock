using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class SaveSystem 
{
 
    public static void SaveGame(Alarma alarm)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "Alarm.Time";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(alarm);
        formatter.Serialize(stream, data);
        stream.Close();

    }
    public static SaveData loadSaveData()
    {
        string path = Application.persistentDataPath + "Alarm.Time";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("No Save Data at " + path);
            return null;
        }
    }
}
