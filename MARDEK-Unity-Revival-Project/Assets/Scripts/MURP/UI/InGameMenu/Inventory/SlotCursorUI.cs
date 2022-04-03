using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MURP.UI
{
    public class SlotCursorUI : MonoBehaviour
    {
        [SerializeField] Image itemImage;
        [SerializeField] Text amountText;
        [SerializeField] Sprite transparentSprite;

        void Update()
        {
            this.transform.position = new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue());
            if (SlotCursor.instance.IsEmpty())
            {
                this.itemImage.sprite = transparentSprite;
                this.amountText.text = "";
            }
            else
            {
                this.itemImage.sprite = SlotCursor.instance.GetItem().sprite;
                this.amountText.text = SlotCursor.instance.GetAmount().ToString();
            }
        }
    }
}