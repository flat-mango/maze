namespace FlatMango.Maze
{
    using UnityEngine;
    using System.Collections.Generic;


    public sealed class WallFollowSolver
    {
        public List<Cell> Solve(Cell start, Cell end, Cell[,] grid)
        {
            List<Cell> path = new List<Cell>() { start };

            Cell current = start;
            Direction direction = Direction.Up;


            while (current != end)
            {
                direction++;

                if (!current.borders.Contains(direction))
                {
                    Vector2Int delta = new Vector2Int(current.x, current.y) + direction.Delta;

                    if (0 <= delta.x && delta.x < grid.GetLength(0) && 0 <= delta.y && delta.y < grid.GetLength(1))
                    {
                        current = grid[delta.x, delta.y];
                        Add(path, current);
                        continue;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    direction--;

                    if (current.borders.Contains(direction))
                        continue;

                    Vector2Int delta = new Vector2Int(current.x, current.y) + direction.Delta;

                    if (0 <= delta.x && delta.x < grid.GetLength(0) && 0 <= delta.y && delta.y < grid.GetLength(1))
                    {
                        current = grid[delta.x, delta.y];
                        Add(path, current);
                        break;
                    }
                }

            }


            return path;
        }

        private void Add(List<Cell> path, Cell cell)
        {
            if (path.Contains(cell))
            {
                while (path[path.Count - 1] != cell)
                    path.RemoveAt(path.Count - 1);
            }
            else
                path.Add(cell);
        }
    }
}