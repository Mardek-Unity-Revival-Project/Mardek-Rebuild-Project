using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MURP.Audio;
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
        [SerializeField] AudioObject pickupSound;
        [SerializeField] AudioObject putSound;
        [SerializeField] AudioObject rejectSound;

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
                Item oldItem = this.ownSlot.item;
                int oldAmount = this.ownSlot.amount;
                this.ownSlot.InteractWithCursor(this.cursorSlot);

                if (oldItem != this.ownSlot.item || oldAmount != this.ownSlot.amount)
                {
                    this.UpdateSprite();
                    if (this.cursorSlot.IsEmpty()) AudioManager.PlaySoundEffect(this.putSound);
                    else AudioManager.PlaySoundEffect(this.pickupSound);
                    if (this.focusAction != null) this.focusAction.Invoke();
                }
                else if (!this.ownSlot.IsEmpty() || !this.cursorSlot.IsEmpty()) AudioManager.PlaySoundEffect(this.rejectSound);
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