using MURP.CharacterSystem;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;

namespace MURP.UI
{
    public class PartyUI : MonoBehaviour
    {
        [SerializeField] Party party = null;

        [SerializeField] List<ConditionEntry> conditionEntries;
        [SerializeField] List<VitalStatisticsEntry> vitalStatisticsEntries;

        void OnEnable()
        {
            List<PartyEntry>[] entriesList = new List<PartyEntry>[]{
                this.conditionEntries.Cast<PartyEntry>().ToList(),
                this.vitalStatisticsEntries.Cast<PartyEntry>().ToList()
                // TODO Update the other entries
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
