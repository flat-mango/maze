namespace FlatMango.Maze
{
    using UnityEngine;
    using System.Collections.Generic;


    public sealed class MazeGenerator : MonoBehaviour
    {
        [Space, SerializeField]
        private int width;
        [SerializeField]
        private int height;

        [Space, SerializeField]
        private CellView cellPrefab;
        [SerializeField]
        private Transform wallPrefab;

        private RecursiveBacktracker algorythm;
        private WallFollowSolver solver;

        Cell[,] grid;

        private void Start()
        {
            algorythm = new RecursiveBacktracker();
            solver = new WallFollowSolver();

            grid = algorythm.Process(width, height);

            DrawMaze();
        }

        private List<Cell> path;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Clear();

                grid = algorythm.Process(width, height);

                DrawMaze();
            }

            if (path != null && path.Count > 1)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Cell node = path[i];
                    Cell next = path[i + 1];

                    Debug.DrawLine(new Vector3(node.x, node.y) * 2, new Vector3(next.x, next.y) * 2, Color.cyan);
                }
            }
        }

        private void Clear()
        {
            Transform[] childs = transform.GetComponentsInChildren<Transform>();

            for (int i = 0; i < childs.Length; i++)
            {
                if(childs[i] != transform)
                    Destroy(childs[i].gameObject);
            }

            path = null;
        }

        private void OnPointerEnterCell(Cell cell)
        {
            path = solver.Solve(grid[0, 0], grid[cell.x, cell.y], grid);
        }

        private void OnPointerExitCell()
        {
            path = null;
        }

        private void DrawMaze()
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Cell cell = grid[x, y];

                    Vector3 position = (Vector3)cell * 2;

                    PlaceCell(cell, position);

                    if (cell.borders.Contains(Direction.Up))
                    {
                        var wall = Instantiate(wallPrefab, position + Vector3.up, Quaternion.identity, transform);
                        wall.localScale = new Vector3(3.0F, 1.0F, 1.0F);
                    }

                    if (cell.borders.Contains(Direction.Down))
                    {
                        var wall = Instantiate(wallPrefab, position - Vector3.up, Quaternion.identity, transform);
                        wall.localScale = new Vector3(3.0F, 1.0F, 1.0F);
                    }

                    if (cell.borders.Contains(Direction.Left))
                    {
                        var wall = Instantiate(wallPrefab, position - Vector3.right, Quaternion.identity, transform);
                        wall.localScale = new Vector3(1.0F, 3.0F, 1.0F);
                    }

                    if (cell.borders.Contains(Direction.Right))
                    {
                        var wall = Instantiate(wallPrefab, position + Vector3.right, Quaternion.identity, transform);
                        wall.localScale = new Vector3(1.0F, 3.0F, 1.0F);
                    }


                }
        }

        private void PlaceCell(Cell cell, Vector3 position)
        {
            CellView cellView = Instantiate(cellPrefab, transform, true);
            cellView.gameObject.name = cell.ToString();
            cellView.transform.position = position;

            cellView.Init(cell);
            cellView.PointerEnter += OnPointerEnterCell;
            cellView.PointerExit += OnPointerExitCell;
        }
    }
}