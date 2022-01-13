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

        SubmenuLayoutController submenuController;
        SelectableLayout partyLayout;
        Slot slot;
        public static Slot selectedSlot { get; private set; }

        public void SetSlot(Slot newSlot)
        {
            slot = newSlot;
            UpdateSprite();
        }

        public void SetSubmenuController(SubmenuLayoutController submenuController, SelectableLayout partyLayout)
        {
            this.submenuController = submenuController;
            this.partyLayout = partyLayout;
        }

        public void UpdateSprite()
        {
            if (slot.IsEmpty())
            {
                itemImage.sprite = transparentSprite;
                amountText.text = "";
            } 
            else 
            {
                itemImage.sprite = slot.item.sprite;
                if (slot.amount == 1)
                {
                    amountText.text = "";
                }
                else
                {
                    amountText.text = slot.amount.ToString();
                }
            }
        }

        public void OnPointerClick(PointerEventData pointerEvent)
        {
            SlotCursor.InteractWithSlot(slot);
            UpdateSprite();
            if (submenuController != null && submenuController.IsFocussed())
            {
                submenuController.Unfocus();
                partyLayout.enabled = true;
            }
        }

        public void OnPointerEnter(PointerEventData pointerEvent)
        {
            selectedSlot = slot;
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