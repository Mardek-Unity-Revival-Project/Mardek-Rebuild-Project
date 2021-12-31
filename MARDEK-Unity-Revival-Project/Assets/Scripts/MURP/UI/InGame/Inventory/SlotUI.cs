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
            focusAction = focusAction;
        }

        public void SetSelectedItemInfo(SelectedItemInfo selectedItemInfo)
        {
            selectedItemInfo = selectedItemInfo;
        }

        public void SetSlot(Slot newSlot)
        {
            ownSlot = newSlot;
            UpdateSprite();
        }

        public void SetCursorSlot(Slot cursorSlot)
        {
            cursorSlot = cursorSlot;
        }

        public void UpdateSprite()
        {
            if (ownSlot.IsEmpty())
            {
                itemImage.sprite = transparentSprite;
                amountText.text = "";
            } 
            else 
            {
                itemImage.sprite = ownSlot.item.sprite;
                if (ownSlot.amount == 1)
                {
                    amountText.text = "";
                }
                else
                {
                    amountText.text = ownSlot.amount.ToString();
                }
            }
        }

        public void OnPointerClick(PointerEventData pointerEvent)
        {
            if (cursorSlot != null)
            {
                Item oldItem = ownSlot.item;
                int oldAmount = ownSlot.amount;
                ownSlot.InteractWithCursor(cursorSlot);

                if (oldItem != ownSlot.item || oldAmount != ownSlot.amount)
                {
                    UpdateSprite();
                    if (cursorSlot.IsEmpty()) AudioManager.PlaySoundEffect(putSound);
                    else AudioManager.PlaySoundEffect(pickupSound);
                    if (focusAction != null) focusAction.Invoke();
                }
                else if (!ownSlot.IsEmpty() || !cursorSlot.IsEmpty()) AudioManager.PlaySoundEffect(rejectSound);
            }
        }

        public void OnPointerEnter(PointerEventData pointerEvent)
        {
            if (selectedItemInfo != null) selectedItemInfo.SetCurrentSlot(ownSlot);
            backgroundImage.sprite = hoverSlotSprite;
        }

        public void OnPointerExit(PointerEventData pointerEvent)
        {
            backgroundImage.sprite = baseSlotSprite;
        }

        public void SetInActive()
        {
            backgroundImage.sprite = baseSlotSprite;
        }
    }
}