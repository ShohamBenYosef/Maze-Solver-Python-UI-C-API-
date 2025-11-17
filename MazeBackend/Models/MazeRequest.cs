namespace MazeBackendAPI.Models
{
    public class MazeRequest
    {
        public int Rows { get; set; }
        public int Cols { get; set; }

        // 0 = empty, 1 = wall
        public int[][] Grid { get; set; } = default!;

        public string Algorithm { get; set; } = "BFS";
    }
}
