using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class CharacterEquipmentUI : Selectable
    {
        Color unselectedColor = new Color(99f / 255f, 75f / 255f, 44f / 255f);
        Color selectedColor = new Color(41f / 255f, 57f / 255f, 106f / 255f);

        [SerializeField] Image backgroundBar = null;
        [SerializeField] Text characterName;
        [SerializeField] List<SlotUI> slots = new List<SlotUI>();
        Character character { get; set; }

        protected void Awake()
        {
            backgroundBar.color = unselectedColor;            
        }
        public void SetCharacter(Character c)
        {
            character = c;
            characterName.text = character.CharacterInfo.displayName;
            for (int i = 0; i < 6; i++)
                slots[i].SetSlot(character.inventory.GetSlot(i));
        }

        public void PropagateSubmenuController(SubmenuLayoutController submenuController, SelectableLayout partyLayout)
        {
            foreach (SlotUI slot in slots) slot.SetSubmenuController(submenuController, partyLayout);
        }

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX: playSFX);
            backgroundBar.color = selectedColor;
            PartyInventoryUI.SelectedCharacter = character;
        }
        public override void Deselect()
        {
            base.Deselect();
            backgroundBar.color = unselectedColor;
        }
    }
}