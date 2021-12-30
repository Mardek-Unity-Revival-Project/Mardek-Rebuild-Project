using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MURP.Inventory;

namespace MURP.UI
{
    public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image backgroundImage;
        [SerializeField] Image itemImage;
        [SerializeField] Text amountText;
        [SerializeField] Sprite baseSlotSprite;
        [SerializeField] Sprite hoverSlotSprite;
        [SerializeField] Sprite transparentSprite;

        SelectedItemInfo selectedItemInfo;
        System.Action focusAction;
        Slot ownSlot;
        Slot cursorSlot;

        public void SetFocusAction(System.Action focusAction)
        {
            this.focusAction = focusAction;
        }

        public void SetSelectedItemInfo(SelectedItemInfo selectedItemInfo)
        {
            this.selectedItemInfo = selectedItemInfo;
        }

        public void SetSlot(Slot newSlot)
        {
            this.ownSlot = newSlot;
            this.UpdateSprite();
        }

        public void SetCursorSlot(Slot cursorSlot)
        {
            this.cursorSlot = cursorSlot;
        }

        public void UpdateSprite()
        {
            if (this.ownSlot.IsEmpty())
            {
                this.itemImage.sprite = transparentSprite;
                this.amountText.text = "";
            } 
            else 
            {
                this.itemImage.sprite = this.ownSlot.item.sprite;
                if (this.ownSlot.amount == 1)
                {
                    this.amountText.text = "";
                }
                else
                {
                    this.amountText.text = this.ownSlot.amount.ToString();
                }
            }
        }

        public void OnPointerClick(PointerEventData pointerEvent)
        {
            if (this.cursorSlot != null)
            {
                this.ownSlot.InteractWithCursor(this.cursorSlot);
                this.UpdateSprite();
                if (this.focusAction != null) this.focusAction.Invoke();
            }
        }

        public void OnPointerEnter(PointerEventData pointerEvent)
        {
            if (this.selectedItemInfo != null) this.selectedItemInfo.SetCurrentSlot(this.ownSlot);
            this.backgroundImage.sprite = this.hoverSlotSprite;
        }

        public void OnPointerExit(PointerEventData pointerEvent)
        {
            this.backgroundImage.sprite = this.baseSlotSprite;
        }
    }
}