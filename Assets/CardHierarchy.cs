using UnityEngine;
using UnityEngine.UI;

public class CardHierarchy : MonoBehaviour
{
  [SerializeField] private Button _button;
  [SerializeField] private Image _cardShirt;
  [SerializeField] private Animator _animator;
  public Button Button => _button;
  public Animator Animator =>  _animator;
  
  public void InitializeView(Sprite sprite) => 
    _cardShirt.sprite = sprite;
}
