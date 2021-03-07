using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : ScriptableObject
{
    [SerializeField] CharacterBio character = null;
    [SerializeField] List<string> dialogueLines = new List<string>();

    public string GetCharacterName()
    {
        if (character)
            return character.GetName();
        return "Null name";
    }

    public List<string> GetLines()
    {
        return dialogueLines;
    }
}
