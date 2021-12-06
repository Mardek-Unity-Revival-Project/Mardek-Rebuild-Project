using System.Collections.Generic;
using UnityEngine;
using MURP.EventSystem;

namespace MURP.Dialogue
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
                    Debug.Log($"Choose option {index+1}");
                    commandsBeingExecuted = commandsByChoice[index];
                    commandsBeingExecuted.TriggerFirst();
                }
                return true;
            }
            else
                return base.IsOngoing();
        }
    }
}