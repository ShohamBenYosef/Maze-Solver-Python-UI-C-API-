using MazeCore.Models;

namespace MazeCore.Services
{
    public interface IMazeSolver
    {
        SolverResult Solve(Maze maze, Point start, Point end);
    }
}