using System;
using System.Collections.Generic;
using MazeCore.Models;
using MazeCore.Services;

namespace MazeCore.Algorithms
{
    public class AStarSolver : IMazeSolver
    {
        
        private int H(Point a, Point b) =>
            Math.Abs(a.Row - b.Row) + Math.Abs(a.Col - b.Col);//

        public SolverResult Solve(Maze maze, Point start, Point goal)
        {
            var g = new Dictionary<Point, int>();
            var f = new Dictionary<Point, int>();
            var parent = new Dictionary<Point, Point>();
            var visited = new HashSet<Point>();
            var visitedOrder = new List<Point>();

            var cmp = Comparer<(int, Point)>.Create((a, b) =>
            {
                int c = a.Item1.CompareTo(b.Item1);
                if (c != 0) return c;
                c = a.Item2.Row.CompareTo(b.Item2.Row);
                if (c != 0) return c;
                return a.Item2.Col.CompareTo(b.Item2.Col);
            });

            var open = new SortedSet<(int, Point)>(cmp);

            g[start] = 0;
            f[start] = H(start, goal);
            open.Add((f[start], start));

            while (open.Count > 0)
            {
                var (cf, cur) = open.Min;
                open.Remove(open.Min);

                if (visited.Contains(cur)) continue;

                visited.Add(cur);
                visitedOrder.Add(cur);

                if (cur.Equals(goal))
                    return new SolverResult(Reconstruct(parent, start, goal), visitedOrder);

                foreach (var n in maze.GetNeighbors(cur))
                {
                    int tentative = g[cur] + 1;

                    if (!g.TryGetValue(n, out int old) || tentative < old)
                    {
                        g[n] = tentative;
                        f[n] = tentative + H(n, goal);
                        parent[n] = cur;
                        open.Add((f[n], n));
                    }
                }
            }

            return new SolverResult(new(), visitedOrder);
        }

        private List<Point> Reconstruct(Dictionary<Point, Point> parent, Point start, Point goal)
        {
            var list = new List<Point>();
            if (!parent.ContainsKey(goal)) return list;

            var cur = goal;
            while (!cur.Equals(start))
            {
                list.Add(cur);
                cur = parent[cur];
            }

            list.Add(start);
            list.Reverse();
            return list;
        }
    }

}
