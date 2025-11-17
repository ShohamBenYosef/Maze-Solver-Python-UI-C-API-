using System.Collections.Generic;
using MazeCore.Models;
using MazeCore.Services;

namespace MazeCore.Algorithms
{     
    public class DijkstraSolver : IMazeSolver
    {
        public SolverResult Solve(Maze maze, Point start, Point goal)
        {
            var dist = new Dictionary<Point, int>();
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

            var pq = new SortedSet<(int, Point)>(cmp);

            dist[start] = 0;
            pq.Add((0, start));

            while (pq.Count > 0)
            {
                var (d, cur) = pq.Min;
                pq.Remove(pq.Min);

                if (visited.Contains(cur)) continue;

                visited.Add(cur);
                visitedOrder.Add(cur);

                if (cur.Equals(goal))
                    return new SolverResult(Reconstruct(parent, start, goal), visitedOrder);

                foreach (var n in maze.GetNeighbors(cur))
                {
                    int alt = d + 1;

                    if (!dist.TryGetValue(n, out int old) || alt < old)
                    {
                        dist[n] = alt;
                        parent[n] = cur;
                        pq.Add((alt, n));
                    }
                }
            }

            return new SolverResult(new(), visitedOrder);
        }

        // 
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
