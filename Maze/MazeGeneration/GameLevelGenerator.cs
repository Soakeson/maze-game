using System.Collections.Generic;

public interface IGameLevelGenerator<T> where T : IGameLevel
{
  public T Generate(int h, int w);

  internal static (int x, int y) AddCord((int x, int y) c1, (int x, int y) c2)
  {
    return (c1.x + c2.x, c1.y + c2.y);
  }
}

public interface IGameLevel
{
  public int Height { get; set; }
  public int Width { get; set; }
  public Dictionary<(int x, int y), Cell> Field { get; }

  internal static (int x, int y) AddCord((int x, int y) c1, (int x, int y) c2)
  {
    return (c1.x + c2.x, c1.y + c2.y);
  }
}

