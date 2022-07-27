using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CreateAssetMenu(menuName = "MURP/TilesetImporter")]
public class TilesetImporter : ScriptableObject
{
    [SerializeField] Texture2D texture = null;
    const int tileSize = 16;

    private void OnValidate()
    {
        Import();
    }

    [ContextMenu("Import")]
    void Import()
    {
        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        List<SpriteMetaData> sprites = new List<SpriteMetaData>();

        for(int x = 0; x < texture.width / tileSize; x++)
        {
            for(int y = 1; y < texture.height / tileSize; y++)
            {
                var tileHeight = 1 + x / 10;
                if (y % tileHeight != 0) // only consider the first tile for tiles with height bigger than 1
                    continue;
                var sprite = new SpriteMetaData();
                sprite.rect = new Rect(x * tileSize, texture.height - (y+1) * tileSize, tileSize, tileSize * tileHeight);
                sprite.name = TileID(x, y);
                sprite.pivot = new Vector2(tileSize / 2, tileSize / 2);
                sprites.Add(sprite);
            }
        }
        importer.spritesheet = sprites.ToArray();
        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();
    }

    string TileID(int x, int y)
    {
        var firstDigit = 1 + x / 10;
        var lastDigits = (y-1)*10 + x % 10;
        return $"{firstDigit}{lastDigits}";
    }
}