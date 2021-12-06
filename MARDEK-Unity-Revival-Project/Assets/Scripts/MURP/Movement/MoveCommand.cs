using System.Collections.Generic;
using UnityEngine;
using MURP.EventSystem;

namespace MURP
{
    public class MoveCommand : OngoingCommand
    {
        [SerializeField] Movement target = null;
        [SerializeField] List<MoveDirection> moves;

        public override bool IsOngoing()
        {
            return target && target.isMoving;
        }

        public override void Trigger()
        {
            if (target == null)
                target = PlayerController.GetPlayerMovement();
            if(target)
                target.EnqueueMoves(moves);
        }
    }
}