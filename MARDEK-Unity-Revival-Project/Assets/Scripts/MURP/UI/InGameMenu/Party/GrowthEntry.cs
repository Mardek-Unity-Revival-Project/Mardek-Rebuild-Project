using MURP.CharacterSystem;
using MURP.StatsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class GrowthEntry : MonoBehaviour, PartyEntry
    {
        [SerializeField] Text xpValueText;
        [SerializeField] Text levelText;
        [SerializeField] Image xpProgressBar;

        [SerializeField] IntegerStat levelStat;

        public void SetCharacter(Character character)
        {
            // TODO Update xp value text
            this.levelText.text = "Level " + character.GetStat(this.levelStat).Value;
            // TODO Update xp progress bar
        }
    }
}
