using System;
using UnityEngine;

namespace _CodeBase
{
  public class Card
  {
    public Action<Card> OnClick;
    public int ID { get; }

    private static readonly int FlipAnimation = Animator.StringToHash("Flip");
    private static readonly int FlipBackAnimation = Animator.StringToHash("FlipBack");
    private readonly CardHierarchy _hierarchy;

    public Card(CardHierarchy hierarchy, Sprite shirt, int id)
    {
      ID = id;
      _hierarchy = hierarchy;

      _hierarchy.InitializeView(shirt);
      _hierarchy.Button.onClick.AddListener(() =>
      {
        OnClick?.Invoke(this);
      });
    }

    public void Flip() => 
      _hierarchy.Animator.SetTrigger(FlipAnimation);

    public void FlipBack() => 
      _hierarchy.Animator.SetTrigger(FlipBackAnimation);

    public void Hide()
    {
      //_hierarchy.Animator.SetTrigger(FlipAnimation);
      _hierarchy.gameObject.SetActive(false);
    }
  }
}