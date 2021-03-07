using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class SpriteAnimationClipList : ScriptableObject
    {
        [SerializeField] List<SpriteAnimationClip> clips = new List<SpriteAnimationClip>();

        public SpriteAnimationClip GetClipByReference(ScriptableObject reference)
        {
            if (reference == null)
                return null;
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
        [SerializeField] List<Sprite> spriteSequence = new List<Sprite>();

        public Sprite GetSprite(float time)
        {
            int index = Mathf.FloorToInt(spriteSequence.Count * time / durationMultiplier);
            return spriteSequence[index];
        }
    }
}
