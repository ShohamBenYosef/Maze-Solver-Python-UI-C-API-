
using System;
using System.Collections.Generic;
using MazeCore.Algorithms;

namespace MazeCore.Services
{

    public static class MazeSolverFactory
    {
        private static readonly Dictionary<string, Func<IMazeSolver>> map =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["dfs"] = () => new DfsSolver(),
                ["bfs"] = () => new BfsSolver(),
                ["dijkstra"] = () => new DijkstraSolver(),
                ["a*"] = () => new AStarSolver()
            };

        public static IMazeSolver Get(string name)
        {
            if (map.TryGetValue(name, out var s))
                return s();

            throw new ArgumentException($"Unknown algorithm {name}");
        }
    }
    
}