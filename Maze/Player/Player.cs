public class Player
{
  (int x, int y) Cord;
  int Score = 0;

  public delegate void Move(Direction dir);

  public Player((int x, int y) pos)
  {
    Cord = pos;
  }

  public void updatePosition((int x, int y) pos)
  {
    Cord = pos;
  }

  public (int x, int y) getCord()
  {
    return Cord;
  }

  public int getY()
  {
    return Cord.y;
  }

  public int getX()
  {
    return Cord.x;
  }
}
