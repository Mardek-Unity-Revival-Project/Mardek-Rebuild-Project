using UnityEngine;

namespace MURP.Inventory
{
    /*
     * Represents the *structure* of an inventory. This defines the size of an inventory and the *behaviour* of each slot (for
     * instance which slots are weapon slots, helmet slots, and regular slots).
     */
    public abstract class InventoryStructure : ScriptableObject
    {
        public abstract SlotBehaviour GetSlot(int index);

        protected void CheckBounds(int index)
        {
            if (index < 0) throw new System.ArgumentException("Index (" + index + ") can't be negative");
            if (index >= this.size) throw new System.ArgumentException("Index (" + index + ") must be smaller than " + this.size);
        }

        public abstract int size { get; }
    }
}