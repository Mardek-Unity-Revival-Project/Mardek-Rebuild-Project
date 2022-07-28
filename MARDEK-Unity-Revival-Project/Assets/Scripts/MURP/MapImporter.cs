using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class MapImporter : MonoBehaviour
{
    [SerializeField] TilesetImporter tilesetImporter;
    [SerializeField] string mapInfo = default;
    [SerializeField] Tilemap tilemap = null;

    private void OnValidate()
    {
        GetTilesetImporter();
    }

    void GetTilesetImporter()
    {
        var tilesetName = mapInfo.Substring(mapInfo.IndexOf("tileset = ")).Split('\"')[1];
        foreach (var ti in TilesetImporter.tilesetImporters)
            if(ti && ti.name == tilesetName)
                tilesetImporter = ti;
    }

    [ContextMenu("DrawMap")]
    public void DrawMap()
    {
        var tileArray = mapInfo.Substring(mapInfo.IndexOf("map = "));
        tileArray = tileArray.Substring(tileArray.IndexOf('[')).Split(';')[0].Substring(2);
        tilemap.ClearAllTiles();
        var tileMatrix = tileArray.Split('[');
        for(int y = 0; y< tileMatrix.Length; y++)
        {
            var row = tileMatrix[y].Split(']')[0].Split(',');
            for(int x = 0; x < row.Length; x++)
            {
                var tile = tilesetImporter.GetTileById(row[x]);
                tilemap.SetTile(new Vector3Int(x, tileMatrix.Length - y - 1, 0), tile);
            }
        }
    }
}