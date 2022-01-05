using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;
using UnityEngine.EventSystems;

namespace MURP.UI
{
    public class CharacterEquipmentUI : Selectable
    {
        static CharacterEquipmentUI selectedCharacterEquipment;
        Color unselectedColor = new Color(99f / 255f, 75f / 255f, 44f / 255f);
        Color selectedColor = new Color(41f / 255f, 57f / 255f, 106f / 255f);

        [SerializeField] Image backgroundBar = null;
        [SerializeField] List<SlotUI> slots = new List<SlotUI>();
        Character character { get; set; }

        protected void Awake()
        {
            backgroundBar.color = unselectedColor;            
        }
        
        public void SetCharacter(Character c)
        {
            if (c == null)
                gameObject.SetActive(false);
            else
            {
                gameObject.SetActive(true);
                character = c;
                for (int i = 0; i < 6; i++)
                    slots[i].SetSlot(character.inventory.GetSlot(i));
            }            
        }
        public override void Select()
        {
            if (selectedCharacterEquipment)
                selectedCharacterEquipment.Deselect();
            backgroundBar.color = selectedColor;
            selectedCharacterEquipment = this;
        }
        public override void Deselect()
        {
            backgroundBar.color = unselectedColor;
        }
    }
}