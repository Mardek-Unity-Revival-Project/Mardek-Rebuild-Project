using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    [System.Serializable]
    public class CharacterLines
    {
        [field: SerializeField] public CharacterBio Character { get; private set; }
        [field: TextArea(0,5)]
        [field: SerializeField] public List<string> Lines { get; private set; }
    }
}