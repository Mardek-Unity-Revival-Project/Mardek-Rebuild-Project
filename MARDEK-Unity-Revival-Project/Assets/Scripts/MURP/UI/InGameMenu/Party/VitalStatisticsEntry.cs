using MURP.CharacterSystem;
using MURP.StatsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class VitalStatisticsEntry : MonoBehaviour, PartyEntry
    {
        [SerializeField] Image portraitImage;

        [SerializeField] Text attackValue;
        [SerializeField] Text defValue;
        [SerializeField] Text mdefValue;

        [SerializeField] Text strValue;
        [SerializeField] Text vitValue;
        [SerializeField] Text sprValue;
        [SerializeField] Text aglValue;

        [SerializeField] Text strBonus;
        [SerializeField] Text vitBonus;
        [SerializeField] Text sprBonus;
        [SerializeField] Text aglBonus;

        [SerializeField] IntegerStat attackStat;
        [SerializeField] IntegerStat defStat;
        [SerializeField] IntegerStat mdefStat;

        [SerializeField] IntegerStat strStat;
        [SerializeField] IntegerStat vitStat;
        [SerializeField] IntegerStat sprStat;
        [SerializeField] IntegerStat aglStat;

        public void SetCharacter(Character character)
        {
            this.attackValue.text = character.GetStat(this.attackStat).Value.ToString();
            this.defValue.text = character.GetStat(this.defStat).Value.ToString();
            this.mdefValue.text = character.GetStat(this.mdefStat).Value.ToString();

            this.strValue.text = character.GetStat(this.strStat).Value.ToString();
            this.vitValue.text = character.GetStat(this.vitStat).Value.ToString();
            this.sprValue.text = character.GetStat(this.sprStat).Value.ToString();
            this.aglValue.text = character.GetStat(this.aglStat).Value.ToString();

            // TODO Update the colors of the values and bonuses
            // TODO Update the bonuses
        }
    }
}
