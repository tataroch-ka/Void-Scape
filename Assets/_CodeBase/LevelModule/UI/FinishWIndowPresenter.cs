using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _CodeBase.LevelModule.UI
{
  public static class FinishWindowPresenter
  {
    private const string FINISH_WINDOW_ADDRESS = "FinishWindow";
    
    public static async UniTask ShowAsync()
    {
      var finishWindowPrefab = await Addressables.LoadAssetAsync<GameObject>(FINISH_WINDOW_ADDRESS);
      
      var uiCanvas = Object.FindObjectOfType<UICanvas>();
      GameObject finishWindowGameObject = Object.Instantiate(finishWindowPrefab, uiCanvas.transform, true);
      
      var rectTransform = finishWindowGameObject.GetComponent<RectTransform>();
      rectTransform.anchoredPosition = Vector2.zero;
      rectTransform.localPosition = Vector3.zero;
    }
  }
}