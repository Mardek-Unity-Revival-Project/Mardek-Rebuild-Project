using UnityEngine;

namespace MURP.Inventory
{
    [CreateAssetMenu(menuName = "MURP/Inventory/Structure/Player")]
    public class PlayerInventoryStructure : InventoryStructure
    {
        [SerializeField] EquipmentCategory[] mainHandCategories;
        [SerializeField] EquipmentCategory[] offHandCategories;
        [SerializeField] EquipmentCategory[] headCategories;
        [SerializeField] EquipmentCategory[] bodyCategories;
        [SerializeField] EquipmentCategory[] accessoryCategories;

        private SlotBehaviour GetEquipmentSlot(EquipmentCategory[] categories)
        {
            if (categories.Length == 0) return new EmptySlotBehaviour();
            else return new EquipmentSlotBehaviour(categories);
        }

        public SlotBehaviour GetMainHandSlot()
        {
            return this.GetEquipmentSlot(this.mainHandCategories);
        }

        public SlotBehaviour GetOffHandSlot()
        {
            return this.GetEquipmentSlot(this.offHandCategories);
        }

        public SlotBehaviour GetHeadSlot()
        {
            return this.GetEquipmentSlot(this.headCategories);
        }

        public SlotBehaviour GetBodySlot()
        {
            return this.GetEquipmentSlot(this.bodyCategories);
        }

        public SlotBehaviour GetAccessorySlot()
        {
            return this.GetEquipmentSlot(this.accessoryCategories);
        }

        override public SlotBehaviour GetSlot(int index)
        {
            this.CheckBounds(index);
            if (index == 0) return this.GetMainHandSlot();
            if (index == 1) return this.GetOffHandSlot();
            if (index == 2) return this.GetHeadSlot();
            if (index == 3) return this.GetBodySlot();
            if (index == 4 || index == 5) return this.GetAccessorySlot();
            return new SimpleSlotBehaviour();
        }

        // 6 equipment slots plus 8x8 simple slots
        public override int size { get { return 6 + 8 * 8; } }
    }
}