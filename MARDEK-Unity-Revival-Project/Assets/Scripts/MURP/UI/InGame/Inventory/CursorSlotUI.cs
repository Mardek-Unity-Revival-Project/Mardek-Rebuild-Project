using UnityEngine;
using MURP.Inventory;

namespace MURP.UI
{
    public class CursorSlotUI
    {
        Slot cursorSlot;

        Item lastItem = null;
        int lastAmount = 0;

        public CursorSlotUI(Slot cursorSlot)
        {
            this.cursorSlot = cursorSlot;
            lastItem = null;
            lastAmount = -1; // -1 is an impossible amount value, so this will force a cursor update in the next Update call
        }

        public void Update()
        {
            if (cursorSlot != null)
            {
                if (cursorSlot.item != lastItem || cursorSlot.amount != lastAmount)
                {
                    Texture2D newTexture;
                    if (cursorSlot.IsEmpty()) newTexture = null;
                    else newTexture = cursorSlot.item.readableSpriteTexture;

                    Cursor.SetCursor(newTexture, new Vector2(0f, 0f), CursorMode.Auto);
                    lastItem = cursorSlot.item;
                    lastAmount = cursorSlot.amount;
                }
            }
        }
    }
}