using UnityEngine;
using System.Collections.Generic;
using MURP.InventorySystem;
using MURP.Audio;

namespace MURP.UI
{
    public class SlotCursor : MonoBehaviour
    {
        public static SlotCursor instance { get; private set; }
        Slot slot = new Slot(null, 0, new System.Collections.Generic.List<EquipmentCategory>(), true, true);

        [SerializeField] AudioObject pickupSound;
        [SerializeField] AudioObject placeSound;
        [SerializeField] AudioObject rejectSound;

        [SerializeField] List<InputReader> inputReadersToHold = new List<InputReader>();
        List<bool> previousEnableValues = new List<bool>();

        private void OnEnable()
        {
            instance = this;
        }
        public bool IsEmpty()
        {
            return slot.IsEmpty();
        }
        public Item GetItem()
        {
            return slot.item;
        }
        public int GetAmount()
        {
            return slot.amount;
        }
        public static void InteractWithSlot(Slot slotInteracted)
        {
            instance.InteractWithSlotInternal(slotInteracted);
            instance.UpdateInputReadersHolding();
        }
        void InteractWithSlotInternal(Slot slotInteracted)
        { 
            if (slot.IsEmpty())
            {
                if (!slotInteracted.IsEmpty()) PickupItemFromSlot(slotInteracted);
            }
            else
            {
                if (slotInteracted.ApplyItemFilter(slot.currentItem) && slotInteracted.canPlayerPutItems)
                {
                    PlaceItemInSlot(slotInteracted);
                }
                else
                {
                    AudioManager.PlaySoundEffect(rejectSound);
                }
            }
        }
        void PlaceItemInSlot(Slot slotInteracted)
        {
            if (slotInteracted.IsEmpty())
            {
                slotInteracted.currentItem = slot.currentItem;
                slotInteracted.currentAmount = slot.currentAmount;
                slot.SetEmpty();
                AudioManager.PlaySoundEffect(placeSound);
            }
            else
            {
                if (slotInteracted.currentItem == slot.currentItem)
                {
                    if (slotInteracted.currentItem.CanStack())
                    {
                        slotInteracted.currentAmount += slot.currentAmount;
                        slot.SetEmpty();
                        AudioManager.PlaySoundEffect(placeSound);
                    }
                }
                else
                {
                    SwapSlotItems(slotInteracted);
                }
            }
        }
        void SwapSlotItems(Slot slotInteracted)
        {
            Item newCursorItem = slotInteracted.currentItem;
            int newCursorAmount = slotInteracted.currentAmount;
            slotInteracted.currentItem = slot.currentItem;
            slotInteracted.currentAmount = slot.currentAmount;
            slot.currentItem = newCursorItem;
            slot.currentAmount = newCursorAmount;
            AudioManager.PlaySoundEffect(pickupSound);
        }
        void PickupItemFromSlot(Slot slotInteracted)
        {
            if (slotInteracted.canBeEmpty)
            {
                slot.currentItem = slotInteracted.currentItem;
                slot.currentAmount = slotInteracted.currentAmount;
                slotInteracted.SetEmpty();
                AudioManager.PlaySoundEffect(pickupSound);
            }
            else
            {
                if (slotInteracted.currentAmount > 1)
                {
                    slot.currentItem = slotInteracted.currentItem;
                    slot.currentAmount = slotInteracted.currentAmount - 1;
                    slotInteracted.currentAmount = 1;
                    AudioManager.PlaySoundEffect(pickupSound);
                }
                else
                {
                    AudioManager.PlaySoundEffect(rejectSound);
                }
            }
        }
    
        void UpdateInputReadersHolding()
        {
            if (instance.slot.IsEmpty())
            {
                for(int i = 0; i < previousEnableValues.Count; i++)
                {
                    inputReadersToHold[i].enabled = previousEnableValues[i];
                }
            }
            else
            {
                previousEnableValues = new List<bool>();
                for (int i = 0; i < inputReadersToHold.Count; i++)
                {
                    previousEnableValues.Add(inputReadersToHold[i].enabled);
                    inputReadersToHold[i].enabled = false;
                }
            }
        }
    }
}