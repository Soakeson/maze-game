public class Cell 
{
  public (int x, int y) cord {get;}
  public Direction passage {get; set;}

  public Cell((int x, int y) cord) 
  {
    this.cord = cord;
    this.passage = 0;
  }
}
