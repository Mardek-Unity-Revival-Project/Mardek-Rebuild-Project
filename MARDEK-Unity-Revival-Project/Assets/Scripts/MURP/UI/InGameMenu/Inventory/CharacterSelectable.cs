using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class CharacterSelectable : Selectable
    {
        [SerializeField] Character character;
        [SerializeField] Text characterName;
        [SerializeField] Image backgroundBar = null;
        Color unselectedColor = new Color(99f / 255f, 75f / 255f, 44f / 255f);
        Color selectedColor = new Color(41f / 255f, 57f / 255f, 106f / 255f);

        public static Character currentSelected { get; private set; }

        private void OnEnable()
        {
            var info = character.CharacterInfo;
            if (info != null)
            {
                characterName.text = info.displayName;
            }
            backgroundBar.color = unselectedColor;
        }
        public override void Select(bool playSFX = true)
        {
            currentSelected = character;
            backgroundBar.color = selectedColor;
            base.Select(playSFX: playSFX);
        }
        public override void Deselect()
        {
            base.Deselect();
            backgroundBar.color = unselectedColor;
        }
    }
}