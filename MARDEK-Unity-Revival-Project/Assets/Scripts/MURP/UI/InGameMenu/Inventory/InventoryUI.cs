using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.InventorySystem;

namespace MURP.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] protected List<SlotUI> slots = new List<SlotUI>();

        protected void AssignInventoryToUI(Inventory inventory)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (i < inventory.Slots.Count)
                {
                    slots[i].SetSlot(inventory.Slots[i]);
                }
                else
                {
                    Debug.LogWarning("UI has more item slots than assigned inventory");
                    break;
                }
            }
        }
    }
}