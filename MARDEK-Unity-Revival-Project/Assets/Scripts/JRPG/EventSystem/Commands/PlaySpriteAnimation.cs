using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class PlaySpriteAnimation : OngoingCommand
    {
        [SerializeField] SpriteAnimator targetAnimator;
        [SerializeField] MoveDirection animationDirection = null;

        public override bool IsOngoing()
        {
            return targetAnimator.isAnimating;
        }

        public override void Trigger()
        {
            targetAnimator.ChangeClipByReferecen(animationDirection);   
        }
    }
}
