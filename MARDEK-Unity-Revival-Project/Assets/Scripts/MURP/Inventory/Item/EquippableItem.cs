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

        string CreateStatsString(string[] names)
        {
            string result = "";
            foreach (string importantStatName in names)
            {
                foreach (var candidateStat in this.statBoosts.intStats)
                {
                    if (candidateStat.statusEnum.name.Equals(importantStatName))
                    {
                        result += importantStatName + ": " + candidateStat.Value + "\n";
                    }
                }
                foreach (var candidateStat in this.statBoosts.floatStats)
                {
                    if (candidateStat.statusEnum.name.Equals(importantStatName))
                    {
                        result += importantStatName + ": " + candidateStat.Value + "\n";
                    }
                }
            }
            return result;
        }

        override protected string CreateFullDescription(string rawDescription)
        {
            return this.category.classification + "\n\n" + CreateStatsString(new string[]{"ATK", "DEF", "MDEF"}) + "\n" + rawDescription;
        }

        protected override string CreateProperties()
        {
            string result = "";
            result += CreateStatsString(new string[]{"ATK", "CRIT", "DEF", "MDEF"});
            if (!this.element.name.Equals("Normal")) result += this.element.name.ToUpper() + " Elemental\n";
            result += CreateStatsString(new string[]{"AGL", "SPR", "STR", "VIT"});
            result += CreateStatsString(new string[]{"MaxHP", "MaxMP"});
            result += CreateStatsString(new string[]{"FireResistance"}); // TODO Other resistances
            // TODO Automatic status effects
            return result;
        }
    }
}