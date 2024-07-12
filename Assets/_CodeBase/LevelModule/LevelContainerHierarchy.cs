using _CodeBase.LevelModule.UI;
using UnityEngine;

namespace _CodeBase.LevelModule
{
  public class LevelContainerHierarchy : MonoBehaviour
  {
    [SerializeField] private RectTransform _cardsContainer;
    [SerializeField] private HudHierarchy _hudHierarchy;

    public RectTransform CardsContainer => _cardsContainer;
    public HudHierarchy HudHierarchy => _hudHierarchy;
  }
}
