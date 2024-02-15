using System.Collections.Generic;
using System;

public class MazeLevel : IGameLevel
{
    public int Height { get; set; }
    public int Width { get; set; }
    public Stack<Cell> Path { get; set; }
    public Cell Start;
    public Cell End;
    public Dictionary<(int x, int y), Cell> Field { get; set; }
    public Player Player { get; set; }

    public MazeLevel(int h, int w, Dictionary<(int x, int y), Cell> f)
    {
      Height = h;
      Width = w;
      Field = f;
      Start = GetCell(0, 0);
      End = GetCell(Width-1, Height-1);
      Path = new Stack<Cell>();
      Player = new Player(Start);
      BuildShortestPath();
      UpdatePath(Player.Pos);
    }

    private void BuildShortestPath()
    {
      Dictionary<(int x, int y), Cell> visited = new Dictionary<(int x, int y), Cell>();
      PriorityQueue<Cell, int> queue = new PriorityQueue<Cell, int>();
      Dictionary<(int x, int y), (int x, int y)> cameFrom = new Dictionary<(int x, int y), (int x, int y)>();

      Cell curr = Start;
      Cell prev = Start;
      cameFrom.Add(Start.Cord, Start.Cord);
      queue.Enqueue(curr, Mdist(curr));
      while (curr.Cord != End.Cord)
      {
        curr = queue.Dequeue();
        visited.Add(curr.Cord, curr);
        foreach(Direction dir in Direction.GetValues(typeof(Direction)))
        {
          if (curr.Passage.HasFlag(dir))
          {
            (int x, int y) = IGameLevel.AddCord(CordOffset.GetOffset(dir),curr.Cord);
            Cell neighbor = GetCell(x, y);
            if (!visited.ContainsKey(neighbor.Cord)) { 
              cameFrom.Add(neighbor.Cord, curr.Cord);
              queue.Enqueue(neighbor, Mdist(neighbor));
            }
          }
        }
      }

      Cell to = End;
      Path.Push(to);
      Cell from = Field[cameFrom[End.Cord]];
      while (to.Cord != Start.Cord)
      {
        Path.Push(from);
        to = from;
        from = Field[cameFrom[from.Cord]];
      }
    }

    private int Mdist(Cell curr) 
    {
      return Math.Abs(End.Cord.x - curr.Cord.x) + Math.Abs(End.Cord.y - curr.Cord.y);
    }

    private void printPath() 
    {
      foreach(Cell curr in Path)
      {
        Console.WriteLine(curr.Cord);
      }
    }

    public void MovePlayer(Direction dir)
    {
      switch(dir)
      {
        case Direction.North:
          if(Player.Pos.Passage.HasFlag(Direction.North)) 
          {
            Cell next = Field[(IGameLevel.AddCord(Player.Pos.Cord, CordOffset.NORTH))];
            UpdatePath(next);
            Player.Pos = next;
          }
        break;
        case Direction.South:
          if(Player.Pos.Passage.HasFlag(Direction.South)) 
          {
            Cell next = Field[(IGameLevel.AddCord(Player.Pos.Cord, CordOffset.SOUTH))];
            UpdatePath(next);
            Player.Pos = next;
          }
        break;
        case Direction.East:
          if(Player.Pos.Passage.HasFlag(Direction.East)) 
          {
            Cell next = Field[(IGameLevel.AddCord(Player.Pos.Cord, CordOffset.EAST))];
            UpdatePath(next);
            Player.Pos = next;
          }
        break;
        case Direction.West:
          if(Player.Pos.Passage.HasFlag(Direction.West)) 
          {
            Cell next = Field[(IGameLevel.AddCord(Player.Pos.Cord, CordOffset.WEST))];
            UpdatePath(next);
            Player.Pos = next;
          }
        break;
      }
    }

    private void UpdatePath(Cell next)
    {
      Cell onPath;
      if (Path.TryPeek(out onPath))
      {
        if (onPath.Cord == next.Cord)
        {
          Path.Pop();
        }
        else
        {
          Path.Push(Player.Pos);
        }
      }
    }

    public Cell GetCell(int x, int y)
    {
      if (x > Width) throw new System.Exception("X > Level Width");
      if (y > Height) throw new System.Exception("Y > Level Height");
      return Field[(x,y)];
    }
}
