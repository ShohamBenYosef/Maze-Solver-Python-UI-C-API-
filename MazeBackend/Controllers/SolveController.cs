using Microsoft.AspNetCore.Mvc;
using MazeBackendAPI.Models;
using MazeCore.Models;
using MazeCore.Services;
using System.Diagnostics;

namespace MazeBackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolveController : ControllerBase
    {
        [HttpPost("solve")]
        public IActionResult Solve([FromBody] MazeRequest request)
        {
            if (request.Grid == null)
                return BadRequest("Grid is null");

            // Convert int[][] to CellType[,]
            var grid = new CellType[request.Rows, request.Cols];

            for (int r = 0; r < request.Rows; r++)
                for (int c = 0; c < request.Cols; c++)
                    grid[r, c] = request.Grid[r][c] == 1 ? CellType.Wall : CellType.Empty;

            var maze = new Maze(request.Rows, request.Cols, grid);

            var start = new Point(0, 0);
            var goal = new Point(request.Rows - 1, request.Cols - 1);

            var solver = MazeSolverFactory.Get(request.Algorithm);

            var sw = Stopwatch.StartNew();
            var result = solver.Solve(maze, start, goal);
            sw.Stop();

            var response = new MazeResponse
            {
                Success = result.Success,
                Message = result.Success ? "OK" : "No path found",
                Path = result.Path,
                Visited = result.VisitedInOrder,
                TimeMilliseconds = sw.Elapsed.TotalMilliseconds
            };

            return Ok(response);
        }
    }
}
