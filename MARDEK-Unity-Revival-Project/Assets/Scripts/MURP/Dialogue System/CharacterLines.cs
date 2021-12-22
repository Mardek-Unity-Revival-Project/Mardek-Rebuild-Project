using System.Collections.Generic;
using UnityEngine;
using CharacterInfo = MURP.CharacterSystem.CharacterInfo;

namespace MURP.DialogueSystem
{
    [System.Serializable]
    public class CharacterLines
    {
        [field: SerializeField] public CharacterInfo Character { get; private set; }
        [field: TextArea(0,5)]
        [field: SerializeField] public List<string> Lines { get; private set; }

        public CharacterLines(CharacterInfo info, List<string> lines)
        {
            Character = info;
            Lines = lines;
        }
    }
}