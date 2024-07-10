using System.Collections.Generic;
using System.Linq;
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
    
    private readonly Stack<Card> _pickedCards = new(); 

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

    private async UniTask CreateGameBoardAsync(RectTransform cardsContainer)
    {
      List<Card> cards = await CreateCards(cardsContainer);
      BindCards(cards);
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
      if (_pickedCards.Count == 0)
      {
        _pickedCards.Push(card);
        return;
      }
      
      await UniTask.WaitForSeconds(1);
      if (_pickedCards.Count > 0) 
        HandleCardMatch(card);
      
      _pickedCards.Clear();
    }

    private void HandleCardMatch(Card card)
    {
      if (_pickedCards.TryPop(out Card lastCard))
      {
        if (lastCard.ID == card.ID)
        {
          Debug.LogError("MATCH");
          card.Hide();
          lastCard.Hide();
        }
        else
        {
          Debug.LogError("NOT MATCH");
          card.FlipBack();
          lastCard.FlipBack();
        }
      }
    }

    private static CardsFactory CreateCardsFactory(
      RectTransform cardsContainer, 
      LevelConfig levelConfig, 
      GameObject cardPrefab)
    {
      int totalCount = levelConfig.Height * levelConfig.Width;
      var idGenerator = new CardIdGenerator(totalCount, 2);
      
      return new CardsFactory(
        levelConfig.Height,
        levelConfig.Width,
        levelConfig.Spacing,
        cardPrefab,
        cardsContainer,
        levelConfig.CardShirts,
        idGenerator
       );
    }
  }
}