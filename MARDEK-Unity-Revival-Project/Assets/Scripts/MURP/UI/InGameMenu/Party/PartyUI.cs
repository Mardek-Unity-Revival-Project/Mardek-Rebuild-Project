using MURP.CharacterSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MURP.UI
{
    public class PartyUI : MonoBehaviour
    {
        [SerializeField] Party party = null;

        [SerializeField] List<ConditionEntry> conditionEntries;
        [SerializeField] List<VitalStatisticsEntry> vitalStatisticsEntries;
        [SerializeField] List<ResistancesEntry> elementalResistancesEntries;
        [SerializeField] List<ResistancesEntry> statusEffectResistancesEntries;
        [SerializeField] List<GrowthEntry> growthEntries;
        [SerializeField] List<Performance1Entry> performance1Entries;
        [SerializeField] List<Performance2Entry> performance2Entries;

        void OnEnable()
        {
            List<PartyEntry>[] entriesList = new List<PartyEntry>[]{
                this.conditionEntries.Cast<PartyEntry>().ToList(),
                this.vitalStatisticsEntries.Cast<PartyEntry>().ToList(),
                this.elementalResistancesEntries.Cast<PartyEntry>().ToList(),
                this.statusEffectResistancesEntries.Cast<PartyEntry>().ToList(),
                this.growthEntries.Cast<PartyEntry>().ToList(),
                this.performance1Entries.Cast<PartyEntry>().ToList(),
                this.performance2Entries.Cast<PartyEntry>().ToList()
            };

            foreach (var entries in entriesList) {
                for (int index = 0; index < entries.Count; index++)
                {
                    var entry = entries[index];
                    if (index < this.party.Characters.Count)
                    {
                        entry.gameObject.SetActive(true);
                        entry.SetCharacter(this.party.Characters[index]);
                    }
                    else entry.gameObject.SetActive(false);
                }
            }  
        }
    }
}
