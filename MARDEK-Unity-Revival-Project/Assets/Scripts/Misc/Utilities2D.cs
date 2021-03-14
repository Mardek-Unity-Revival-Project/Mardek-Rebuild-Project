using UnityEngine;

public class Utilities2D
{
    static Grid _grid = null;
    public static Grid grid
    {
        get
        {
            if (_grid == null)
                _grid = Object.FindObjectOfType<Grid>();
            return _grid;
        }
    }

    public static Vector3 SnapPositionToGrid(Vector3 pos)
    { 
        if (grid)
        {
            pos = grid.WorldToCell(pos);
            pos += grid.cellSize / 2;
        }
        return pos;
    }

    public static void SetTransformPosition(Transform transform, Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
