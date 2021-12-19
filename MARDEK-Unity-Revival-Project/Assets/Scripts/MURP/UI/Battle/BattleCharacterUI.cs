using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class BattleCharacterUI : MonoBehaviour
    {
        Character character = null;
        [SerializeField] TMPro.TMP_Text characteName;

        public void AssignCharacter(Character c)
        {
            character = c;
            characteName.text = character.CharacterInfo == null ? "Null" : character.CharacterInfo.displayName;
        }
    }
}