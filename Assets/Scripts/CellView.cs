namespace FlatMango.Maze
{
    using UnityEngine;
    using UnityEngine.EventSystems;


    [DisallowMultipleComponent]
    public sealed class CellView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Cell cell;
        public event System.Action<Cell> PointerEnter;
        public event System.Action PointerExit;

        public void Init(Cell cell)
        {
            this.cell = cell;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter?.Invoke(cell);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExit?.Invoke();
        }

        private void OnDestroy()
        {
            PointerEnter = null;
            PointerExit = null;
        }
    }
}