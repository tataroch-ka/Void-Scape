using System;

namespace _CodeBase
{
  public class Card
  {
    public Action<Card> OnClick;
    public int ID { get; }

    private CardHierarchy _hierarchy;

    public Card(CardHierarchy hierarchy, int id)
    {
      ID = id;
      _hierarchy = hierarchy;

      _hierarchy.Button.onClick.AddListener(() =>
      {
        OnClick?.Invoke(this);
      });
    }
  }
}