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

        SelectedItemInfo selectedItemInfo;
        Slot ownSlot;

        public void SetSelectedItemInfo(SelectedItemInfo _selectedItemInfo)
        {
            selectedItemInfo = _selectedItemInfo;
        }

        public void SetSlot(Slot newSlot)
        {
            ownSlot = newSlot;
            UpdateSprite();
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
            SlotCursor.InteractWithSlot(ownSlot);
            UpdateSprite();
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