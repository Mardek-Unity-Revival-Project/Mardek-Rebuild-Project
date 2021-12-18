using UnityEngine;
using System.Collections.Generic;

namespace MURP.Inventory
{
    /*
     * Represents the *content* of an inventory. This is basically a list of `ItemStack`s.
     */
    public class InventoryContent : MonoBehaviour
    {
        [SerializeField] ItemStack[] items;

        public void SetItem(ItemStack newItem, int index)
        {
            this.items[index] = newItem;
        }

        public ItemStack GetItem(int index)
        {
            return this.items[index];
        }

        /*
         * Get all `ItemStack`s in this `InventoryContent`, excluding null/empty `ItemStack`s.
         */
        public List<ItemStack> GetAllItems()
        {
            List<ItemStack> result = new List<ItemStack>();
            foreach(ItemStack item in this.items)
            {
                if (item != null)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public int size { get { return this.items.Length; } }
    }
}