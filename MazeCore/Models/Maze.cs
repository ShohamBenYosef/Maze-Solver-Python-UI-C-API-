using System.Collections.Generic;


namespace MazeCore.Models
{
    public class Maze
    {
        public int Rows { get; }
        public int Cols { get; }
        public CellType[,] Grid { get; }

        public Maze(int rows, int cols, CellType[,] grid)
        {
            Rows = rows;
            Cols = cols;
            Grid = grid;
        }

        public bool IsInside(Point p) => p.Row >= 0 && p.Row < Rows && p.Col >= 0 && p.Col < Cols;

        public bool IsWall(Point p) => Grid[p.Row, p.Col] == CellType.Wall;

        public bool IsWalkable(Point p) => IsInside(p) && !IsWall(p);

        public IEnumerable<Point> GetNeighbors(Point p)
        {
            var dirs = new[]
            {
                new Point(p.Row - 1, p.Col),
                new Point(p.Row + 1, p.Col),
                new Point(p.Row, p.Col - 1),
                new Point(p.Row, p.Col + 1)
            };

            foreach (var n in dirs)
                if (IsWalkable(n))
                    yield return n;
        }
    }

}