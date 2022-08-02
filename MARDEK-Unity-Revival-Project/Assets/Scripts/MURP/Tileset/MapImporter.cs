using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class MapImporter : MonoBehaviour
{
    [SerializeField] TilesetImporter tilesetImporter;
    [SerializeField] string mapInfo = default;
    [SerializeField] Tilemap waterTilemap = null;
    [SerializeField] Tilemap waterBackgroundTilemap = null;
    [SerializeField] Tilemap terrainTilemap = null;
    [SerializeField] Tilemap objectsTilemap = null;

    private void OnValidate()
    {
        //GetTilesetImporter();
    }

    public void GetTilesetImporter()
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
        waterTilemap.ClearAllTiles();
        waterBackgroundTilemap.ClearAllTiles();
        terrainTilemap.ClearAllTiles();
        objectsTilemap.ClearAllTiles();
        var tileMatrix = tileArray.Split('[');
        for(int y = 0; y< tileMatrix.Length; y++)
        {
            var row = tileMatrix[y].Split(']')[0].Split(',');
            for(int x = 0; x < row.Length; x++)
            {
                var tileID = row[x];
                var tile = tilesetImporter.GetTileById(tileID);
                var position = new Vector3Int(x, -y, 0);
                var hasCollider = ((tile as Tile)?.colliderType == Tile.ColliderType.Grid
                    || (tile as AnimatedTile)?.m_TileColliderType == Tile.ColliderType.Grid);
                if (hasCollider)
                    objectsTilemap.SetTile(position, tile);
                else
                    terrainTilemap.SetTile(position, tile);
                var waterTile = tilesetImporter.PutWaterBellowTile(tileID);
                if(waterTile != null)
                {
                    waterTilemap.SetTile(position, waterTile);
                    if (waterTilemap.GetTile(position + Vector3Int.up) != null)
                        waterBackgroundTilemap.SetTile(position, tilesetImporter.GetTileById("water_background"));
                    else
                        waterBackgroundTilemap.SetTile(position, tilesetImporter.GetTileById("water_backwall"));
                }
            }
        }
    }
}