using TMPro;
using UnityEngine;

namespace _CodeBase.LevelModule.UI
{
  public class HudHierarchy : MonoBehaviour
  {
    [SerializeField] private TMP_Text _matchScore;
    [SerializeField] private TMP_Text _turnScore;

    public TMP_Text MatchScore => _matchScore;
    public TMP_Text TurnScore => _turnScore;
  }
}
