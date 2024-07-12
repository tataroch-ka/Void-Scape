using UnityEngine;
using UnityEngine.UI;

namespace _CodeBase.Core
{
  public class MainScreenHierarchy : MonoBehaviour
  {
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _loadGameButton;
  
    public Button NewGameButton => _newGameButton;
    public Button LoadGameButton => _loadGameButton;
  }
}
