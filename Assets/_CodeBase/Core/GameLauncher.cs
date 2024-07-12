using _CodeBase.LevelModule;
using _CodeBase.LevelModule.ProgressSaver;
using _CodeBase.LevelModule.ProgressSaver.Entities;
using Cysharp.Threading.Tasks;

namespace _CodeBase.Core
{
  public static class GameLauncher
  {
    public static async UniTaskVoid LaunchGame()
    {
      bool hasProgress = ProgressSaver.TryGetSave(out SaveLevelData saveLevelData);
      var mainScreenController = new MainScreenController();
      await mainScreenController.InitializeAsync(hasProgress);

      mainScreenController.OnNewGameClicked += StartNewGame;
      
      if (hasProgress) 
        mainScreenController.OnLoadGameClicked += () => LoadGame(saveLevelData);
    }

    private static async void StartNewGame()
    {
      var levelController = new LevelController();
      await levelController.StartLevelAsync();
    }

    private static async void LoadGame(SaveLevelData saveLevelData)
    {
      var levelController = new LevelController();
      await levelController.StartLevelAsync(saveLevelData);
    }
  }
}