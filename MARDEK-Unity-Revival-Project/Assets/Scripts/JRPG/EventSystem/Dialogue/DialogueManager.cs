using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    static DialogueManager instance;

    Dialogue currentDialogue;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public static void EnqueueDialogue(Dialogue dialogue)
    {
        if (instance == null)
        {
            Debug.LogWarning("No DialogueManager instance found");
            foreach (string s in dialogue.GetEntries())
                Debug.Log(s);
        }
        else
            instance.currentDialogue = dialogue;
    }

    public static bool IsOngoing()
    {
        if (instance == null)
            return false;
        else
            return instance.currentDialogue != null;
    }
}
