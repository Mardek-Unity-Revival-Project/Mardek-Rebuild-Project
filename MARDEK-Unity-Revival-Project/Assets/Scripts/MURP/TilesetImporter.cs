using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "MURP/TilesetImporter")]
public class TilesetImporter : ScriptableObject
{
    public static List<TilesetImporter> tilesetImporters { get; private set; } = new List<TilesetImporter>();
    [SerializeField] Texture2D texture = null;
    //[SerializeField] TextureImporter importer = null;
    [SerializeField] List<TileWrapper> tileWrappers = new List<TileWrapper>();
    const int tileSize = 16;

    string TileID(int x, int y)
    {
        var firstDigit = 1 + x / 10;
        var lastDigits = (y-1)*10 + x % 10;
        return $"{firstDigit}{lastDigits}";
    }
    private void OnValidate()
    {
        if (!tilesetImporters.Contains(this))
            tilesetImporters.Add(this);
        //Import();
    }
    [ContextMenu("Import")]
    void Import()
    {
        string path = AssetDatabase.GetAssetPath(texture);
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        List<SpriteMetaData> sprites = new List<SpriteMetaData>();

        for(int x = 0; x < texture.width / tileSize; x++)
        {
            for(int y = 1; y < texture.height / tileSize; y++)
            {
                var tileHeight = 1 + x / 10;
                if ((y-1) % tileHeight != 0)
                {
                    Debug.Log($"skipping tile at {x},{y}");
                    // only consider the first tile for tiles with height bigger than 1
                    continue;
                }
                var sprite = new SpriteMetaData
                {
                    name = TileID(x, y),
                    rect = new Rect(x * tileSize, texture.height - (y + tileHeight) * tileSize, tileSize, tileSize * tileHeight),
                    alignment = 9,
                    pivot = new Vector2(.5f, .5f / tileHeight)
                };
                sprites.Add(sprite);
            }
        }
        importer.spritesheet = sprites.ToArray();
        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();

        var newName = texture.name.Substring(texture.name.LastIndexOf("_") + 1);
        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(this), newName);
        AssetDatabase.SaveAssets();
    }
    [System.Serializable]
    struct TileWrapper
    {
        public string id;
        public TileBase tile;
    }
    public TileBase GetTileById(string id)
    {
        foreach(var tileWrapper in tileWrappers)
        {
            if (tileWrapper.id == id)
                return tileWrapper.tile;
        }
        var tile = CreateInstance<Tile>();
        var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(texture));
        foreach (var sprite in sprites)
            if (string.Equals(sprite.name, id))
            {
                tile.sprite = sprite as Sprite;
                var tilePath = AssetDatabase.GetAssetPath(this);
                tilePath = System.IO.Path.GetDirectoryName(tilePath);
                tilePath = System.IO.Path.Combine(tilePath, $"{id}.asset");
                AssetDatabase.CreateAsset(tile, tilePath);
                tileWrappers.Add(new TileWrapper() { id = id, tile = tile });
                return tile;
            }
        Debug.LogError($"couldn't find tile of id = {id}, and failed to create one");
        return null;
    }
}