using System.Collections.Generic;
using MazeCore.Models;
using MazeCore.Services;

namespace MazeCore.Algorithms
{
    public class DfsSolver : IMazeSolver
    {
        public SolverResult Solve(Maze maze, Point start, Point goal)
        {
            var visited = new HashSet<Point>();
            var visitedOrder = new List<Point>();
            var path = new List<Point>();

            bool Dfs(Point p)
            {
                if (!maze.IsWalkable(p) || visited.Contains(p))
                    return false;

                visited.Add(p);
                visitedOrder.Add(p);

                if (p.Equals(goal))
                {
                    path.Add(p);
                    return true;
                }

                foreach (var n in maze.GetNeighbors(p))
                {
                    if (Dfs(n))
                    {
                        path.Add(p);
                        return true;
                    }
                }

                return false;
            }

            Dfs(start);
            path.Reverse();

            return new SolverResult(path, visitedOrder);
        }
    }
}