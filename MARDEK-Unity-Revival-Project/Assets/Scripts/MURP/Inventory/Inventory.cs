using UnityEngine;
using System.Collections.Generic;

namespace MURP.Inventory
{
    /*
     * Represents an inventory. This is a pair of an *immutable* `InventoryStructure` and a *mutable* `InventoryContent`.
     */
    public class Inventory : MonoBehaviour
    {
        readonly InventoryStructure structure;
        readonly InventoryContent content;

        public Inventory(InventoryStructure structure, InventoryContent content)
        {
            if (this.structure.size != this.content.size)
            {
                throw new System.ArgumentException("Structure size (" + this.structure.size + ") must match content size (" + this.content.size + ")");
            }
            this.structure = structure;
            this.content = content;
        }

        public Slot GetSlot(int index)
        {
            return new Slot(this.structure.GetSlot(index), this.content, index);
        }

        public List<ItemStack> GetAllItems()
        {
            return this.content.GetAllItems();
        }
    }
}