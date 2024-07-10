using UnityEngine;
using UnityEngine.UI;

public class CardHierarchy : MonoBehaviour
{
  [SerializeField] private Button _button;
  [SerializeField] private Image _defaultImage;

  public Button Button => _button;
  
  public void InitializeView()
  {
    
  }
}
