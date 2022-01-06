using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.CharacterSystem;

namespace MURP.UI {
    public class TurnIconUI : MonoBehaviour {

        public Character character { get; private set; }
        [SerializeField] TMPro.TMP_Text characterInitial;

        public void AssignCharacter(Character c) {
            character = c;
            characterInitial.text = character.CharacterInfo.displayName.Substring(0, 1);
        }

    }
}
