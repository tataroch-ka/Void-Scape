namespace _CodeBase.LevelModule.UI
{
  public class ScoreHandler
  {
    public int Matches { get; private set; }
    public int Turns { get; private set; }
    
    private const string MATCHES = "Matches";
    private const string TURNS = "Turns";

    private readonly HudHierarchy _hudHierarchy;

    public ScoreHandler(HudHierarchy hudHierarchy)
    {
      _hudHierarchy = hudHierarchy;
    }

    public void AddMatch(int count) => 
      _hudHierarchy.MatchScore.text = $"{MATCHES}: {Matches+=count}";

    public void AddTurn(int count) => 
      _hudHierarchy.TurnScore.text = $"{TURNS}: {Turns+=count}";
  }
}