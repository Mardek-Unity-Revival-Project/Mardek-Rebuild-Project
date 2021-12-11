using UnityEngine;

namespace MURP.Inventory
{
    [CreateAssetMenu(menuName = "MURP/Inventory/ExpendableItem")]
    public class ExpendableItem : Item
    {
        [SerializeField] int plainHealthRestore;
        [SerializeField] int percentHealthRestore;
        [SerializeField] int plainManaRestore;
        [SerializeField] int percentManaRestore;
        [SerializeField] bool canResurrect;

        // TODO Giving and curing status effects
    }
}