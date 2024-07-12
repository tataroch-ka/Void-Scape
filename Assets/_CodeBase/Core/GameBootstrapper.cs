using UnityEngine;

namespace _CodeBase.Core
{
  public class GameBootstrapper : MonoBehaviour
  {
    private void Awake()
    {
      GameLauncher.LaunchGame().Forget();
      Destroy(gameObject);
    }
  }
}
