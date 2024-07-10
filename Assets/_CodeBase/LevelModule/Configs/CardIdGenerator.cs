using System.Collections.Generic;

namespace _CodeBase.Configs
{
  public class CardIdGenerator
  {
    private readonly List<int> _ids;
    private int _index;

    public CardIdGenerator(int totalIdCount, int repeatIdCount) => 
      _ids = GenerateIds(totalIdCount, repeatIdCount);

    private static List<int> GenerateIds(int totalIdCount, int repeatIdCount)
    {
      var ids = new List<int>();
      for (var i = 0; i < totalIdCount / repeatIdCount; i++)
      for (var j = 0; j < repeatIdCount; j++)
        ids.Add(i);
      
      Shuffle(ids);
      
      return ids;
    }

    public int Generate()
    {
      int returnId = _ids[_index];
      _index++;

      return returnId;
    }

    private static void Shuffle(List<int> list)
    {
      var random = new System.Random();
      int count = list.Count;

      while (count > 1)
      {
        int k = random.Next(count--);
        (list[count], list[k]) = (list[k], list[count]);
      }
    }
  }
}