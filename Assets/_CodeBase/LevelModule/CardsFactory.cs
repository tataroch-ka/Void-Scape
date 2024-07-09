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

    public CardsFactory(
      int width, 
      int height,
      float spacing,
      GameObject cardPrefab, 
      RectTransform parent)
    {
      _width = width;
      _height = height;
      _spacing = spacing;
      _cardPrefab = cardPrefab;
      _parent = parent;
    }
    
    public void CreateCards()
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

      InstantiateCards(startX, cardWidth, scale, startY, cardHeight);
    }

    private void InstantiateCards(float startX, float cardWidth, float scale, float startY, float cardHeight)
    {
      for (var y = 0; y < _height; y++)
      {
        for (var x = 0; x < _width; x++)
        {
          float posX = startX + x * (cardWidth * scale + _spacing);
          float posY = startY - y * (cardHeight * scale + _spacing);

          var position = new Vector3(posX, posY, 0);
          InstantiateAndScaleCard(scale, position);
        }
      }
    }

    private void InstantiateAndScaleCard(float scale, Vector3 position)
    {
      GameObject card = Object.Instantiate(_cardPrefab, _parent);
      var rectTransform = card.GetComponent<RectTransform>();
      rectTransform.localPosition = position;
      rectTransform.localScale = new Vector3(scale, scale, scale);
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