***Maze Solver — Python UI + C# API***
  A cross-platform maze-building and pathfinding visualizer using:
  Python (Tkinter) → UI, drawing, animation.
  C# (.NET 9 Web API) → BFS, DFS, Dijkstra, A*.
  HTTP communication between them.

**Demos**
  Building:
  ![2025-11-17-16 13 36-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/c413bb7e-2d11-4f85-b331-95dc7cdcb012)
  
  Solving:
  ![2025-11-17-16 17 54-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/08a42f38-4143-4cc8-8a25-cfccd4ff0049)


**Features:**
  Python (Tkinter UI)
    Click-to-draw walls
    Start (green) and Goal (red) pre-set
    Dropdown to choose algorithm
    Animated visited cells (yellow)
    Animated final path (green)
    Live timer (“X ms”)
    Pause/Resume visualization
    Clear board button
    
  C# (asp.net)
    Implements robust pathfinding algorithms:
      Breadth-First Search (BFS)
      Depth-First Search (DFS)
      Dijkstra
      A* with Manhattan heuristic
      
    The API receives a grid and returns:
      Visited: all explored nodes
      Path: optimal solution path
      TimeMilliseconds: runtime

**Clean Folder Structure**

    MazeSolver/
    │
    ├── MazeCore/          # C# shared logic
    │   ├── Models/
    │   ├── Algorithms/
    │   └── MazeCore.csproj
    │
    ├── MazeBackend/       # C# Web API (ASP.NET)
    │   ├── Controllers/
    │   ├── Models/
    │   └── MazeBackend.csproj
    │
    ├── MazeUi/            # Python Tkinter UI
    │   ├── main.py
    │   ├── maze_canvas.py
    │   ├── api_client.py
    │   └── requirements.txt
    │
    └── README.md


**Starting Commands**

  dotnet build
  dotnet run
  
  python3 main.py
  (http://localhost:5131)

      
