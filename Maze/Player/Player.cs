public class Player
{
  public Cell Pos {get; set;}
  public int Score {get; set;}

  public Player(Cell start)
  {
    Pos = start;
    Score = 0;
  }
}
