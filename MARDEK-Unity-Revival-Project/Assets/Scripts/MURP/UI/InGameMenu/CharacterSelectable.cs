using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;
using UnityEngine.Serialization;

namespace MURP.UI
{
    public class CharacterSelectable : SelectableWithCurrentSelected<CharacterSelectable>
    {
        [SerializeField] Character character;
        public Character Character { get { return character; } }
    }
}