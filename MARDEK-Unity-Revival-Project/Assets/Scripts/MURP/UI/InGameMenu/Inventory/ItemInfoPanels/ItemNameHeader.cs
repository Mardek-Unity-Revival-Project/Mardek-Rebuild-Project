using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class ItemNameHeader : MonoBehaviour
    {
        [SerializeField] Text textField;

        void Update()
        {
            var slot = SlotUI.selectedSlot;
            if (slot != null && slot.item != null)
                textField.text = $"{slot.item.displayName} x{slot.amount}";
            else
                textField.text = string.Empty;
        }
    }
}