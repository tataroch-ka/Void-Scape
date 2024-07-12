using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace _CodeBase.Core
{
  public class MainScreenController
  {
    private const string LOADING_SCREEN = "MainScreen";

    public Action OnNewGameClicked { get; set; }
    public Action OnLoadGameClicked { get; set; }

    public async UniTask InitializeAsync(bool hasProgress)
    {
      var loadingScreenPrefab = await Addressables.LoadAssetAsync<GameObject>(LOADING_SCREEN);
      var loadingScreenHierarchy = Object.Instantiate(loadingScreenPrefab).GetComponent<MainScreenHierarchy>();
      
      loadingScreenHierarchy.NewGameButton.onClick.AddListener(() =>
      {
        OnNewGameClicked?.Invoke();
        loadingScreenHierarchy.gameObject.SetActive(false);
      });

      if (hasProgress)
      {
        loadingScreenHierarchy.LoadGameButton.onClick.AddListener(() =>
        {
          OnLoadGameClicked?.Invoke();
          loadingScreenHierarchy.gameObject.SetActive(false);
        });
      }
      else
      {
        Button button = loadingScreenHierarchy.LoadGameButton;
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = Color.gray;
        button.interactable = false;
        
        button.colors = colorBlock;
      }
    }
  }
}