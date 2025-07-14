using UnityEngine;
using System.IO;

public class FileIOManager : MonoBehaviour
{
    //quick fix pls ty google
    public static FileIOManager instance;
    public SettingsObject settings;
    public static string SavePath => Application.dataPath + "/savefile.json";

    private void Awake()
    {
        instance = this;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(SavePath, json);
        //Debug.Log("Data saved to: " + SavePath);
    }

    public void LoadData()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            JsonUtility.FromJsonOverwrite(json, settings);
            //Debug.Log("Data loaded from: " + SavePath);
        }
        else
        {
            Debug.Log("No save file found at: " + SavePath);
        }
    }
}
