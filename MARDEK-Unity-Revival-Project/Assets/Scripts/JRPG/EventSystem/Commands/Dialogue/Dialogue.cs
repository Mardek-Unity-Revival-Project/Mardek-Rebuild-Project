using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : ScriptableObject
{
    [SerializeField] List<string> dialogueEntries = new List<string>();

    public List<string> GetEntries()
    {
        return dialogueEntries;
    }
}
