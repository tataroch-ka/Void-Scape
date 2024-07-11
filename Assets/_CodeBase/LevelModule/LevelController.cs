using System.Collections.Generic;
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

    private List<Card> _levelCards;
    private ScoreHandler _scoreHandler;
    
    private readonly ProgressSaver _progressSaver = new();
    private readonly Stack<Card> _pickedCards = new();

    public LevelController()
    {
      
    }

    public async UniTask StartLevelAsync(SaveLevelData save = null)
    {
      if (save != null)
      {
        
      }
      
      LevelContainerHierarchy levelContainer = await InstantiateLevelContainer();
      _scoreHandler = new ScoreHandler(levelContainer.HudHierarchy);
      
      await CreateGameBoardAsync(levelContainer.CardsContainer);
    }

    private static async Task<LevelContainerHierarchy> InstantiateLevelContainer()
    {
      var levelContainerPrefab = await Addressables.LoadAssetAsync<GameObject>(LEVEL_CONTAINER_KEY);
      var levelContainer = Object.Instantiate(levelContainerPrefab).GetComponent<LevelContainerHierarchy>();
      
      return levelContainer;
    }

    private async UniTask CreateGameBoardAsync(RectTransform container)
    {
      _levelCards = await CreateCards(container);
      BindCards(_levelCards);
    }

    private static async Task<List<Card>> CreateCards(RectTransform cardsContainer)
    {
      var cardPrefab = await Addressables.LoadAssetAsync<GameObject>(CARD_PREFAB_KEY);
      var levelConfig = await Addressables.LoadAssetAsync<LevelConfig>(LEVEL_CONFIG_KEY);
      
      CardsFactory cardsFactory = CreateCardsFactory(cardsContainer, levelConfig, cardPrefab);
      List<Card> cards = cardsFactory.CreateCards();
      
      return cards;
    }
    
    private void BindCards(List<Card> cards)
    {
      foreach (Card card in cards)
        card.OnClick += OnClickCardAsync;
    }
    
    private async void OnClickCardAsync(Card card)
    {
      card.Flip();
      _pickedCards.Push(card);

      if (_pickedCards.Count % 2 != 0) 
        return;
      
      Card firstCard = _pickedCards.Pop();
      Card secondCard = _pickedCards.Pop();
        
      await UniTask.WaitForSeconds(1);
      if (firstCard.ID == secondCard.ID)
      {
        firstCard.Hide();
        secondCard.Hide();
        
        _scoreHandler.CountMatch();
      }
      else
      {
        firstCard.FlipBack();
        secondCard.FlipBack();
        
        _scoreHandler.CountTurn();
      }

      SaveLeveState();
    }

    private void SaveLeveState()
    {
      List<SaveCardData> cardsData = new();
      foreach (Card card in _levelCards)
      {
        Vector3 position = card.GetPosition();
        var saveCardData = new SaveCardData
        {
          ID = card.ID,
          SpriteName = card.Shirt,
          PosX = position.x,
          PosY = position.y
        };
        
        cardsData.Add(saveCardData);
      }
      
      SaveLevelData saveLevelData = new()
      {
        Matches = _scoreHandler.Matches,
        Turns = _scoreHandler.Turns,
        CardsOnTable = cardsData
      };
     
      _progressSaver.SaveGame(saveLevelData);
    }

    private static CardsFactory CreateCardsFactory(
      RectTransform cardsContainer, 
      LevelConfig levelConfig, 
      GameObject cardPrefab)
    {
      ValidateAndUpdateDimensions(levelConfig, out int height, out int width, out int totalCount);
      var idGenerator = new CardIdGenerator(totalCount, levelConfig.RepeatCardCount);
      
      return new CardsFactory(
        height,
        width,
        levelConfig.Spacing,
        cardPrefab,
        cardsContainer,
        levelConfig.CardShirts,
        idGenerator
       );
    }

    private static void ValidateAndUpdateDimensions(LevelConfig levelConfig, out int height, out int width, out int totalCount)
    {
      height = levelConfig.Height;
      width = levelConfig.Width;
      totalCount = height * width;
      
      int repeatCardCount = levelConfig.RepeatCardCount;
      if (totalCount % repeatCardCount == 0) 
        return;
      
      Debug.LogWarning($"Odd number of cards detected. Height: {height}, Width: {width}. Adjusting to ensure pairs.");
      while (totalCount % repeatCardCount != 0)
      {
        if (width >= height)
          height++;
        else
          width++;
        totalCount = height * width;
      }
    }
  }
}