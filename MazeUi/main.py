import tkinter as tk
from api_client import call_solver
from maze_canvas import MazeCanvas

ROWS = 20
COLS = 20


class MazeApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Maze Solver - API Version")

        # Canvas (maze drawer)
        self.maze = MazeCanvas(root, ROWS, COLS)
        self.maze.pack()

        # Algorithm selector
        self.algorithm = tk.StringVar(value="BFS")
        tk.OptionMenu(root, self.algorithm, "BFS", "DFS", "Dijkstra", "A*").pack()

        # Toolbar
        toolbar = tk.Frame(root)
        toolbar.pack(pady=5)

        tk.Button(toolbar, text="Solve", command=self.solve).grid(row=0, column=0)
        tk.Button(toolbar, text="Clear", command=self.maze.clear_path).grid(row=0, column=2)

        self.paused = False
        self.pause_button = tk.Button(toolbar, text="Pause", command=self.toggle_pause)
        self.pause_button.grid(row=0, column=1)

        self.time_label = tk.Label(toolbar, text="Time: N/A")
        self.time_label.grid(row=0, column=3)


    def solve(self):
        print("Solve started!")

        self.paused = False
        self.pause_button.config(text="Pause")
        # Convert maze walls into grid matrix
        grid = self.maze.export_grid()

        payload = {
            "Rows": ROWS,
            "Cols": COLS,
            "Grid": grid,
            "Algorithm": self.algorithm.get()
        }

        print("\n====== SENDING TO API ======")
        print(payload)

        # Call API
        res = call_solver(payload)

        print("\n====== API RESPONSE ======")
        print(res)

        # ----- FIXED JSON KEYS (lowercase) -----
        if not res.get("success", False):
            print("Error:", res.get("message", "Unknown error"))
            self.time_label.config(text="Time: Error")
            return

        # Display time
        ms = res.get("timeMilliseconds")
        if ms is not None:
            self.time_label.config(text=f"Time: {ms:.3f} ms")
        else:
            self.time_label.config(text="Time: N/A")

        # Clear previous visualization
        self.maze.clear_path()

        # Draw visited (yellow)
        for p in res["visited"]:

            while self.paused:
                self.root.update()
                self.root.after(50)

            r, c = p["row"], p["col"]
            if (r, c) != self.maze.start and (r, c) != self.maze.goal:
                self.maze.paint_cell(r, c, "yellow")
                self.root.update()
                self.root.after(5)

        # Draw final path (green)
        for p in res["path"]:
            r, c = p["row"], p["col"]
            if (r, c) != self.maze.start and (r, c) != self.maze.goal:
                self.maze.paint_cell(r, c, "green")
                self.root.update()
                self.root.after(20)

    def toggle_pause(self):
        self.paused = not self.paused
        self.pause_button.config(text="Resume" if self.paused else "Pause")

if __name__ == "__main__":
    root = tk.Tk()
    app = MazeApp(root)
    root.mainloop()
