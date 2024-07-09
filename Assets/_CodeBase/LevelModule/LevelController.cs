using System.Threading.Tasks;
using _CodeBase.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace _CodeBase
{
  public class LevelController
  {
    private const string LEVEL_CONFIG_KEY = "LevelConfig";
    private const string CARD_PREFAB_KEY = "CardPrefab";
    private const string LEVEL_CONTAINER_KEY = "LevelContainer";

    private readonly UnityEventManager _eventManager;

    public LevelController(UnityEventManager eventManager)
    {
      _eventManager = eventManager;
    }

    public async UniTask PrepareLevelAsync()
    {
      LevelContainerHierarchy levelContainer = await InstantiateLevelContainer();
      await CreateGameBoardAsync(levelContainer.CardsContainer);
    }

    private static async Task<LevelContainerHierarchy> InstantiateLevelContainer()
    {
      var levelContainerPrefab = await Addressables.LoadAssetAsync<GameObject>(LEVEL_CONTAINER_KEY);
      var levelContainer = Object.Instantiate(levelContainerPrefab).GetComponent<LevelContainerHierarchy>();
      
      return levelContainer;
    }

    private static async UniTask CreateGameBoardAsync(RectTransform cardsContainer)
    {
      var cardPrefab = await Addressables.LoadAssetAsync<GameObject>(CARD_PREFAB_KEY);
      var levelConfig = await Addressables.LoadAssetAsync<LevelConfig>(LEVEL_CONFIG_KEY);
      
      CardsFactory cardsFactory = CreateCardsFactory(cardsContainer, levelConfig, cardPrefab);
      cardsFactory.CreateCards();
    }

    private static CardsFactory CreateCardsFactory(
      RectTransform cardsContainer, 
      LevelConfig levelConfig, 
      GameObject cardPrefab) =>
      new(
        levelConfig.Height, 
        levelConfig.Width,
        levelConfig.Spacing,
        cardPrefab,
        cardsContainer);
  }
}