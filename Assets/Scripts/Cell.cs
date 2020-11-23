namespace FlatMango.Maze
{
    using UnityEngine;


    public sealed class Cell
    {
        public int x, y;
        public Direction borders;


        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{x}, {y} -> {borders}";
        }

        public static explicit operator Vector3(Cell cell)
        {
            return new Vector3(cell.x, cell.y);
        }
    }
}