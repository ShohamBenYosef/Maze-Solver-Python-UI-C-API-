using System.Collections.Generic;
using MazeCore.Models;
using MazeCore.Services;

namespace MazeCore.Algorithms
{
    
    public class BfsSolver : IMazeSolver
    {
        public SolverResult Solve(Maze maze, Point start, Point goal)
        {
            var queue = new Queue<Point>();
            var visited = new HashSet<Point>();
            var visitedOrder = new List<Point>();
            var parent = new Dictionary<Point, Point>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                visitedOrder.Add(current);

                if (current.Equals(goal))
                    return new SolverResult(Reconstruct(parent, start, goal), visitedOrder);

                foreach (var n in maze.GetNeighbors(current))
                {
                    if (visited.Contains(n)) continue;

                    visited.Add(n);
                    parent[n] = current;
                    queue.Enqueue(n);
                }
            }

            return new SolverResult(new(), visitedOrder);
        }

        private List<Point> Reconstruct(Dictionary<Point, Point> parent, Point start, Point goal)
        {
            var path = new List<Point>();
            if (!parent.ContainsKey(goal)) return path;

            var cur = goal;
            while (!cur.Equals(start))
            {
                path.Add(cur);
                cur = parent[cur];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }
    }
}