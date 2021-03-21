using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class BoolBranch : BranchCommand
    {
        [SerializeField] Object check;
        [CreateReference(typeof(CommandBase))]
        [SerializeReference] List<CommandBase> ifTrueCommands = null;
        [CreateReference(typeof(CommandBase))]
        [SerializeReference] List<CommandBase> ifFalseCommands = null;

        public override void Trigger()
        {
            if (IsOngoing())
            {
                Debug.LogWarning("Trying to trigger event, but this event is already ongoing");
                return;
            }

            IBoolCheck boolCheck = check as IBoolCheck;
            if(boolCheck == null)
            {
                Debug.LogError("branch check is null or not of required type");
                return;
            }

            if (boolCheck.GetBoolValue())
                commandsBeingExecuted = new List<CommandBase>(ifTrueCommands);
            else
                commandsBeingExecuted = new List<CommandBase>(ifFalseCommands);

            base.Trigger();
        }
    }
}