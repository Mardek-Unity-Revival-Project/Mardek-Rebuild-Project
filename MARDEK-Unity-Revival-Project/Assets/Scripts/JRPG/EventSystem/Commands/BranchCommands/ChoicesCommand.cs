using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class ChoicesCommand : BranchCommand
    {
        [SerializeField] Dialogue choicesDialogue = null;
        [SerializeField] List<CommandQueue> commandsByChoice = new List<CommandQueue>();

        public override void Trigger()
        {
            ChoicesManager.TriggerChoices(choicesDialogue);
        }
    }
}