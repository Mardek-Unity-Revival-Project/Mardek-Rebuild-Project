using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.Tilemaps;
using System.IO;

[CreateAssetMenu(menuName = "MURP/TilesetImporter")]
public class TilesetImporter : ScriptableObject
{
    public static List<TilesetImporter> tilesetImporters { get; private set; } = new List<TilesetImporter>();
    [SerializeField] Texture2D texture = null;
    const int tileSize = 16;
    Color WaterEncoding = new Color(0, 221, 255, 255) / 255;
    string TileFolderPath
    { 
        get
        {
            var tileImporterPath = AssetDatabase.GetAssetPath(this);
            var tileFolderPath = Path.Combine(Path.GetDirectoryName(tileImporterPath), "Imported Tiles");
            return tileFolderPath;
        }
    }

    [System.Serializable]
    class TileWrapper
    {
        static Color GreenEncoding = new Color(0, 255, 0, 255) / 255;
        static Color DarkGreenEncoding = new Color(0, .6f, 0, 1f);
        public string id;
        public Color encodingColor;
        public Sprite sprite;
        public Tile tile;

        public void UpdateTileProperties()
        {
            tile.sprite = sprite;
            if (encodingColor == GreenEncoding || encodingColor.Equals(DarkGreenEncoding))
                tile.colliderType = Tile.ColliderType.None;
            else
                tile.colliderType = Tile.ColliderType.Grid;
        }
    }
    
    [SerializeField] List<TileWrapper> tileWrappers = new List<TileWrapper>();

    private void OnValidate()
    {
        if (!tilesetImporters.Contains(this))
            tilesetImporters.Add(this);
    }
    [ContextMenu("Import")]
    void Import()
    {
        FileUtil.DeleteFileOrDirectory(TileFolderPath);
        string path = AssetDatabase.GetAssetPath(texture);
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        List<SpriteMetaData> slices = new List<SpriteMetaData>();
        tileWrappers = new List<TileWrapper>();

        // water tile
        slices.Add(new SpriteMetaData()
        {
            name = "water",
            rect = new Rect(10 * tileSize, texture.height - tileSize, tileSize, tileSize),
            alignment = 9,
            pivot = new Vector2(.5f, .5f)
        });
        tileWrappers.Add(new TileWrapper()
        {
            id = "water"
        });

        // encoded tiles
        for (int x = 0; x < texture.width / tileSize; x++)
        {
            for(int y = 1; y < texture.height / tileSize; y++)
            {
                var tileHeight = 1 + x / 10;
                // only consider the first tile for tiles with height bigger than 1
                if ((y-1) % tileHeight != 0) 
                    continue;
                // check if rect is out of bounds
                var rect = new Rect(x * tileSize, texture.height - (y + tileHeight) * tileSize, tileSize, tileSize * tileHeight);
                if (rect.y < 0 || rect.yMax > texture.height)
                    continue;
                // skip tile if it's not encoded
                var encodingColor = texture.GetPixel(x, texture.height - y);
                if (encodingColor.a == 0)
                    continue;
                
                var tileID = TileID(x, y);
                var sprite = new SpriteMetaData
                {
                    name = tileID,
                    rect = rect,
                    alignment = 9,
                    pivot = new Vector2(.5f, .5f / tileHeight)
                };
                slices.Add(sprite);
                tileWrappers.Add(new TileWrapper()
                {
                    id = tileID,
                    encodingColor = encodingColor
                });
            }
        }
        
        importer.spritesheet = slices.ToArray();
        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();

        var newName = texture.name.Substring(texture.name.LastIndexOf("_") + 1);
        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(this), newName);
        AssetDatabase.SaveAssets();

        var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(texture));
        for (int i = 0; i < tileWrappers.Count; i++)
            foreach (var sprite in sprites)
                if (string.Equals(sprite.name, tileWrappers[i].id))
                    tileWrappers[i].sprite = sprite as Sprite;
    }
    string TileID(int x, int y)
    {
        var firstDigit = 1 + x / 10;
        var lastDigits = (y-1)*10 + x % 10;
        return $"{firstDigit}{lastDigits}";
    }
    public TileBase GetTileById(string id)
    {
        foreach(var wrapper in tileWrappers)
            if (string.Equals(wrapper.id, id))
            {
                if(wrapper.tile == null)
                    CreateTile(wrapper);
                return wrapper.tile;
            }
        Debug.LogError($"Couldn't find tile of id = {id}, and failed to create one");
        return null;
    }
    void CreateTile(TileWrapper wrapper)
    {
        var newTile = CreateInstance<Tile>();
        wrapper.tile = newTile;
        wrapper.UpdateTileProperties();
        Directory.CreateDirectory(TileFolderPath);
        var tilePath = Path.Combine(TileFolderPath , $"{name}_{wrapper.id}.asset");
        AssetDatabase.CreateAsset(newTile, tilePath);
        Debug.Log("New tile created", newTile);
    }
    public TileBase TileBeneathTileOfId(string id)
    {
        foreach (var wrapper in tileWrappers)
            if (wrapper.id.Equals(id) && (wrapper.encodingColor == WaterEncoding)) 
                return GetTileById("water");
        return null;
    }
}