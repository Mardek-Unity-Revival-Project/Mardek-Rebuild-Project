using UnityEngine;

namespace MURP.Inventory
{
    public abstract class SlotBehaviour : MonoBehaviour
    {
        /*
         * Gets a copy of the `ItemStack` in the slot (or null if the slot is empty).
         */
        public ItemStack GetItem(int index, InventoryContent inventory)
        {
            return inventory.GetItem(index);
        }

        /*
         * Replaces the current item in the slot with `newItem`. This method should not be used to handle inventory operations for
         * the player; that's what `AttemptPutItem`is for. This method is intended for hardcoded inventory transfer actions.
         *
         * If the slot can't hold `newItem` (for instance when setting the item in a weapon slot to a helmet), this method will throw 
         * an exception.
         */
        public abstract void SetItem(int index, InventoryContent inventory, ItemStack newItem);

        /*
         * Takes `amount` items from the `ItemStack` in the slot and returns them in a new `ItemStack`. This method is designed
         * for shopping actions (where the player has already paid and needs to take the given `amount` of items from the shop).
         *
         * If there are not enough items in the slot, this method will throw an exception.
         */
        public abstract ItemStack TakeAmount(int index, InventoryContent inventory, int amount);

        /*
         * Attempts to put `inputItem` in the slot. If `inputItem` can't be stored in the slot (for whatever reason), it will be
         * returned. If only a part of the `inputItem` stack can be put in the slot, the remaining `inputItem` stack will be
         * returned. If `inputItem` is successfully put in the slot, the original item in the slot will be returned (possibly null).
         *
         * This method should be called when the player clicks on a slot while managing his inventory, where `inputItem` is the
         * original item on the cursor and the return value becomes the new item on the cursor.
         */
        public abstract ItemStack AttemptPutItem(int index, InventoryContent inventory, ItemStack inputItem);
    }
}