using UnityEngine;

namespace MURP.Inventory
{
    [CreateAssetMenu(menuName = "MURP/Inventory/EquipmentCategory")]
    public class EquipmentCategory : ScriptableObject
    {
        [SerializeField] EquipmentSlot _slot;
        [SerializeField] string _classification;

        public EquipmentSlot slot { get { return _slot; } }

        public string classification { get { return _classification; } }
    }
}