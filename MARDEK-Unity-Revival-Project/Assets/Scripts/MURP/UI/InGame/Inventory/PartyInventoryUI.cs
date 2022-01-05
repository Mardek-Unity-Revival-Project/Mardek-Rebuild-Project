using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class PartyInventoryUI : SelectableLayout
    {
        [SerializeField] Party party = null;
        [SerializeField] CharacterBagUI characterBagUI;

        private void Awake()
        {
            for (int i = 0; i < party.Characters.Count; i++)
                if (i < selectables.Length)
                    (selectables[i] as CharacterEquipmentUI).SetCharacter(party.Characters[i]);
            characterBagUI.SetCharacter(party.Characters[0]);
        }
    }
}