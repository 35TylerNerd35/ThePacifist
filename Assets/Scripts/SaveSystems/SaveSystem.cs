using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class MySaveData
{
    //Floats
    public float fovVal;
    public float subtitleOpacity;

    public float[] vols;
    public float[] sensSliders;
    public bool[] mutes;

    //Bools
    public bool doHeadBob;
    public bool isFullscreen;

    //Init defaults
    public MySaveData()
    {
        //Default audio settings
        vols = new float[4] {1, 1, 1, 1};
        mutes = new bool[4] {true, true, true, true};

        //Default gameplay settings
        sensSliders = new float[] {60, 60};
        fovVal = 1;
        subtitleOpacity = 100;

        doHeadBob = true;
        isFullscreen = true;
    }
}

public static class SaveSystem
{
    static string savePath = Application.persistentDataPath + "/Settings.json";
    public static MySaveData data = new MySaveData();

    //Replace defaults with saved values
    static SaveSystem()
    {
        LoadData();
    }

    public static void LoadData()
    {
        if (File.Exists(savePath))
        {
            string jsonString = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<MySaveData>(jsonString);
        }
        else
        {
            // Create new file
            SaveData();
        }
    }

    public static void SaveData()
    {
        string jsonString = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, jsonString);
    }
}
