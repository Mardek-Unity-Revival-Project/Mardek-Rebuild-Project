using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.Inventory;
using UnityEngine.UI;

namespace MURP.UI
{
    public class ItemSkillsPanel : MonoBehaviour
    {
        [SerializeField] Text textField;

        void Update()
        {
            var slot = SlotUI.selectedSlot;
            if (slot != null && slot.item != null)
                textField.text = "Skills: WIP";
            else
                textField.text = string.Empty;
        }
    }
}
