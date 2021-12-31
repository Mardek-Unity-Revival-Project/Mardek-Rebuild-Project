using UnityEngine;
using MURP.Inventory;
using System.Collections.Generic;

namespace MURP.UI
{
    public class SlotGrid
    {
        readonly List<GameObject> slotComponents;
        readonly Inventory.Inventory inventory;
        readonly int firstInventorySlotIndex;
        readonly int numSlots;

        public SlotGrid(List<GameObject> slotComponents, Inventory.Inventory inventory, int firstInventorySlotIndex, int numSlots)
        {
            this.slotComponents = slotComponents;
            this.inventory = inventory;
            this.firstInventorySlotIndex = firstInventorySlotIndex;
            this.numSlots = numSlots;
        }

        public void UpdateSlots(Slot cursorSlot, SelectedItemInfo selectedItemInfo, System.Action focusAction)
        {
            for (int uiSlotIndex = 0; uiSlotIndex < this.numSlots; uiSlotIndex++)
            {
                int inventorySlotIndex = this.firstInventorySlotIndex + uiSlotIndex;
                Slot inventorySlot = this.inventory.GetSlot(inventorySlotIndex);
                GameObject uiSlot = this.slotComponents[uiSlotIndex];

                SlotUI slotScript = uiSlot.GetComponent<SlotUI>();
                slotScript.SetSelectedItemInfo(selectedItemInfo);
                slotScript.SetFocusAction(focusAction);
                slotScript.SetSlot(inventorySlot);
                slotScript.SetCursorSlot(cursorSlot);
            }
        }

        public void SetInActive()
        {
            foreach (GameObject slotObject in this.slotComponents)
            {
                slotObject.GetComponent<SlotUI>().SetInActive();
            }
        }
    }
}
