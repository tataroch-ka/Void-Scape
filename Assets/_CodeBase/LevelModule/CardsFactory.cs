using System.Collections.Generic;
using _CodeBase.Configs;
using UnityEngine;

namespace _CodeBase
{
  public class CardsFactory
  {
    private readonly int _width;
    private readonly int _height;
    private readonly float _spacing;
    private readonly GameObject _cardPrefab;
    private readonly RectTransform _parent;
    private readonly CardIdGenerator _idGenerator;

    public CardsFactory(
      int width, 
      int height,
      float spacing,
      GameObject cardPrefab, 
      RectTransform parent,
      CardIdGenerator idGenerator)
    {
      _width = width;
      _height = height;
      _spacing = spacing;
      _cardPrefab = cardPrefab;
      _parent = parent;
      _idGenerator = idGenerator;
    }
    
    public List<Card> CreateCards()
    {
      Vector2 cardSize = GetPrefabSize(_cardPrefab);
      
      float cardWidth = cardSize.x;
      float cardHeight = cardSize.y;
      
      float availableWidth = _parent.rect.width - _spacing * (_width - 1);
      float availableHeight = _parent.rect.height - _spacing * (_height - 1);

      float scaleWidth = availableWidth / (_width * cardWidth);
      float scaleHeight = availableHeight / (_height * cardHeight);

      float scale = Mathf.Min(scaleWidth, scaleHeight, 1f);
      float startX = -(_width * cardWidth * scale + (_width - 1) * _spacing) / 2f + cardWidth * scale / 2f;
      float startY = (_height * cardHeight * scale + (_height - 1) * _spacing) / 2f - cardHeight * scale / 2f;

      List<Card> cards = InstantiateCards(startX, cardWidth, scale, startY, cardHeight);
      
      return cards;
    }

    private List<Card> InstantiateCards(float startX, float cardWidth, float scale, float startY, float cardHeight)
    {
      List<Card> cards = new();
      for (var y = 0; y < _height; y++)
      {
        for (var x = 0; x < _width; x++)
        {
          float posX = startX + x * (cardWidth * scale + _spacing);
          float posY = startY - y * (cardHeight * scale + _spacing);

          var position = new Vector3(posX, posY, 0);
          Card card = InstantiateAndScaleCard(scale, position);
          cards.Add(card);
        }
      }

      return cards;
    }

    private Card InstantiateAndScaleCard(float scale, Vector3 position)
    {
      var cardHierarchy = Object.Instantiate(_cardPrefab, _parent).GetComponent<CardHierarchy>();
      var rectTransform = cardHierarchy.GetComponent<RectTransform>();
      rectTransform.localPosition = position;
      rectTransform.localScale = new Vector3(scale, scale, scale);
      var card = new Card(cardHierarchy, _idGenerator.Generate());

      return card;
    }

    private static Vector2 GetPrefabSize(GameObject prefab)
    {
      var rectTransform = prefab.GetComponent<RectTransform>();
      if (rectTransform)
        return rectTransform.sizeDelta;

      Debug.LogError("GetPrefabSize() : Error! CardPrefab does not have a RectTransform component.");
      return Vector2.one;
    }
  }
}