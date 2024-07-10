using _CodeBase;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    private void Awake()
    {
        GameLauncher.LaunchGame().Forget();
        Destroy(gameObject);
    }
}
