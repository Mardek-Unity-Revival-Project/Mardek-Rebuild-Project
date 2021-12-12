using System.Collections.Generic;
using UnityEngine;
using MURP.EventSystem;

namespace MURP.DialogueSystem
{
    public class ChoicesCommand : BranchCommand
    {
        [SerializeField] Dialogue choicesDialogue = null;
        [SerializeField] List<CommandQueue> commandsByChoice = new List<CommandQueue>();

        public override void Trigger()
        {
            commandsBeingExecuted = null;
            ChoicesManager.TriggerChoices(choicesDialogue);
        }

        public override bool IsOngoing()
        {
            if(commandsBeingExecuted == null)
            {
                int index = ChoicesManager.GetChosenIndex();
                if(index > -1)
                {
                    commandsBeingExecuted = commandsByChoice[index];
                    commandsBeingExecuted.TriggerFirst();
                }
                return true;
            }
            return base.IsOngoing();
        }
    }
}