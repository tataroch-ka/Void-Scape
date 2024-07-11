using TMPro;
using UnityEngine;

public class HudHierarchy : MonoBehaviour
{
  [SerializeField] private TMP_Text _matchScore;
  [SerializeField] private TMP_Text _turnScore;

  public TMP_Text MatchScore => _matchScore;
  public TMP_Text TurnScore => _turnScore;
}
