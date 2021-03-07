using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class MoveCommand : OngoingCommand
    {
        [SerializeField] Movement target = null;
        [SerializeField] List<MoveDirection> moves;

        public override bool IsOngoing()
        {
            return target.isMoving;
        }

        public override void Trigger()
        {
            target.EnqueueMoves(moves);
        }
    }
}
