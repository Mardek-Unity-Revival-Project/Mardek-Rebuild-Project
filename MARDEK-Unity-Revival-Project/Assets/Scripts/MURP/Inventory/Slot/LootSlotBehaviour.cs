namespace MURP.Inventory
{
    public class LootSlotBehaviour : SlotBehaviour
    {
        public override void SetItem(int index, InventoryContent inventory, ItemStack newItem)
        {
            if (newItem != null) throw new System.ArgumentException("Can't put items back in loot slots");
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
            if (inputItem == null)
            {
                ItemStack result = inventory.GetItem(index);
                inventory.SetItem(null, index);
                return result;
            }
            else return inputItem;
        }
    }
}