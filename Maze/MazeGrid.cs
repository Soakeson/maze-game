using System;
using System.Collections.Generic;

public class MazeGrid 
{
  public int m_height;
  public int m_width;
  public Dictionary<(int x, int y), Cell> m_grid;
  private readonly (int x, int y) NORTHOFFSET = (0, -1), SOUTHOFFSET = (0, 1), WESTOFFSET = (-1, 0), EASTOFFSET = (1, 0);

  public MazeGrid(int height, int width)
  {
    this.m_height = height;
    this.m_width = width;
    this.m_grid = new Dictionary<(int x, int y), Cell>();
    generate();
  }

  private void generate()
  {
    Random rnd = new Random();
    Dictionary<(int x, int y), Cell> cells = new Dictionary<(int x, int y), Cell>();
    for (int y = 0; y < m_height; y++)
    {
      for (int x = 0; x < m_width; x++)
      {
        cells.Add((x, y), new Cell((x, y)));
      }
    }

    List<((int x, int y) from, (int x, int y) to, Direction dir)> walls = new List<((int x, int y) from, (int x, int y) to, Direction dir)>();
    Cell start = cells[(0,0)];
    m_grid.Add(start.cord, start);
    walls.Add((start.cord, addCord(NORTHOFFSET, start.cord), Direction.North));
    walls.Add((start.cord, addCord(SOUTHOFFSET, start.cord), Direction.South));
    walls.Add((start.cord, addCord(EASTOFFSET, start.cord), Direction.East));
    walls.Add((start.cord, addCord(WESTOFFSET, start.cord), Direction.West));
    do {
      int rand = rnd.Next(0, walls.Count);
      var currWall = walls[rand];
      walls.RemoveAt(rand);
      if (cells.ContainsKey(currWall.to) && !m_grid.ContainsKey(currWall.to) && m_grid.ContainsKey(currWall.from))
      {
        m_grid.Add(currWall.to, cells[currWall.to]);
        Cell from = m_grid[currWall.from];
        Cell to = m_grid[currWall.to];
        switch (currWall.dir)
        {
          case Direction.North:
            from.passage = from.passage | Direction.North;
            to.passage = to.passage | Direction.South;
            break;
          case Direction.South:
            from.passage = from.passage | Direction.South;
            to.passage = to.passage | Direction.North;
            break;
          case Direction.East:
            from.passage = from.passage | Direction.East;
            to.passage = to.passage | Direction.West;
            break;
          case Direction.West:
            from.passage = from.passage | Direction.West;
            to.passage = to.passage | Direction.East;
            break;
        }
        walls.Add((currWall.to, addCord(NORTHOFFSET, currWall.to), Direction.North));
        walls.Add((currWall.to, addCord(SOUTHOFFSET, currWall.to), Direction.South));
        walls.Add((currWall.to, addCord(EASTOFFSET, currWall.to), Direction.East));
        walls.Add((currWall.to, addCord(WESTOFFSET, currWall.to), Direction.West));
      }
    } while (walls.Count > 0);
  }

  public void print()
  {
    for (int y = 0; y < m_height; y++)
    {
      for (int x = 0; x < m_width; x++)
      {
        Cell cell = m_grid[(x,y)];
        if (cell.passage.HasFlag(Direction.North)) Console.Write("N");
        if (cell.passage.HasFlag(Direction.South)) Console.Write("S");
        if (cell.passage.HasFlag(Direction.West)) Console.Write("W");
        if (cell.passage.HasFlag(Direction.East)) Console.Write("E");
        Console.Write(" ");
      }
      Console.WriteLine();
    }
  }

  public (int x, int y) addCord((int x, int y) c1, (int x, int y) c2)
  {
    return (c1.x + c2.x, c1.y + c2.y);
  }
}
