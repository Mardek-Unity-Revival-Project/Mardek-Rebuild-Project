using UnityEngine;
using System.Collections.Generic;

namespace MURP.InventorySystem
{
    [System.Serializable]
    public class Inventory
    {
        [SerializeField] List<Slot> slots;
        public List<Slot> Slots { get { return slots; } }
    }
}