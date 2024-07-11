using Cysharp.Threading.Tasks;

namespace _CodeBase
{
  public static class GameLauncher
  {
    public static async UniTaskVoid LaunchGame()
    {
      
      
      var levelController = new LevelController();
      await levelController.StartLevelAsync();
    }
  }
}