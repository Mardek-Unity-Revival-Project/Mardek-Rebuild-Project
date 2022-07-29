using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.IO;

[CreateAssetMenu(menuName = "2D/Tiles/WaveringTile")]
public class WaveringTile : AnimatedTile
{
    [SerializeField] int[] deltasByFrame = { 1, 1, 1, 15, 15, 15 };
    [ContextMenu("CreateAnimation")]
    public void CreateAnimation()
    {
        var originalSprite = m_AnimatedSprites[0];
        int tileWidth = (int)originalSprite.rect.width;
        int tileHeight = (int)originalSprite.rect.height;

        var animationDirectory = AssetDatabase.GetAssetPath(this);
        animationDirectory = Path.GetDirectoryName(animationDirectory);
        animationDirectory = Path.Combine(animationDirectory, $"{originalSprite.name}_Animation");
        Directory.CreateDirectory(animationDirectory);

        List<Sprite> list = new List<Sprite>();
        list.Add(originalSprite);

        Texture2D lastTex = new Texture2D(tileWidth, tileHeight);
        var originalX = (int)originalSprite.rect.x;
        var originalY = (int)originalSprite.rect.y;
        lastTex.SetPixels(originalSprite.texture.GetPixels(originalX,originalY, tileWidth, tileHeight));

        // 4 horizontal slices * 3 displacements of 1 pixel * 2 (back and forth loop) - 1 to not repeat the initial frame
        for (int i = 0; i < deltasByFrame.Length * 4 - 1; i++)
        {
            var tex = new Texture2D(tileWidth, tileHeight);
            tex.filterMode = originalSprite.texture.filterMode;
            tex.SetPixels(lastTex.GetPixels());
            for (int y = 0; y < 4; y++)
            {
                if (y == i % 4) // only displace 1/4 of tile per frame (top to bottom)
                {
                    var delta = deltasByFrame[i/4];
                    var ypos = tileHeight - (y + 1) * 4;
                    var leftColors = lastTex.GetPixels(0, ypos, delta, 4);
                    var rightColors = lastTex.GetPixels(delta, ypos, tileWidth - delta, 4);
                    tex.SetPixels(tileWidth - delta, ypos, delta, 4, leftColors);
                    tex.SetPixels(0, ypos, tileWidth - delta, 4, rightColors);
                }
            }
            lastTex = tex;
            AssetDatabase.CreateAsset(tex, Path.Combine(animationDirectory, $"tex{i}.asset"));
            var sprite = Sprite.Create(tex, new Rect(0, 0, tileWidth, tileHeight), originalSprite.pivot / originalSprite.rect.size, originalSprite.pixelsPerUnit);
            AssetDatabase.CreateAsset(sprite, Path.Combine(animationDirectory, $"sprite{i}.asset"));
            list.Add(sprite);
        }
        m_AnimatedSprites = list.ToArray();
    }
}