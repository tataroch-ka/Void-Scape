using _CodeBase;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    private void Awake()
    {
        var gameLauncher = new GameLauncher();
        gameLauncher.LaunchGame().Forget();
            
        Destroy(gameObject);
    }
}
