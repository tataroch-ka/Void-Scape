using System.Collections.Generic;
using UnityEngine;

namespace _CodeBase.LevelModule.Configs
{
  [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configuration/Level Config", order = 1)]
  public class LevelConfig : ScriptableObject
  {
    [SerializeField] private int _width = 5;
    [SerializeField] private int _height = 5;
    [SerializeField] private int _repeatCardCount = 2;
    [SerializeField] private float _spacing = 50f;
    [SerializeField] private List<Sprite> _cardShirts;

    public int Width => _width;
    public int Height => _height;
    public int RepeatCardCount => _repeatCardCount;
    public float Spacing => _spacing;
    public List<Sprite> CardShirts => _cardShirts;
  }
}