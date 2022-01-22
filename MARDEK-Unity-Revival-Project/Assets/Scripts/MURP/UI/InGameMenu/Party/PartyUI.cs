using MURP.CharacterSystem;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.UI
{
    public class PartyUI : MonoBehaviour
    {
        [SerializeField] Party party = null;

        [SerializeField] List<ConditionEntry> conditionEntries;

        void OnEnable()
        {
            for (int index = 0; index < this.conditionEntries.Count; index++)
            {
                var entry = this.conditionEntries[index];
                if (index < this.party.Characters.Count)
                {
                    entry.gameObject.SetActive(true);
                    entry.SetCharacter(this.party.Characters[index]);
                }
                else entry.gameObject.SetActive(false);
            }

            // TODO Update the other entries
        }
    }
}
