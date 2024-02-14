using System.Collections.Generic;

public class MazeLevel : IGameLevel
{
    public int Height { get; set; }
    public int Width { get; set; }
    public Stack<Cell> Path {get; set;}
    public Cell Start;
    public Cell End;
    public Dictionary<(int x, int y), Cell> Field { get; set; }

    public MazeLevel(int h, int w, Dictionary<(int x, int y), Cell> f)
    {
      Height = h;
      Width = h;
      Field = f;
    }

    public Cell getCell(int x, int y)
    {
      if (x > Width) throw new System.Exception("X > Level Width");
      if (y > Height) throw new System.Exception("Y > Level Height");
      return Field[(x,y)];
    }
}
