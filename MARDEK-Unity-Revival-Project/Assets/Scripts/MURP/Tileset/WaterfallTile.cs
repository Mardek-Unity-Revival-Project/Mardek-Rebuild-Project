using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.IO;

public class WaterfallTile : AnimatedTile
{
    [ContextMenu("CreateAnimation")]
    public void CreateAnimation()
    {
        var originalSprite = m_AnimatedSprites[0];
        var animationDirectory = AssetDatabase.GetAssetPath(this);
        animationDirectory = Path.GetDirectoryName(animationDirectory);
        animationDirectory = Path.Combine(animationDirectory, $"{originalSprite.name}_Animation");
        Directory.CreateDirectory(animationDirectory);

        List<Sprite> list = new List<Sprite>();
        list.Add(originalSprite);

        int tileWidth = (int)originalSprite.rect.width;
        int tileHeight = (int)originalSprite.rect.height;
        Texture2D lastTex = new Texture2D(tileWidth, tileHeight);
        var originalX = (int)originalSprite.rect.x;
        var originalY = (int)originalSprite.rect.y;
        lastTex.SetPixels(originalSprite.texture.GetPixels(originalX, originalY, tileWidth, tileHeight));

        for(int i = 0; i < tileHeight-1; i++)
        {
            var tex = new Texture2D(tileWidth, tileHeight);
            tex.filterMode = originalSprite.texture.filterMode;
            tex.SetPixels(lastTex.GetPixels());

            var topColors = lastTex.GetPixels(0, 0, tileWidth, 1);
            var bottomColors = lastTex.GetPixels(0, 1, tileWidth, tileHeight-1);
            tex.SetPixels(0, tileWidth-1, tileWidth, 1, topColors);
            tex.SetPixels(0, 0, tileWidth, tileWidth - 1, bottomColors);

            lastTex = tex;
            AssetDatabase.CreateAsset(tex, Path.Combine(animationDirectory, $"tex{i}.asset"));
            var sprite = Sprite.Create(tex, new Rect(0, 0, tileWidth, tileHeight), originalSprite.pivot / originalSprite.rect.size, originalSprite.pixelsPerUnit);
            AssetDatabase.CreateAsset(sprite, Path.Combine(animationDirectory, $"sprite{i}.asset"));
            list.Add(sprite);
        }
        m_AnimatedSprites = list.ToArray();
    }
}