using UnityEngine;

namespace JRPG
{
    [ExecuteInEditMode]
    [SelectionBase]
    public class GridObject : MonoBehaviour
    {
        private void OnValidate()
        {
            enabled = true;
        }

        void Update()
        {
            if (!Application.isPlaying)
                EditorUpdate();
        }

        void EditorUpdate()
        {
            Vector3 pos = Utilities2D.SnapPositionToGrid(transform.position);
            if (transform.position != pos)
                transform.position = pos;
        }

        private void OnDrawGizmos()
        {
            Grid grid = Utilities2D.grid;
            if (grid)
                Gizmos.DrawWireCube(transform.position, grid.cellSize);
        }
    }
}
