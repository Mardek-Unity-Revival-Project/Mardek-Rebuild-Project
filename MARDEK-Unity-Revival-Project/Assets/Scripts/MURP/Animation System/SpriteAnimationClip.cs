using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.Core;

namespace MURP.Animation
{
    [System.Serializable]
    public class SpriteAnimationClip
    {
        public MoveDirection indexBySOReference = null;
        public bool loop { get { return _loop; } }
        [SerializeField] bool _loop = true;
        [SerializeField] float durationMultiplier = 1f;
        [SerializeField] List<Sprite> spriteSequence = new List<Sprite>();

        public Sprite GetSprite(float animationRatio)
        {
            int index = Mathf.FloorToInt(spriteSequence.Count * animationRatio / durationMultiplier);
            index = Mathf.Clamp(index, 0, spriteSequence.Count - 1);
            return spriteSequence[index];
        }
    }
}