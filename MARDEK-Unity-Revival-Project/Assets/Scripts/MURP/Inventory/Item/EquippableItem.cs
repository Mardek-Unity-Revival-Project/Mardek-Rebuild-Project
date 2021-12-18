using UnityEngine;
using MURP.StatsSystem;

namespace MURP.Inventory
{
    [CreateAssetMenu(menuName = "MURP/Inventory/EquippableItem")]
    public class EquippableItem : Item
    {
        [SerializeField] EquipmentCategory _category;
        [SerializeField] StatsSet _statsSet;

        // TODO Skills
        // TODO Status effects

        public EquipmentCategory category { get { return _category; } }

        public StatsSet statBoosts { get { return _statsSet; } }

        public override bool CanStack()
        {
            return false;
        }
    }
}