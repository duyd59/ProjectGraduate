using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalsaveManager : MyObj
{
    private string SaveFilePath => Path.Combine(Application.persistentDataPath, "farm_save.json");

    public void SaveToJson(FarmeSaveDate data)
    {
        if (data == null) return;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(this.SaveFilePath, json);
    }

    public FarmeSaveDate LoadFromJson()
    {
        if (!File.Exists(this.SaveFilePath))
        {
            return new FarmeSaveDate();
        }

        string json = File.ReadAllText(this.SaveFilePath);
        FarmeSaveDate data = JsonUtility.FromJson<FarmeSaveDate>(json);
        return data ?? new FarmeSaveDate();
    }
}
