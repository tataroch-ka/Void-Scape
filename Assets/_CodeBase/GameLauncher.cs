using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _CodeBase
{
  public class GameLauncher
  {
    private const string UNITY_EVENT_MANAGER = "UnityEventManager";

    public async UniTaskVoid LaunchGame()
    {
      var eventManager = new GameObject(UNITY_EVENT_MANAGER).AddComponent<UnityEventManager>();
      Object.DontDestroyOnLoad(eventManager);
      
      var levelController = new LevelController(eventManager);
      await levelController.PrepareLevelAsync();
    }
  }
}