using System.Collections.Generic;
using UnityEngine;
using MURP.EventSystem;

namespace MURP.Movement
{
    public class MoveCommand : OngoingCommand
    {
        [SerializeField] Movable target = null;
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