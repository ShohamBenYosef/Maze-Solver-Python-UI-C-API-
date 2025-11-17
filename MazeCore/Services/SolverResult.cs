using MazeCore.Models;
using System.Collections.Generic;

namespace MazeCore.Services
{
    public class SolverResult
    {
        public List<Point> Path { get; }
        public List<Point> VisitedInOrder { get; }
        public bool Success => Path.Count > 0;

        public SolverResult(List<Point> path, List<Point> visited)
        {
            Path = path ?? new();
            VisitedInOrder = visited ?? new();
        }
    }
    
}