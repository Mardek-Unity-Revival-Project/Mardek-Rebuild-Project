using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.EventSystem;

namespace MURP.Animation
{
    public class PlaySpriteAnimationCommand : OngoingCommand
    {
        [SerializeField] SpriteAnimator targetAnimator;
        [SerializeField] MoveDirection animationDirection = null;

        public override bool IsOngoing()
        {
            return targetAnimator.isAnimating && !targetAnimator.currentClipLoops; // don't wait for a looping animation to end
        }

        public override void Trigger()
        {
            targetAnimator.PlayClipByMoveDirectionReference(animationDirection);   
        }
    }
}