using UnityEngine;

namespace MURP.Inventory
{
    /*
     * The `SlotBehaviour` for equipment slots. This behaviour only allows the player to insert `EquippableItem` whose `category`
     * allows the `slotType` of this behaviour. Furthermore, if the `slotType` of this behaviour can't be empty (for instance
     * weapon slots), the player can only take the item from this slot by trading it for another item of with the right `category`.
     */
    public class EquipmentSlotBehaviour : SlotBehaviour
    {
        [SerializeField] EquipmentCategory[] categories;

        public EquipmentSlotBehaviour(EquipmentCategory[] categories)
        {
            this.categories = categories;
        }

        private EquipmentSlot GetSlot()
        {
            EquipmentSlot slot = null;
            foreach (EquipmentCategory category in categories) {
                if (slot == null)
                {
                    slot = category.slot;
                }
                else if (slot != category.slot)
                {
                    throw new System.ApplicationException("Not all categories have the same slot");
                }
            }

            return slot;
        }

        public override void SetItem(int index, InventoryContent inventory, ItemStack newItem)
        {
            if (newItem == null && this.GetSlot().forbidEmpty)
            {
                throw new System.ArgumentException("This slot must not be empty");
            }
            if (newItem != null && !(newItem.item is EquippableItem && System.Array.IndexOf(this.categories, (newItem.item as EquippableItem).category) != -1))
            {
                throw new System.ArgumentException("Only equippable " + this.categories + " items are allowed");
            }
            inventory.SetItem(newItem, index);
        }

        public override ItemStack TakeAmount(int index, InventoryContent inventory, int amount)
        {
            throw new System.InvalidOperationException("TakeAmount() is not meant for Equipment slots");
        }

        override public ItemStack AttemptPutItem(int index, InventoryContent inventory, ItemStack inputItem)
        {
            bool canTrade = false;
            if (inputItem == null) {
                canTrade = !this.GetSlot().forbidEmpty;
            } else if (inputItem.item is EquippableItem) {
                EquippableItem replacement = inputItem.item as EquippableItem;
                canTrade = System.Array.IndexOf(this.categories, replacement.category) != -1;
            }

            if (canTrade) {
                ItemStack result = inventory.GetItem(index);
                inventory.SetItem(inputItem, index);
                return result;
            } else {
                return inputItem;
            }
        }
    }
}