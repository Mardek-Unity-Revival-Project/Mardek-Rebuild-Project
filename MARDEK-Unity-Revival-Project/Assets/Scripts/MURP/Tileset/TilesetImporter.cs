using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.IO;

[CreateAssetMenu(menuName = "MURP/TilesetImporter")]
public class TilesetImporter : ScriptableObject
{
    public Texture2D texture = null;
    public static List<TilesetImporter> tilesetImporters { get; private set; } = new List<TilesetImporter>();
    string TileFolderPath
    { 
        get
        {
            var tileImporterPath = AssetDatabase.GetAssetPath(this);
            var tileFolderPath = Path.Combine(Path.GetDirectoryName(tileImporterPath), $"{name}");
            return tileFolderPath;
        }
    }
    Color BlueEncoding = new Color(0, 221, 255, 255) / 255;
    Color CyanEncoding = new Color(0, 255, 255, 255) / 255;
    Color DarkBlueEncoding = new Color(0, 0, 200, 255) / 255;
    Color RedEncoding = new Color(255, 0, 0, 255) / 255;
    Color GreenEncoding = new Color(0, 255, 0, 255) / 255;
    Color DarkGreenEncoding = new Color(0, .6f, 0, 1f);
    const int tileSize = 16;

    [System.Serializable]
    class TileWrapper
    {
        public string id;
        public Color encodingColor;
        public Sprite sprite;
        public TileBase tile;
    }
    
    [SerializeField] List<TileWrapper> tileWrappers = new List<TileWrapper>();

    private void OnValidate()
    {
        if (!tilesetImporters.Contains(this))
            tilesetImporters.Add(this);
    }
    [ContextMenu("Import")]
    public void Import()
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
        // water background tile
        slices.Add(new SpriteMetaData()
        {
            name = "water_background",
            rect = new Rect(11 * tileSize, texture.height - tileSize, tileSize, tileSize),
            alignment = 9,
            pivot = new Vector2(.5f, .5f)
        });
        tileWrappers.Add(new TileWrapper()
        {
            id = "water_background"
        });
        // water backwall tile
        slices.Add(new SpriteMetaData()
        {
            name = "water_backwall",
            rect = new Rect(12 * tileSize, texture.height - tileSize, tileSize, tileSize),
            alignment = 9,
            pivot = new Vector2(.5f, .5f)
        });
        tileWrappers.Add(new TileWrapper()
        {
            id = "water_backwall"
        });
        // lava tile
        slices.Add(new SpriteMetaData()
        {
            name = "lava",
            rect = new Rect(13 * tileSize, texture.height - tileSize, tileSize, tileSize),
            alignment = 9,
            pivot = new Vector2(.5f, .5f)
        });
        tileWrappers.Add(new TileWrapper()
        {
            id = "lava"
        });
        // waterfall tile
        slices.Add(new SpriteMetaData()
        {
            name = "waterfall",
            rect = new Rect(14 * tileSize, texture.height - tileSize, tileSize, tileSize),
            alignment = 9,
            pivot = new Vector2(.5f, .5f)
        });
        tileWrappers.Add(new TileWrapper()
        {
            id = "waterfall"
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

                var encodingColor = texture.GetPixel(x, texture.height - y);
                // skip fully transparent tiles
                if(encodingColor.a == 0)
                {
                    var colors = texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
                    var skipTile = true;
                    foreach(var c in colors)
                        if(c.a != 0)
                        {
                            skipTile = false;
                            break;
                        }
                    if (skipTile)
                        continue;
                }
                
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

        var newName = texture.name.Substring(texture.name.IndexOf("_")+1);
        newName = newName.Substring(newName.IndexOf("_")+1);
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
        TileBase newTile;
        if (   wrapper.id == "water" 
            || wrapper.id == "water_background" 
            || wrapper.id == "water_backwall" 
            || wrapper.id == "lava")
        {
            var tile = CreateInstance<WaveringTile>();
            tile.m_AnimatedSprites = new List<Sprite>() { wrapper.sprite }.ToArray();
            tile.m_TileColliderType = Tile.ColliderType.None;
            newTile = tile;
        }
        else
        {
            var tile = CreateInstance<Tile>();
            tile.sprite = wrapper.sprite;
            var tileIsPassable = wrapper.encodingColor == GreenEncoding 
                || wrapper.encodingColor == DarkGreenEncoding
                || wrapper.encodingColor == CyanEncoding;
            if (tileIsPassable)
                tile.colliderType = Tile.ColliderType.None;
            else
                tile.colliderType = Tile.ColliderType.Grid;
            newTile = tile;
        }
        wrapper.tile = newTile;
        Directory.CreateDirectory(TileFolderPath);
        var tilePath = Path.Combine(TileFolderPath , $"{name}_{wrapper.id}.asset");
        AssetDatabase.CreateAsset(newTile, tilePath);
        Debug.Log("New tile created", newTile);

        var waveringTile = newTile as WaveringTile;
        if (waveringTile)
        {
            float animSpeed;
            if (wrapper.id == "water")
                animSpeed = 8;
            else
                animSpeed = 2;
            waveringTile.m_MinSpeed = animSpeed;
            waveringTile.m_MaxSpeed = animSpeed;
            waveringTile.CreateAnimation();
        }
    }
    public TileBase PutWaterBellowTile(string id)
    {
        foreach (var wrapper in tileWrappers)
            if (wrapper.id.Equals(id))
            {
                if (wrapper.encodingColor == BlueEncoding 
                    || wrapper.encodingColor == CyanEncoding 
                    || wrapper.encodingColor == DarkGreenEncoding) 
                    return GetTileById("water");
                if (wrapper.encodingColor.Equals(DarkBlueEncoding))
                    return GetTileById("waterfall");
                if (wrapper.encodingColor.Equals(RedEncoding))
                    return GetTileById("lava");
            }
        return null;
    }

    [MenuItem("Flash Importer/Import Tileset")]
    static void GenerateImporters()
    {
        var selectedTextures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);
        if (selectedTextures.Length == 0)
            throw new System.Exception("No tileset textures selected");
        foreach (var obj in selectedTextures)
        {
            var newImporter = CreateInstance<TilesetImporter>();
            newImporter.texture = obj as Texture2D;
            var path = Path.Combine("Assets", "Tiles", "Importers", $"{obj.name}.asset");
            AssetDatabase.CreateAsset(newImporter, path);
        }
    }
}