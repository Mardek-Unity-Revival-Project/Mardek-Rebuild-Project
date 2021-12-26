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
            this.lastItem = null;
            this.lastAmount = -1; // -1 is an impossible amount value, so this will force a cursor update in the next Update call
        }

        public void Update()
        {
            if (this.cursorSlot != null)
            {
                if (this.cursorSlot.item != this.lastItem || this.cursorSlot.amount != this.lastAmount)
                {
                    Texture2D newTexture;
                    if (this.cursorSlot.IsEmpty()) newTexture = null;
                    else newTexture = this.cursorSlot.item.readableSpriteTexture;

                    Cursor.SetCursor(newTexture, new Vector2(0f, 0f), CursorMode.Auto);
                    this.lastItem = this.cursorSlot.item;
                    this.lastAmount = this.cursorSlot.amount;
                }
            }
        }
    }
}