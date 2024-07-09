using UnityEngine;

public class LevelContainerHierarchy : MonoBehaviour
{
  [SerializeField] private RectTransform _cardsContainer;

  public RectTransform CardsContainer => _cardsContainer;
}
