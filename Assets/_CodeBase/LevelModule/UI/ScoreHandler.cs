namespace _CodeBase
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

    public void CountMatch() => 
      _hudHierarchy.MatchScore.text = $"{MATCHES}: {++Matches}";

    public void CountTurn() => 
      _hudHierarchy.TurnScore.text = $"{TURNS}: {++Turns}";
  }
}