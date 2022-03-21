using UnityEngine;
using System.Collections.Generic;

namespace MURP.Inventory
{
    /*
     * Represents an inventory. This is basically a list of `Slot`s.
     */
    [System.Serializable]
    public class Inventory
    {
        [SerializeField, FullSerializer.fsIgnore] bool isForPlayer;
        [SerializeField, FullSerializer.fsIgnore] int fullSize;

        [SerializeField] List<Slot> slots;

        // Note: Inventory doesn't extend MonoBehaviour, so this needs to be called by the parent object of the Inventory
        public void Start()
        {
            if (this.isForPlayer) {
                if (this.slots.Count < 6) throw new System.NotSupportedException("You must declare at least the 6 equipment slots");
                while (this.slots.Count < this.fullSize)
                {
                    this.slots.Add(new Slot(null, 0, new List<EquipmentCategory>(), true, true));
                }
                foreach (Slot slot in this.slots)
                {
                    slot.Validate();
                }
            }
        }

        public Slot GetSlot(int index)
        {
            if (index >= slots.Count)
                return null;
            return slots[index];
        }

        public List<Slot> GetAllNonEmptySlots()
        {
            List<Slot> result = new List<Slot>();
            foreach(Slot slot in slots)
            {
                if (!slot.IsEmpty())
                {
                    result.Add(slot);
                }
            }
            return result;
        }

        public int size { get { return fullSize; } }
    }
}