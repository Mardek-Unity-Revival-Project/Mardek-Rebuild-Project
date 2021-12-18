namespace MURP.Inventory
{
    /*
     * A slot that is always empty.
     */
    public class EmptySlotBehaviour : SlotBehaviour
    {
        override public void SetItem(int index, InventoryContent inventory, ItemStack newItem)
        {
            if (newItem != null)
            {
                throw new System.ArgumentException("Empty slots can only have null items");
            }
        }

        override public ItemStack TakeAmount(int index, InventoryContent inventory, int amount)
        {
            throw new System.InvalidOperationException("No item can be taken from an empty slot");
        }

        override public ItemStack AttemptPutItem(int index, InventoryContent inventory, ItemStack inputItem)
        {
            return inputItem;
        }
    }
}