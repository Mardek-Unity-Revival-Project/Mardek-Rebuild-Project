namespace MURP.Inventory
{
    /*
     * The `SlotBehaviour` that allows the user to freely insert and take any item.
     */
    public class SimpleSlotBehaviour : SlotBehaviour
    {
        public override void SetItem(int index, InventoryContent inventory, ItemStack newItem)
        {
            inventory.SetItem(newItem, index);
        }

        public override ItemStack TakeAmount(int index, InventoryContent inventory, int amount)
        {
            ItemStack currentItem = inventory.GetItem(index);
            if (currentItem == null || currentItem.amount < amount)
            {
                throw new System.ArgumentException("The ItemStack in the slot is too small");
            }

            ItemStack takenItem = new ItemStack(currentItem.item, amount);
            ItemStack remainingItem = new ItemStack(currentItem.item, currentItem.amount - amount);
            inventory.SetItem(remainingItem, index);
            return takenItem;
        }

        override public ItemStack AttemptPutItem(int index, InventoryContent inventory, ItemStack inputItem)
        {
            ItemStack result = inventory.GetItem(index);
            inventory.SetItem(inputItem, index);
            return result;
        }
    }
}