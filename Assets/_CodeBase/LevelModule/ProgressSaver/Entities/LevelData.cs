using System;
using System.Collections.Generic;

namespace _CodeBase.LevelModule.ProgressSaver.Entities
{
  [Serializable]
  public class SaveLevelData
  {
    public int Matches;
    public int Turns;
    public List<SaveCardData> CardsOnTable;
  }
}