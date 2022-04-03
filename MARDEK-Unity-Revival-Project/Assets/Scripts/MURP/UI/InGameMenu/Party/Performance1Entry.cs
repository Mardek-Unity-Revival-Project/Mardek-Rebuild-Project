using MURP.CharacterSystem;
using MURP.StatsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class Performance1Entry : MonoBehaviour, PartyEntry
    {
        [SerializeField] Text battleCount;
        [SerializeField] Text killCount;
        [SerializeField] Text koCount;
        [SerializeField] Text meleeCount;
        [SerializeField] Text magicCount;
        [SerializeField] Text itemCount;

        [SerializeField] IntegerStat battleCountStat;
        [SerializeField] IntegerStat killCountStat;
        [SerializeField] IntegerStat koCountStat;
        [SerializeField] IntegerStat meleeCountStat;
        [SerializeField] IntegerStat magicCountStat;
        [SerializeField] IntegerStat itemCountStat;

        public void SetCharacter(Character character)
        {
            this.UpdateStat(character, this.battleCountStat, this.battleCount);
            this.UpdateStat(character, this.killCountStat, this.killCount);
            this.UpdateStat(character, this.koCountStat, this.koCount);
            this.UpdateStat(character, this.meleeCountStat, this.meleeCount);
            this.UpdateStat(character, this.magicCountStat, this.magicCount);
            this.UpdateStat(character, this.itemCountStat, this.itemCount);
        }

        private void UpdateStat(Character character, IntegerStat stat, Text text)
        {
            if (stat != null) text.text = character.GetStat(stat).Value.ToString();
        }
    }
}
