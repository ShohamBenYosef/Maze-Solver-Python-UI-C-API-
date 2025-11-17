import tkinter as tk

CELL = 25


class MazeCanvas(tk.Canvas):
    def __init__(self, root, rows, cols):
        width = cols * CELL
        height = rows * CELL

        super().__init__(root, width=width, height=height, bg="white")

        self.rows = rows
        self.cols = cols

        # start & goal
        self.start = (0, 0)
        self.goal = (rows - 1, cols - 1)

        # grid storage
        self.grid = []
        self.rects = []

        # Draw grid
        for r in range(rows):
            rects_row = []
            grid_row = []
            for c in range(cols):
                x1 = c * CELL
                y1 = r * CELL
                x2 = x1 + CELL
                y2 = y1 + CELL

                rect = self.create_rectangle(x1, y1, x2, y2, fill="white")
                rects_row.append(rect)

                grid_row.append({"is_wall": False})

            self.rects.append(rects_row)
            self.grid.append(grid_row)

        # paint start & goal
        self.paint_cell(self.start[0], self.start[1], "green")
        self.paint_cell(self.goal[0], self.goal[1], "red")

        # click to toggle wall
        self.bind("<Button-1>", self.toggle_wall)

    def paint_cell(self, r, c, color):
        self.itemconfig(self.rects[r][c], fill=color)

    def toggle_wall(self, event):
        c = event.x // CELL
        r = event.y // CELL

        # out of bounds
        if not (0 <= r < self.rows and 0 <= c < self.cols):
            return

        # cannot toggle start/goal
        if (r, c) == self.start or (r, c) == self.goal:
            return

        cell = self.grid[r][c]
        cell["is_wall"] = not cell["is_wall"]

        color = "black" if cell["is_wall"] else "white"
        self.paint_cell(r, c, color)

    def export_grid(self):
        return [[1 if self.grid[r][c]["is_wall"] else 0 for c in range(self.cols)] for r in range(self.rows)]

    def clear_path(self):
        for r in range(self.rows):
            for c in range(self.cols):
                if (r, c) == self.start:
                    self.paint_cell(r, c, "green")
                elif (r, c) == self.goal:
                    self.paint_cell(r, c, "red")
                else:
                    color = "black" if self.grid[r][c]["is_wall"] else "white"
                    self.paint_cell(r, c, color)
