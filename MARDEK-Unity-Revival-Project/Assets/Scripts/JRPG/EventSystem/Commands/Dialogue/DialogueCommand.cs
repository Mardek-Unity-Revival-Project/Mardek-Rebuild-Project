using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCommand : OngoingCommand
{
    [ExtendedSO]
    [SerializeField] Dialogue dialogue;

    public override bool IsOngoing()
    {
        return DialogueManager.IsOngoing();
    }

    public override void Trigger()
    {
        DialogueManager.EnqueueDialogue(dialogue);
    }
}
