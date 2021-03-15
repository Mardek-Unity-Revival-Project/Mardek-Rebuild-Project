using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    public class DialogueCommand : OngoingCommand
    {
        [SerializeField] List<Dialogue> dialogues = new List<Dialogue>();

        public override bool IsOngoing()
        {
            return DialogueManager.isOngoing;
        }

        public override void Trigger()
        {
            DialogueManager.EnqueueDialogue(dialogues);
        }
    }
}
