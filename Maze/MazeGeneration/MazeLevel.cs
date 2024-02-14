using System.Collections.Generic;
using System;

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
      Width = w;
      Field = f;
      Start = GetCell(0, 0);
      End = GetCell(Width-1, Height-1);
      Path = new Stack<Cell>();
      BuildShortestPath();
      printPath();
    }

    private void BuildShortestPath()
    {
      Dictionary<(int x, int y), Cell> visited = new Dictionary<(int x, int y), Cell>();
      PriorityQueue<Cell, int> queue = new PriorityQueue<Cell, int>();

      Cell curr = Start;
      Path.Push(Start);
      queue.Enqueue(curr, FScore(curr));
      int lastFScore = FScore(curr);
      while (curr.Cord != End.Cord)
      {
        curr  = queue.Dequeue();
        for(int i = FScore(curr) - lastFScore; i >= 0; i--){Path.Pop();};
        Path.Push(curr);
        visited.Add(curr.Cord, curr);
        foreach(Direction dir in Direction.GetValues(typeof(Direction)))
        {
          if (curr.Passage.HasFlag(dir))
          {
            (int x, int y) = IGameLevel.AddCord(CordOffset.GetOffset(dir),curr.Cord);
            Cell neighbor = GetCell(x, y);
            if (!visited.ContainsKey(neighbor.Cord)) { queue.Enqueue(neighbor, FScore(neighbor)); }
          }
        }
        lastFScore = FScore(curr);
      }
    }

    private void printPath() 
    {
      foreach(Cell curr in Path)
      {
        Console.WriteLine(curr.Cord);
      }

    }

    private int FScore(Cell curr) 
    {
      return (End.Cord.x + End.Cord.y) - (curr.Cord.x + curr.Cord.y);
    }

    public Cell GetCell(int x, int y)
    {
      if (x > Width) throw new System.Exception("X > Level Width");
      if (y > Height) throw new System.Exception("Y > Level Height");
      return Field[(x,y)];
    }
}
