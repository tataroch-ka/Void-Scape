using System.Collections.Generic;
using System.Threading.Tasks;
using _CodeBase.LevelModule.Configs;
using _CodeBase.LevelModule.GamePlay;
using _CodeBase.LevelModule.GamePlay.CardsFactory;
using _CodeBase.LevelModule.ProgressSaver.Entities;
using _CodeBase.LevelModule.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace _CodeBase.LevelModule
{
  public class LevelController
  {
    private const string LEVEL_CONFIG_KEY = "LevelConfig";
    private const string CARD_PREFAB_KEY = "CardPrefab";
    private const string LEVEL_CONTAINER_KEY = "LevelContainer";

    private List<Card> _levelCards;
    private ScoreHandler _scoreHandler;
    
    private readonly Stack<Card> _pickedCards = new();
    private LevelContainerHierarchy _levelContainer;
    private SoundManager _soundManager;

    public async UniTask StartLevelAsync(SaveLevelData save = null)
    {
      _levelContainer = await InstantiateLevelContainer();
      _scoreHandler = new ScoreHandler(_levelContainer.HudHierarchy);
      _soundManager = Object.FindObjectOfType<SoundManager>();
      
      var cardPrefab = await Addressables.LoadAssetAsync<GameObject>(CARD_PREFAB_KEY);
      var levelConfig = await Addressables.LoadAssetAsync<LevelConfig>(LEVEL_CONFIG_KEY);

      CardsFactory configCardsFactory;
      if (save != null)
      {
        configCardsFactory = CreateSaveCardsFactory(_levelContainer.CardsContainer, save, cardPrefab);
        _scoreHandler.AddMatch(save.Matches);
        _scoreHandler.AddTurn(save.Turns);
      }
      else
      {
        configCardsFactory = CreateConfigCardsFactory(_levelContainer.CardsContainer, levelConfig, cardPrefab);
      }
      
      _levelCards = configCardsFactory.CreateCards();
      BindCards(_levelCards);
    }

    private static async Task<LevelContainerHierarchy> InstantiateLevelContainer()
    {
      var levelContainerPrefab = await Addressables.LoadAssetAsync<GameObject>(LEVEL_CONTAINER_KEY);
      var levelContainer = Object.Instantiate(levelContainerPrefab).GetComponent<LevelContainerHierarchy>();
      
      return levelContainer;
    }
    
    private void BindCards(List<Card> cards)
    {
      foreach (Card card in cards)
        card.OnClick += OnClickCardAsync;
    }
    
    private async void OnClickCardAsync(Card card)
    {
      card.Flip();
      _soundManager.PlayFlip();
      
      _pickedCards.Push(card);
      if (_pickedCards.Count % 2 != 0) 
        return;
      
      Card firstCard = _pickedCards.Pop();
      Card secondCard = _pickedCards.Pop();
        
      await UniTask.WaitForSeconds(1);
      if (firstCard.ID == secondCard.ID)
      {
        _soundManager.PlayMatch();
        _scoreHandler.AddMatch(1);
        
        firstCard.Hide();
        secondCard.Hide();
        
        if (_scoreHandler.Matches >= _levelCards.Count / 2)
        {
          _soundManager.PlayGameOver();
          await FinishWindowPresenter.ShowAsync();
        }
      }
      else
      {
        _soundManager.PlayMissMatch();
        
        firstCard.FlipBack();
        secondCard.FlipBack();
        
        _scoreHandler.AddTurn(1);
      }

      SaveLeveState();
    }

    private void SaveLeveState()
    {
      List<SaveCardData> cardsData = new();
      foreach (Card card in _levelCards)
      {
        Transform transform = card.GetTransform();
        
        Vector3 localScale = transform.localScale;
        Vector3 position = transform.position;
        bool isActive = transform.gameObject.activeSelf;
        
        var saveCardData = new SaveCardData
        {
          ID = card.ID,
          SpriteName = card.Shirt,
          PosX = position.x,
          PosY = position.y,
          LocalScale = localScale,
          IsActive = isActive
        };
        
        cardsData.Add(saveCardData);
      }
      
      SaveLevelData saveLevelData = new()
      {
        Matches = _scoreHandler.Matches,
        Turns = _scoreHandler.Turns,
        CardsOnTable = cardsData
      };
     
      ProgressSaver.ProgressSaver.SaveGame(saveLevelData);
    }

    private static CardsFactory CreateConfigCardsFactory(
      RectTransform cardsContainer,
      LevelConfig levelConfig, 
      GameObject cardPrefab)
    {
      ValidateAndUpdateDimensions(levelConfig, out int height, out int width, out int totalCount);
      var idGenerator = new CardIdGenerator(totalCount, levelConfig.RepeatCardCount);
      
      return new ConfigCardsFactory(
        height,
        width,
        levelConfig.Spacing,
        cardPrefab,
        cardsContainer,
        levelConfig.CardShirts,
        idGenerator
       );
    }
    
    private static CardsFactory CreateSaveCardsFactory(
      RectTransform cardsContainer, 
      SaveLevelData saveData, 
      GameObject cardPrefab)
    {
      
      return new SaveCardsFactory(
        cardsContainer,
        saveData,
        cardPrefab
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