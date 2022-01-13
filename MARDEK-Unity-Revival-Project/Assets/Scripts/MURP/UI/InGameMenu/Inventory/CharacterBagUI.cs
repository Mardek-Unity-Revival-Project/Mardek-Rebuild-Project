using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class CharacterBagUI : MonoBehaviour
    {
        [SerializeField] List<SlotUI> slots = new List<SlotUI>();

        public void SetCharacter(Character character)
        {
            if(character.CharacterInfo)
                for (int i = 0; i < 64; i++)
                    slots[i].SetSlot(character.inventory.GetSlot(i+6));
        }

        public void PropagateSubmenuController(SubmenuLayoutController submenuController, SelectableLayout partyLayout)
        {
            foreach (SlotUI slot in slots) slot.SetSubmenuController(submenuController, partyLayout);
        }
    }
}
