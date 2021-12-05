using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG {
    public abstract class BranchCommand : OngoingCommand
    {
        protected CommandQueue commandsBeingExecuted = null;

        public override void UpdateCommand()
        {
            if(commandsBeingExecuted != null)
                commandsBeingExecuted.TryAdvanceQueue();
        }

        public override bool IsOngoing()
        {
            return waitForExecutionEnd && commandsBeingExecuted != null && commandsBeingExecuted.isOngoing;
        }

        public override void Trigger()
        {
            if(commandsBeingExecuted != null)
                commandsBeingExecuted.TriggerFirst();
        }
    }
}
