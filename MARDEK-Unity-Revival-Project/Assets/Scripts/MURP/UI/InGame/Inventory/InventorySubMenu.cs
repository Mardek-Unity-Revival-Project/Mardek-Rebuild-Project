using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;
using MURP.Inventory;
using System.Collections.Generic;

namespace MURP.UI
{
    public class InventorySubMenu : FocusSubMenu
    {
        const int NUM_EQUIPMENT_SLOTS = 6;
        const int NUM_BASIC_SLOTS = 64;

        [SerializeField] GridLayoutGroup slotsLayout;
        [SerializeField] GameObject slotPrefab;
        
        CursorSlotUI cursorItem;
        Party theParty;
        SlotGrid slotGrid;
        Slot cursorSlot = new Slot(null, 0, new List<EquipmentCategory>(), true, true);
        bool isActive = false;
        System.Action focusAction;

        override public void SetActive()
        {
            this.slotGrid.UpdateSlots(this.cursorSlot, this.focusAction);
            this.cursorItem = new CursorSlotUI(this.cursorSlot);
            this.isActive = true;
        }

        public override void SetInActive()
        {
            this.isActive = false;
        }

        void Update()
        {
            if (this.isActive)
            {
                this.cursorItem.Update();
            }
        }

        public override bool StopFocus()
        {
            return this.cursorSlot.IsEmpty();
        }

        override public void SetParty(Party theParty)
        {
            this.theParty = theParty;
            ConstructSlots();
        }

        public void SetForceFocusAction(System.Action forceFocusAction)
        {
            this.focusAction = forceFocusAction;
        }

        void ConstructSlots()
        {
            List<GameObject> slotComponents = new List<GameObject>(NUM_BASIC_SLOTS);
            for (int index = 0; index < NUM_BASIC_SLOTS; index++)
            {
                GameObject slotComponent = Instantiate(this.slotPrefab);
                slotComponent.transform.SetParent(this.slotsLayout.transform, false);
                slotComponents.Add(slotComponent);
            }

            // TODO Make it possible to switch to inventory of other party member
            Inventory.Inventory testInventory = this.theParty.Characters[0].inventory;
            this.slotGrid = new SlotGrid(slotComponents, testInventory, NUM_EQUIPMENT_SLOTS, NUM_BASIC_SLOTS);
        }
    }
}
