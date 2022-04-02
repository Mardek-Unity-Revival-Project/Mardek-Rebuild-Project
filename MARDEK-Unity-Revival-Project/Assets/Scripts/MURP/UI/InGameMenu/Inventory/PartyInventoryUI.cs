using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class PartyInventoryUI : MonoBehaviour
    {
        static PartyInventoryUI instance;
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
            }
        }

        private void OnEnable()
        {
            instance = this;
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