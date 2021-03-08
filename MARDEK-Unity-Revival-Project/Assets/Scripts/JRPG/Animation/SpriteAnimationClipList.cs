using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    [CreateAssetMenu(menuName = "JRPG/AnimationClipList")]
    public class SpriteAnimationClipList : ScriptableObject
    {
        [SerializeField] List<SpriteAnimationClip> clips = new List<SpriteAnimationClip>();

        public SpriteAnimationClip GetClipByIndex(int i)
        {
            if (clips.Count > i)
                return clips[i];
            return null;
        }

        public SpriteAnimationClip GetClipByReference(MoveDirection reference)
        {
            if (reference == null)
                return null;
            foreach (SpriteAnimationClip clip in clips)
            {
                if (clip.indexBySOReference == reference)
                    return clip;
            }                    
            return null;
        }
    }
}
