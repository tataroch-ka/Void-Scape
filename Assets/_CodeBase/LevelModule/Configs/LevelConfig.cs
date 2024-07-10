using System.Collections.Generic;
using UnityEngine;

namespace _CodeBase.Configs
{
  [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configuration/Level Config", order = 1)]
  public class LevelConfig : ScriptableObject
  {
    [SerializeField] private int _width = 5;
    [SerializeField] private int _height = 5;
    [SerializeField] private float _spacing = 50f;
    [SerializeField] private List<Sprite> _cardShirts;

    public int Width => _width;
    public int Height => _height;
    public float Spacing => _spacing;
    public List<Sprite> CardShirts => _cardShirts;
  }
}