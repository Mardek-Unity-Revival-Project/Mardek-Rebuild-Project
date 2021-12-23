using UnityEngine;

namespace MURP.Inventory
{
    [CreateAssetMenu(menuName = "MURP/Inventory/EquipmentSlot")]
    public class EquipmentSlot : ScriptableObject
    {
        [SerializeField] bool _forbidEmpty;

        public bool forbidEmpty { get { return _forbidEmpty; } }
    }
}