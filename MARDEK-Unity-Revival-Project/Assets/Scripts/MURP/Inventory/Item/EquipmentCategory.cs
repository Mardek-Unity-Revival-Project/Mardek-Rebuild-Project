using UnityEngine;

namespace MURP.Inventory
{
    [CreateAssetMenu(menuName = "MURP/Inventory/EquipmentCategory")]
    public class EquipmentCategory : ScriptableObject
    {
        [SerializeField] EquipmentSlot _slot;

        public EquipmentSlot slot { get { return _slot; } }
    }
}