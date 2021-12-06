using UnityEngine;

namespace MURP
{
    [ExecuteInEditMode, SelectionBase]
    public class GridObject : MonoBehaviour
    {
        void Update()
        {
            if (!Application.isPlaying)
                EditorUpdate();
        }

        void EditorUpdate()
        {
            float z = transform.position.z;
            Vector3 pos = Utilities2D.SnapPositionToGrid(transform.position);
            pos.z = z;
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