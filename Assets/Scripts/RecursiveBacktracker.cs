namespace FlatMango.Maze
{
    using UnityEngine;

    public sealed class RecursiveBacktracker
    {
        private Cell[,] cells;

        private int width;
        private int height;


        public Cell[,] Process(int width, int height)
        {
            this.width = width;
            this.height = height;

            cells = new Cell[width, height];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    cells[x, y] = new Cell(x, y);

            CarvePassageFrom(0, 0);

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    cells[x, y].borders = ~cells[x, y].borders;

            cells[0, 0].borders &= ~Direction.Down;
            cells[width - 1, height - 1].borders &= ~Direction.Up;

            return cells;
        }
        
        private void CarvePassageFrom(int x, int y)
        {
            Direction[] directions = new Direction[]
            {
                Direction.Up, Direction.Down,
                Direction.Left, Direction.Right
            };

            directions.Randomize();
            
            foreach (Direction direction in directions)
            {
                Vector2Int next = new Vector2Int(x, y) + direction.Delta;
                
                if ((0 <= next.y && next.y < height) && (0 <= next.x && next.x < width))
                {
                    if (cells[next.x, next.y].borders == Direction.None)
                    {
                        cells[x, y].borders |= direction;
                        cells[next.x, next.y].borders |= direction.Opposite;
                        
                        CarvePassageFrom(next.x, next.y);
                    }
                }
            }
        }
    }
}