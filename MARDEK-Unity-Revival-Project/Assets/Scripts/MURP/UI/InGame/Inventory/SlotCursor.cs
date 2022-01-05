using UnityEngine;
using MURP.Inventory;
using MURP.Audio;

namespace MURP.UI
{
    public class SlotCursor : MonoBehaviour
    {
        public static SlotCursor instance { get; private set; }

        [SerializeField] AudioObject pickupSound;
        [SerializeField] AudioObject putSound;
        [SerializeField] AudioObject rejectSound;

        Slot slot = new Slot(null, 0, new System.Collections.Generic.List<EquipmentCategory>(), true, true);
        
        public static Slot CursorSlot
        {
            get
            { 
                return instance.slot;
            } 
            set
            {
                instance.slot = value;
            }
        }

        public static void InteractWithSlot(Slot slotInteracted)
        { 
            if (CursorSlot.IsEmpty())
            {
                if (slotInteracted.canBeEmpty)
                {
                    CursorSlot.currentItem = slotInteracted.currentItem;
                    CursorSlot.currentAmount = slotInteracted.currentAmount;
                    slotInteracted.SetEmpty();
                }
                else
                {
                    if (slotInteracted.currentAmount > 1)
                    {
                        CursorSlot.currentItem = slotInteracted.currentItem;
                        CursorSlot.currentAmount = slotInteracted.currentAmount - 1;
                        slotInteracted.currentAmount = 1;
                    }
                }
            }
            else
            {
                if (slotInteracted.ApplyItemFilter(CursorSlot.currentItem) && slotInteracted.canPlayerPutItems)
                {
                    if (slotInteracted.IsEmpty())
                    {
                        slotInteracted.currentItem = CursorSlot.currentItem;
                        slotInteracted.currentAmount = CursorSlot.currentAmount;
                        CursorSlot.SetEmpty();
                    }
                    else
                    {
                        if (slotInteracted.currentItem == CursorSlot.currentItem)
                        {
                            if (slotInteracted.currentItem.CanStack())
                            {
                                slotInteracted.currentAmount += CursorSlot.currentAmount;
                                CursorSlot.SetEmpty();
                            }
                        }
                        else
                        {
                            Item newCursorItem = slotInteracted.currentItem;
                            int newCursorAmount = slotInteracted.currentAmount;
                            slotInteracted.currentItem = CursorSlot.currentItem;
                            slotInteracted.currentAmount = CursorSlot.currentAmount;
                            CursorSlot.currentItem = newCursorItem;
                            CursorSlot.currentAmount = newCursorAmount;
                        }
                    }
                }
            }

            instance.UpdateCursorTexture();
        }

        private void OnEnable()
        {
            instance = this;
        }

        void UpdateCursorTexture()
        {
            Texture2D newTexture;
            if (CursorSlot.IsEmpty()) newTexture = null;
            else newTexture = CursorSlot.item.readableSpriteTexture;

            Cursor.SetCursor(newTexture, new Vector2(0f, 0f), CursorMode.Auto);
        }
    }
}