using System.IO;
using _CodeBase.LevelModule.ProgressSaver.Entities;
using UnityEngine;

namespace _CodeBase.LevelModule.ProgressSaver
{
  public static class ProgressSaver
  {
    private const string SAVE_DATA_JSON = "GameSave.json";

    public static void SaveGame(SaveLevelData saveLevelData)
    {
      string jsonSaveData = JsonUtility.ToJson(saveLevelData);
      string path = Path.Combine(Application.persistentDataPath, SAVE_DATA_JSON);
      
      File.WriteAllText(path, jsonSaveData);
    }

    public static bool TryGetSave(out SaveLevelData saveLevelData)
    {
      string path = Path.Combine(Application.persistentDataPath, SAVE_DATA_JSON);
      if (!File.Exists(path))
      {
        saveLevelData = new SaveLevelData();
        return false;
      }
      
      string jsonSaveData = File.ReadAllText(path);
      saveLevelData = JsonUtility.FromJson<SaveLevelData>(jsonSaveData);
      
      return true;
    }
  }
}