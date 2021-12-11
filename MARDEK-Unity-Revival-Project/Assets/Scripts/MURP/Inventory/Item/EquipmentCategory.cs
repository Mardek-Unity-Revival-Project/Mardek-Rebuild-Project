using UnityEngine;

namespace MURP.Inventory
{
    public class EquipmentCategory
    {
        public static readonly EquipmentCategory SWORD = new EquipmentCategory(EquipmentSlot.MainHand);
        public static readonly EquipmentCategory GREAT_SWORD = new EquipmentCategory(EquipmentSlot.MainHand);
        public static readonly EquipmentCategory DOUBLE_SWORD = new EquipmentCategory(EquipmentSlot.MainHand);
        public static readonly EquipmentCategory GREAT_AXE = new EquipmentCategory(EquipmentSlot.MainHand);
        public static readonly EquipmentCategory SPEAR = new EquipmentCategory(EquipmentSlot.MainHand);
        public static readonly EquipmentCategory STAFF = new EquipmentCategory(EquipmentSlot.MainHand);
        public static readonly EquipmentCategory WAND = new EquipmentCategory(EquipmentSlot.MainHand);
        public static readonly EquipmentCategory CLAW = new EquipmentCategory(EquipmentSlot.MainHand);
        public static readonly EquipmentCategory ROBOT_ARM = new EquipmentCategory(EquipmentSlot.MainHand);
        public static readonly EquipmentCategory INVENTION = new EquipmentCategory(EquipmentSlot.MainHand);
        public static readonly EquipmentCategory HARP = new EquipmentCategory(EquipmentSlot.MainHand);

        public static readonly EquipmentCategory SHIELD = new EquipmentCategory(EquipmentSlot.OffHand);
        public static readonly EquipmentCategory SHEET_MUSIC = new EquipmentCategory(EquipmentSlot.OffHand);


        public static readonly EquipmentCategory FULL_HELMET = new EquipmentCategory(EquipmentSlot.Head);
        public static readonly EquipmentCategory HALF_HELMET = new EquipmentCategory(EquipmentSlot.Head);
        public static readonly EquipmentCategory HAT = new EquipmentCategory(EquipmentSlot.Head);

        public static readonly EquipmentCategory HEAVY_ARMOUR = new EquipmentCategory(EquipmentSlot.Body);
        public static readonly EquipmentCategory MEDIUM_ARMOUR = new EquipmentCategory(EquipmentSlot.Body);
        public static readonly EquipmentCategory LIGHT_ARMOUR = new EquipmentCategory(EquipmentSlot.Body);
        public static readonly EquipmentCategory STOLE = new EquipmentCategory(EquipmentSlot.Body);
        public static readonly EquipmentCategory ROBE = new EquipmentCategory(EquipmentSlot.Body);
        public static readonly EquipmentCategory CLOTHING = new EquipmentCategory(EquipmentSlot.Body);

        public static readonly EquipmentCategory ACCESSORY = new EquipmentCategory(EquipmentSlot.Accessory);
        public static readonly EquipmentCategory GEMSTONE = new EquipmentCategory(EquipmentSlot.Accessory);
        
        public readonly EquipmentSlot slot;

        public EquipmentCategory(EquipmentSlot slot)
        {
            this.slot = slot;
        }
    }
}