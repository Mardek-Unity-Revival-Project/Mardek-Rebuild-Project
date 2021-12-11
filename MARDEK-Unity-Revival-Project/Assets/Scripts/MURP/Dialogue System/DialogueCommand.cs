using UnityEngine;
using MURP.EventSystem;

namespace MURP.DialogueSystem
{
    public class DialogueCommand : OngoingCommand
    {
        [SerializeField] Dialogue dialogue = null;

        public override bool IsOngoing()
        {
            return DialogueManager.isOngoing;
        }

        public override void Trigger()
        {
            DialogueManager.EnqueueDialogue(dialogue);
        }
    }
}