using UnityEngine;
using MURP.StatsSystem;

namespace MURP.Inventory
{
    [CreateAssetMenu(menuName = "MURP/Inventory/ExpendableItem")]
    public class ExpendableItem : Item
    {
        [SerializeField] int _percentHealthRestore;
        [SerializeField] int _percentManaRestore;
        [SerializeField] bool _canResurrect;
        [SerializeField] StatsSet _statsSet;

        // TODO Giving and curing status effects

        public int percentHealthRestore { get { return _percentHealthRestore; } }
        public int percentManaRestore { get { return _percentManaRestore; } }

        public bool canResurrect { get { return _canResurrect; } }

        public StatsSet statsSet { get { return _statsSet; } }

        override protected string CreateFullDescription(string rawDescription)
        {
            return "EXPENDABLE ITEM\n\n\n" + rawDescription;
        }
    }
}