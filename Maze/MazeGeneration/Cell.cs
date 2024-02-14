public class Cell 
{
  public (int x, int y) Cord {get;}
  public Direction Passage {get; set;}

  public Cell((int x, int y) cord) 
  {
    this.Cord = cord;
    this.Passage = 0;
  }
}
