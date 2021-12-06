using UnityEngine;

namespace MURP.EventSystem
{
    public class BoolBranchCommand : BranchCommand
    {
        [SerializeField] Object check;

        [SerializeField] CommandQueue ifTrueCommands = null;

        [SerializeField] CommandQueue ifFalseCommands = null;

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
                commandsBeingExecuted = ifTrueCommands;
            else
                commandsBeingExecuted = ifFalseCommands;

            base.Trigger();
        }
    }
}