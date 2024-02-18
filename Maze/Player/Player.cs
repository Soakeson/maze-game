using System.Collections.Generic;

public class Player
{
  public Cell Pos {get; set;}
  public int Score {get; set;}
  public Dictionary<(int x, int y), Cell> History;

  public Player(Cell start)
  {
    Pos = start;
    Score = 0;
    History = new Dictionary<(int x, int y), Cell>();
    History.Add(Pos.Cord, Pos);
  }

  public void SetPos(Cell newPos)
  {
    if (!History.ContainsKey(newPos.Cord))
    {
      History.Add(newPos.Cord, newPos);
    }
    Pos = newPos;
  }

}
