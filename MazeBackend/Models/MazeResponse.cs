using System.Collections.Generic;
using MazeCore.Models;

namespace MazeBackendAPI.Models
{
    public class MazeResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public List<Point> Path { get; set; } = new();
        public List<Point> Visited { get; set; } = new();
        public double TimeMilliseconds { get; set; }
    }
}
