using System.IO;
using UnityEngine;

namespace _CodeBase
{
  public class ProgressSaver
  {
    private const string SAVE_DATA_JSON = "GameSave.json";

    public void SaveGame(SaveLevelData saveLevelData)
    {
      string jsonSaveData = JsonUtility.ToJson(saveLevelData);
      string path = Path.Combine(Application.persistentDataPath, SAVE_DATA_JSON);
      
      Debug.Log(path);
      File.WriteAllText(path, jsonSaveData);
    }

    public bool TryLoadGame(out SaveLevelData saveLevelData)
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