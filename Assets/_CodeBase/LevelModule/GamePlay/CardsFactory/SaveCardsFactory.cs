using System.Collections.Generic;
using _CodeBase.LevelModule.ProgressSaver.Entities;
using UnityEngine;

namespace _CodeBase.LevelModule.GamePlay.CardsFactory
{
  public class SaveCardsFactory : CardsFactory
  {
    private readonly RectTransform _container;
    private readonly SaveLevelData _saveData;

    public SaveCardsFactory(
      RectTransform container, 
      SaveLevelData saveData, 
      GameObject cardsPrefab) : base(cardsPrefab)
    {
      _container = container;
      _saveData = saveData;
    }

    public override List<Card> CreateCards()
    {
      List<Card> cards = new();
      foreach (SaveCardData cardData in _saveData.CardsOnTable)
      {
        if (!cardData.IsActive)
          continue;
        
        float xPos = cardData.PosX;
        float yPos = cardData.PosY;
        
        GameObject instance = Object.Instantiate(CardPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity, _container);
        var cardHierarchy = instance.GetComponent<CardHierarchy>();
        cardHierarchy.GetComponent<RectTransform>().localScale = cardData.LocalScale;
        
        var card = new Card(cardHierarchy, cardData.SpriteName, cardData.ID);
        cards.Add(card);
      }

      return cards;
    }
  }
}