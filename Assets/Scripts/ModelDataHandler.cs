using UnityEngine;
using System.IO;

public static class ModelDataHandler
{
    private static string playerFilePath = Application.persistentDataPath + "/playerModelData.json";
    private static string petFilePath = Application.persistentDataPath + "/petModelData.json";

    public static void SavePlayerModelData(ModelData modelData)
    {
        string json = JsonUtility.ToJson(modelData);
        File.WriteAllText(playerFilePath, json);
    }

    public static ModelData LoadPlayerModelData()
    {
        if (File.Exists(playerFilePath))
        {
            string json = File.ReadAllText(playerFilePath);
            return JsonUtility.FromJson<ModelData>(json);
        }
        return null;
    }

    public static void SavePetModelData(ModelData modelData)
    {
        string json = JsonUtility.ToJson(modelData);
        File.WriteAllText(petFilePath, json);
    }

    public static ModelData LoadPetModelData()
    {
        if (File.Exists(petFilePath))
        {
            string json = File.ReadAllText(petFilePath);
            return JsonUtility.FromJson<ModelData>(json);
        }
        return null;
    }
}