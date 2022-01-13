using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class PartyInventoryUI : MonoBehaviour
    {
        static PartyInventoryUI instance;
        [SerializeField] Party party = null;
        [SerializeField] List<CharacterEquipmentUI> characterEquipments;
        [SerializeField] CharacterBagUI characterBagUI;
        [SerializeField] SubmenuLayoutController submenuController;
        [SerializeField] SelectableLayout partyLayout;

        Character selectedCharacter;
        public static Character SelectedCharacter
        {
            get
            {
                return instance.selectedCharacter;
            }
            set
            {
                instance.selectedCharacter = value;
                instance.UpdateBagUI();
            }
        }

        private void OnEnable()
        {
            instance = this;
            PropagateSubmenuController();
            UpdateEquipmentUI();
            if(selectedCharacter == null)
                characterEquipments[0].Select(playSFX: false);
            UpdateBagUI();            
        }

        void PropagateSubmenuController()
        {
            foreach (var equipmentUI in characterEquipments) equipmentUI.PropagateSubmenuController(submenuController, partyLayout);
            characterBagUI.PropagateSubmenuController(submenuController, partyLayout);
        }

        void UpdateEquipmentUI()
        {
            for (int i = 0; i < characterEquipments.Count; i++)
            {
                characterEquipments[i].gameObject.SetActive(false);
                if (i < party.Characters.Count)
                {
                    characterEquipments[i].SetCharacter(party.Characters[i]);
                    characterEquipments[i].gameObject.SetActive(true);
                }
            }
        }

        void UpdateBagUI()
        {
            characterBagUI.SetCharacter(selectedCharacter);
        }

        public void TryExitInventory()
        {
            if (SlotCursor.instance.IsEmpty())
            {
                partyLayout.enabled = false;
                submenuController.Focus();
            }
        }
    }
}