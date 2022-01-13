using UnityEngine;
using MURP.Inventory;
using MURP.Audio;

namespace MURP.UI
{
    public class SlotCursor : MonoBehaviour
    {
        public static SlotCursor instance { get; private set; }

        [SerializeField] AudioObject pickupSound;
        [SerializeField] AudioObject placeSound;
        [SerializeField] AudioObject rejectSound;

        Slot slot = new Slot(null, 0, new System.Collections.Generic.List<EquipmentCategory>(), true, true);

        private void OnEnable()
        {
            instance = this;
        }

        public static void InteractWithSlot(Slot slotInteracted)
        {
            instance.InteractWithSlotInternal(slotInteracted);
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
            UpdateCursorTexture();
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

        void UpdateCursorTexture()
        {
            Texture2D newTexture;
            if (slot.IsEmpty()) newTexture = null;
            else newTexture = slot.item.readableSpriteTexture;

            Cursor.SetCursor(newTexture, new Vector2(0f, 0f), CursorMode.Auto);
        }

        public bool IsEmpty()
        {
            return slot.IsEmpty();
        }
    }
}