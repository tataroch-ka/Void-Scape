using System.Collections.Generic;
using UnityEngine;

namespace _CodeBase.LevelModule.GamePlay.CardsFactory
{
  public abstract class CardsFactory
  {
    protected readonly GameObject CardPrefab;
    public abstract List<Card> CreateCards();

    protected CardsFactory(GameObject cardPrefab)
    {
      CardPrefab = cardPrefab;
    }
  }
}