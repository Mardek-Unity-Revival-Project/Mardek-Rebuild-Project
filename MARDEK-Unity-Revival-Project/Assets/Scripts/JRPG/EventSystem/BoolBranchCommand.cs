using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class BoolBranchCommand : BranchCommand
    {
        [SerializeField] Object check;
        [CreateReference(typeof(CommandBase))]
        [SerializeReference] List<CommandBase> ifTrueCommands = null;
        [SerializeField] CommandList ifFalseCommands = new CommandList();

        CommandList ongoingCommandList = null;

        public override void Trigger()
        {
            IBoolCheck boolCheck = check as IBoolCheck;
            if(boolCheck == null)
            {
                Debug.LogError("branch check is null or not of required type");
                return;
            }
            if (boolCheck.GetBoolValue())
            {
                //ongoingCommandList = ifTrueCommands;
            }
            else
            {
                ongoingCommandList = ifFalseCommands;
            }
            if (ongoingCommandList != null) ongoingCommandList.Trigger();
        }

        public override void Update()
        {
            if (IsOngoing()) ongoingCommandList.Update();
        }

        public override bool IsOngoing()
        {
            if (ongoingCommandList == null)
                return false;
            return ongoingCommandList.IsOngoing();
        }
    }
}