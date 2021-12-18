namespace MURP.Inventory
{
    public class LootInventoryStructure : InventoryStructure
    {

        readonly int _size;

        public LootInventoryStructure(int size)
        {
            this._size = size;
        }

        override public SlotBehaviour GetSlot(int index)
        {
            this.CheckBounds(index);
            return new LootSlotBehaviour();
        }

        override public int size { get { return _size; } }
    }
}