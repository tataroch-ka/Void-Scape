using UnityEngine;

public class LevelContainerHierarchy : MonoBehaviour
{
  [SerializeField] private RectTransform _cardsContainer;
  [SerializeField] private HudHierarchy _hudHierarchy;

  public RectTransform CardsContainer => _cardsContainer;
  public HudHierarchy HudHierarchy => _hudHierarchy;
}
