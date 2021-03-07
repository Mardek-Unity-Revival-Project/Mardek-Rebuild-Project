using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationClipList : ScriptableObject
{
    [SerializeField] List<SpriteAnimationClip> clips;

    public SpriteAnimationClip GetClipByReference(ScriptableObject reference)
    {
        if (reference == null && clips.Count > 0)
            return clips[0];
        foreach (SpriteAnimationClip clip in clips)
            if (clip.indexBySOReference == reference)
                return clip;
        return null;
    }
}

[System.Serializable]
public class SpriteAnimationClip
{
    [SerializeField] public ScriptableObject indexBySOReference = null;
    [SerializeField] float durationMultiplier = 1f;
    [SerializeField] List<Sprite> spriteSequence;

    public Sprite GetSprite(float time)
    {
        int index = Mathf.FloorToInt(spriteSequence.Count * time / durationMultiplier);
        return spriteSequence[index];
    }
}
