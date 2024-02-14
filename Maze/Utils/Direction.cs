using System;

[Flags]
public enum Direction 
{
  North = 1,
  South = 2,
  East = 4,
  West = 8
}

public static class CordOffset 
{
  public static (int x, int y) NORTH = (0, -1);
  public static (int x, int y) SOUTH = (0, 1); 
  public static (int x, int y) WEST = (-1, 0); 
  public static (int x, int y) EAST = (1, 0);

  public static (int x, int y) GetOffset(Direction dir)
  {
    switch(dir)
    {
      case Direction.North:
        return NORTH;
      case Direction.South:
        return SOUTH;
      case Direction.East:
        return EAST;
      case Direction.West:
        return WEST;
    }
    return (0,0);
  }
}
