using UnityEngine;

namespace MURP.Inventory
{
    [CreateAssetMenu(menuName = "MURP/Inventory/EquippableItem")]
    public class EquippableItem : Item
    {
        [SerializeField] int _attackDamage;
        [SerializeField] int _criticalChance;
        [SerializeField] int _physicalDefense;
        [SerializeField] int _magicDefense;
        [SerializeField] EquipmentCategory _category;

        // TODO Skills
        // TODO Status effects

        public int attackDamage { get { return _attackDamage; } }

        public int criticalChance { get { return _criticalChance; } }

        public int physicalDefense { get { return _physicalDefense; } }

        public int magicDefense { get { return _magicDefense; } }

        public EquipmentCategory category { get { return _category; } }
    }
}