using System;
using System.Collections.Generic;

public class MazeGrid 
{
  public int Height;
  public int Width;
  public Dictionary<(int x, int y), Cell> Grid;
  private readonly (int x, int y) NORTHOFFSET = (0, -1), SOUTHOFFSET = (0, 1), WESTOFFSET = (-1, 0), EASTOFFSET = (1, 0);

  public MazeGrid(int height, int width)
  {
    this.Height = height;
    this.Width = width;
    this.Grid = new Dictionary<(int x, int y), Cell>();
    generate();
  }

  private void generate()
  {
    Random rnd = new Random();
    Dictionary<(int x, int y), Cell> cells = new Dictionary<(int x, int y), Cell>();
    for (int y = 0; y < Height; y++)
    {
      for (int x = 0; x < Width; x++)
      {
        cells.Add((x, y), new Cell((x, y)));
      }
    }

    List<((int x, int y) from, (int x, int y) to, Direction dir)> walls = new List<((int x, int y) from, (int x, int y) to, Direction dir)>();
    Cell start = cells[(0,0)];
    Grid.Add(start.Cord, start);
    walls.Add((start.Cord, addCord(NORTHOFFSET, start.Cord), Direction.North));
    walls.Add((start.Cord, addCord(SOUTHOFFSET, start.Cord), Direction.South));
    walls.Add((start.Cord, addCord(EASTOFFSET, start.Cord), Direction.East));
    walls.Add((start.Cord, addCord(WESTOFFSET, start.Cord), Direction.West));
    do {
      int rand = rnd.Next(0, walls.Count);
      var currWall = walls[rand];
      walls.RemoveAt(rand);
      if (cells.ContainsKey(currWall.to) && !Grid.ContainsKey(currWall.to) && Grid.ContainsKey(currWall.from))
      {
        Grid.Add(currWall.to, cells[currWall.to]);
        Cell from = Grid[currWall.from];
        Cell to = Grid[currWall.to];
        switch (currWall.dir)
        {
          case Direction.North:
            from.Passage = from.Passage | Direction.North;
            to.Passage = to.Passage | Direction.South;
            break;
          case Direction.South:
            from.Passage = from.Passage | Direction.South;
            to.Passage = to.Passage | Direction.North;
            break;
          case Direction.East:
            from.Passage = from.Passage | Direction.East;
            to.Passage = to.Passage | Direction.West;
            break;
          case Direction.West:
            from.Passage = from.Passage | Direction.West;
            to.Passage = to.Passage | Direction.East;
            break;
        }
        walls.Add((currWall.to, addCord(NORTHOFFSET, currWall.to), Direction.North));
        walls.Add((currWall.to, addCord(SOUTHOFFSET, currWall.to), Direction.South));
        walls.Add((currWall.to, addCord(EASTOFFSET, currWall.to), Direction.East));
        walls.Add((currWall.to, addCord(WESTOFFSET, currWall.to), Direction.West));
      }
    } while (walls.Count > 0);
  }

  public (int x, int y) addCord((int x, int y) c1, (int x, int y) c2)
  {
    return (c1.x + c2.x, c1.y + c2.y);
  }

  public (int x, int y) move((int x, int y) pos, Direction dir)
  {
    if (Grid[pos].Passage.HasFlag(dir))
    {
      switch (dir)
      {
        case Direction.North:
          return addCord(NORTHOFFSET, pos);
        case Direction.South:
          return addCord(SOUTHOFFSET, pos);
        case Direction.East:
          return addCord(EASTOFFSET, pos);
        case Direction.West:
          return addCord(WESTOFFSET, pos);
      }
    }
    return pos;
  }
}
