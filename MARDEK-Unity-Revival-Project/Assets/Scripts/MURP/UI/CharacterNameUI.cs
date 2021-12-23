using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.UI
{
    public class CharacterNameUI : MonoBehaviour
    {
        [SerializeField] CharacterUI characterUI;
        [SerializeField] TMPro.TMP_Text characteName;

        void Start()
        {
            characteName.text = characterUI.character.CharacterInfo == null ? "Null" : characterUI.character.CharacterInfo.displayName;
        }
    }
}