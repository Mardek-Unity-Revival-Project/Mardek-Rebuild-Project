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

        public SlotGrid(List<GameObject> _slotComponents, Inventory.Inventory _inventory, int _firstInventorySlotIndex, int _numSlots)
        {
            slotComponents = _slotComponents;
            inventory = _inventory;
            firstInventorySlotIndex = _firstInventorySlotIndex;
            numSlots = _numSlots;
        }

        public void UpdateSlots(Slot cursorSlot, SelectedItemInfo selectedItemInfo, System.Action focusAction)
        {
            for (int uiSlotIndex = 0; uiSlotIndex < numSlots; uiSlotIndex++)
            {
                int inventorySlotIndex = firstInventorySlotIndex + uiSlotIndex;
                Slot inventorySlot = inventory.GetSlot(inventorySlotIndex);
                GameObject uiSlot = slotComponents[uiSlotIndex];

                SlotUI slotScript = uiSlot.GetComponent<SlotUI>();
                slotScript.SetSelectedItemInfo(selectedItemInfo);
                //slotScript.SetFocusAction(focusAction);
                slotScript.SetSlot(inventorySlot);
                //slotScript.SetCursorSlot(cursorSlot);
            }
        }

        public void SetInActive()
        {
            foreach (GameObject slotObject in slotComponents)
            {
                slotObject.GetComponent<SlotUI>().SetInActive();
            }
        }
    }
}
