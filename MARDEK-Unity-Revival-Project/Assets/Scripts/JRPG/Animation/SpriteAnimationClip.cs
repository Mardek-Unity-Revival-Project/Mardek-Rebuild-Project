using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    [System.Serializable]
    public class SpriteAnimationClip
    {
        public MoveDirection indexBySOReference = null;
        public bool loop { get { return _loop; } }
        [SerializeField] bool _loop = true;
        [SerializeField] float durationMultiplier = 1f;
        [SerializeField] List<Sprite> spriteSequence = new List<Sprite>();

        public Sprite GetSprite(float time)
        {
            int index = Mathf.FloorToInt(spriteSequence.Count * time / durationMultiplier);
            return spriteSequence[index];
        }
    }
}