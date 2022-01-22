using MURP.CharacterSystem;
using MURP.StatsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class ConditionEntry : MonoBehaviour
    {
        [SerializeField] Image portraitImage;
        [SerializeField] Image elementImage;

        [SerializeField] Text nameText;
        [SerializeField] Text levelText;
        [SerializeField] Text classText;

        [SerializeField] ConditionBar hpBar;
        [SerializeField] ConditionBar mpBar;
        [SerializeField] Image xpProgressBar;
        [SerializeField] Text xpText;

        [SerializeField] IntegerStat levelStat;
        [SerializeField] IntegerStat currentHpStat;
        [SerializeField] IntegerStat maxHpStat;
        [SerializeField] IntegerStat currentMpStat;
        [SerializeField] IntegerStat maxMpStat;

        public void SetCharacter(Character character)
        {
            // TODO Portrait
            // TODO Element
            this.nameText.text = character.CharacterInfo.displayName;
            this.levelText.text = "Lv " + character.GetStat(this.levelStat).Value;
            // TODO Class
            this.hpBar.SetValues(character.GetStat(this.currentHpStat).Value, character.GetStat(this.maxHpStat).Value);
            // TODO Update mp bar when mana stats are available
            //this.mpBar.SetValues(character.GetStat(this.currentMpStat).Value, character.GetStat(this.maxMpStat).Value);
            // TODO Update XP bar
        }
    }
}
