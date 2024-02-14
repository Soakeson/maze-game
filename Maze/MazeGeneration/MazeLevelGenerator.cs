using System.Collections.Generic;
using System;

public class MazeLevelGenerator : IGameLevelGenerator<MazeLevel>
{
  private Dictionary<(int x, int y), Cell> Frontier = new Dictionary<(int x, int y), Cell>();
  public int test = 0;

  //<summary>
  // Generates a MazeLevel of size hxw using a variation of prims algorithm.
  //</summary>
  public MazeLevel Generate(int h, int w)
  {
    Random rnd = new Random();
    Dictionary<(int x, int y), Cell> field = new Dictionary<(int x, int y), Cell>();
    for (int y = 0; y < h; y++)
    {
      for (int x = 0; x < w; x++)
      {
        Frontier.Add((x, y), new Cell((x, y)));
      }
    }

    List<(Cell from, Cell to, Direction dir)> walls = new List<(Cell from, Cell to, Direction dir)>();
    Cell start = Frontier[(0,0)];
    field.Add(start.Cord, start);
    walls.AddRange(BuildWalls(start));
    do {
      int rand = rnd.Next(0, walls.Count);
      (Cell from, Cell to, Direction dir) = walls[rand];
      walls.RemoveAt(rand);
      if (Frontier.ContainsKey(to.Cord) && !field.ContainsKey(to.Cord) && field.ContainsKey(from.Cord))
      {
        field.Add(to.Cord, to);
        switch (dir)
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
        walls.AddRange(BuildWalls(to));
      }
    } while (walls.Count > 0);

    Frontier.Clear();
    return new MazeLevel(h, w, field);
  }

  //<summary>
  // Generates an array of "Walls" to from a given cord 
  //</summary>
  private List<(Cell from, Cell to, Direction dir)> BuildWalls(Cell from)
  {
    List<(Cell from, Cell to, Direction dir)> arr = new List<(Cell from, Cell to, Direction dir)>();
    int i = 0;
    foreach(Direction dir in Direction.GetValues(typeof(Direction)))
    {
      Cell to;
      if (Frontier.TryGetValue(IGameLevelGenerator<MazeLevel>.AddCord(from.Cord, CordOffset.GetOffset(dir)), out to))
      {
        arr.Add((from, to, dir));
        i++;
      }
    }
    return arr;
  }

}
